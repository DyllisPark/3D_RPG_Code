using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_BehaviourTree;
using Global_Define;

public class Boss : Monster, IRangedAtkBullet   //던전의 보스. 기본적인 근거리 공격, 원거리 공격이 있고, 페이즈에 따라 원거리 공격이 추가됨.
{
    #region INSPECTOR
    public GameObject bulletPoint;

    bool m_bAtkFailState = false;
    bool m_bMeleeAtkState = false;
    bool m_bAddFirstPhaseSkill = true;
    bool m_bAddSecondPhaseSKill = true;
    bool m_bFirstPhase { get { return m_fHpRate < 0.7; } }
    bool m_bSecondPhase { get { return m_fHpRate < 0.4; } }
    List<eBossAtkType> m_liMeleeSkillAtk = new List<eBossAtkType>();
    List<eBossAtkType> m_liRangedSkillAtk = new List<eBossAtkType>();
    eBossAtkType m_eBossAtkType = eBossAtkType.None;
    #endregion

    new void Start()
    {
        base.Start();
        spawnPos = transform.position;
        ai = BT.Root();
        SetBehaviourTree();
        SetBasicSkill();
    }

    void Update()
    {
        DoUpdate();
        AddPhaseSkillCheck();
    }

    void SetBasicSkill()
    {
        m_liMeleeSkillAtk.Add(eBossAtkType.MeleeBasicAtk1);
        m_liRangedSkillAtk.Add(eBossAtkType.RangedBasicAtk1);
    }

    void SetBehaviourTree()
    {
        ai.OpenBranch(
        BT.Selector().OpenBranch(
            BT.Sequence().OpenBranch(
                BT.Condition_Method(IsDead), BT.Call(Dead)),
            BT.Sequence().OpenBranch(
                BT.Condition_Method(IsReturnSpawnPos), BT.Call(ReturnSpawnPos)),
            BT.Sequence().OpenBranch(
                BT.Condition_Method(IsInMaximumMoveRange),
                BT.Selector().OpenBranch(
                    BT.Sequence().OpenBranch(
                        BT.Condition_Method(IsInChaseRange),
                        BT.Selector().OpenBranch(
                            BT.Sequence().OpenBranch(
                                BT.Condition_Method(IsInMeleeAtkRange), BT.Call(Attack)),
                            BT.Sequence().OpenBranch(
                                BT.Condition_Method(IsInRangedAtkRange), BT.Call(Attack)),
                            BT.Call(Chase))),
                    BT.Call(Idle))),
            BT.Call(ActivateReturnSpawnPosState))
        );
    }

    protected override bool IsInAtkRange()
    {
        return Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fMeleeAtkRange) ||
            Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fRangedAtkRange);
    }

    bool IsInMeleeAtkRange()
    {
        m_bMeleeAtkState = true;
        return Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fMeleeAtkRange);
    }

    bool IsInRangedAtkRange()
    {
        m_bMeleeAtkState = false;
        return Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fRangedAtkRange);
    }

    protected override void Chase()
    {
        transform.LookAt(targetHero.transform);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetHero.transform.position.x, transform.position.y, targetHero.transform.position.z), monsterInfo.m_fMoveSpeed * Time.deltaTime);
        anim.SetBool("IsInChaseRange", IsInChaseRange());
        anim.SetBool("IsInAtkRange", IsInAtkRange());
        m_fAtkTime = monsterInfo.m_fAtkSpeed + Time.deltaTime;
        anim.SetFloat("AtkSpeed", m_fAtkTime);
    }


    protected override void Attack()
    {
        anim.SetFloat("AtkSpeed", m_fAtkTime);
        anim.SetBool("IsInAtkRange", IsInAtkRange());
        anim.SetBool("IsInChaseRange", IsInChaseRange());
        if (m_fAtkTime > monsterInfo.m_fAtkSpeed)
        {
            m_fAtkTime = 0;
            transform.LookAt(targetHero.transform);
            //eBossAtkType eBossAtk = eBossAtkType.None;
            if (m_bMeleeAtkState == true)
            {
                m_eBossAtkType = m_liMeleeSkillAtk[Random.Range(0, m_liMeleeSkillAtk.Count)];
                
                var hits = Physics.SphereCastAll(transform.position, monsterInfo.m_fMeleeAtkRange, Vector3.up, 0f);
                foreach (var hit in hits)
                {
                    if (hit.transform.gameObject.CompareTag("Player"))
                    {
                        
                        if (m_eBossAtkType == eBossAtkType.MeleeBasicAtk1)
                            GameMng.Ins.damageCalc.HitToHero(this, targetHero);
                        //else
                        //    StartCoroutine(ActivateSkillEffect(eBossAtk));
                    }
                }
            }
            else
            {
                m_eBossAtkType = m_liRangedSkillAtk[Random.Range(0, m_liRangedSkillAtk.Count)];
                //if(eBossAtk != eBossAtkType.RangedBasicAtk1)
                //    StartCoroutine(ActivateSkillEffect(eBossAtk));
                
            }
            anim.SetInteger("KindOfAttack", (int)m_eBossAtkType);
            
        }
    }

    //보스의 페이즈에 따라 스킬 추가.
    void AddPhaseSkillCheck()
    {
        if (m_bFirstPhase)
        {
            if (m_bSecondPhase)
            {
                if (m_bAddSecondPhaseSKill)
                {
                    m_liMeleeSkillAtk.Add(eBossAtkType.MeleeSkillAtk1);
                    m_bAddSecondPhaseSKill = false;
                }
            }
            else
            {
                if (m_bAddFirstPhaseSkill)
                {
                    m_liMeleeSkillAtk.Add(eBossAtkType.MeleeSkillAtk2);
                    m_liRangedSkillAtk.Add(eBossAtkType.RangedSkillAtk1);
                    m_bAddFirstPhaseSkill = false;
                }
            }
        }
    }
   
    public IEnumerator FireBullet()
    {
        float fBulletMaintainTime = 0f;
        GameObject bullet = EffectMng.Ins.GetBullet(monsterInfo.m_eRangedMonsterBullet);
        bullet.transform.position = bulletPoint.transform.position;
        bullet.SetActive(true);

        while (bullet.transform.position != targetHero.transform.position + Define.addPosForHeroBody)
        {
            fBulletMaintainTime += Time.deltaTime;
            if (fBulletMaintainTime > Define.fBulletMaintainMaxTime)
            {
                m_bAtkFailState = true;
                break;
            }

            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetHero.transform.position + Define.addPosForHeroBody, monsterInfo.m_fBulletSpeed * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(BulletExplosion(bullet));
        if (m_bAtkFailState == false)
            GameMng.Ins.damageCalc.HitToHero(this, targetHero);

        bullet.SetActive(false);
        EffectMng.Ins.gameObject.transform.SetChild(bullet);
        --EffectMng.Ins.m_dicUsingBulletCount[monsterInfo.m_eRangedMonsterBullet];
        m_bAtkFailState = false;
    }

    public IEnumerator BulletExplosion(GameObject a_bullet)
    {
        GameObject explosion = EffectMng.Ins.GetExplosion(monsterInfo.m_eRangedMonsterExlposion);
        explosion.transform.position = a_bullet.transform.position;
        explosion.SetActive(true);
        yield return YieldReturnCaching.WaitForSeconds(1f);
        explosion.SetActive(false);
        EffectMng.Ins.gameObject.transform.SetChild(explosion);
        --EffectMng.Ins.m_dicUsingExplosionCount[monsterInfo.m_eRangedMonsterExlposion];
    }

    public void MeleeAtk()
    {
        GameMng.Ins.damageCalc.HitToHero(this, targetHero);
    }

    //==> 애니메이션 이벤트 함수
    public override void Hit()
    {
        StartCoroutine(FireBullet());
    }

    //<==

    //IEnumerator ActivateSkillEffect(eBossAtkType a_eBossAtkType)    
    public void ActivateSkillEffect()//eBossAtkType a_eBossAtkType)
    {
        if (m_eBossAtkType == eBossAtkType.None) return;//yield return null;

        //yield return YieldReturnCaching.WaitForSeconds(1.3f);

        GameObject effect = EffectMng.Ins.GetBossSkill(m_eBossAtkType);

        if (m_eBossAtkType >= eBossAtkType.MeleeSkillAtk1 && m_eBossAtkType <= eBossAtkType.MeleeSkillAtk2)
        {
            effect.transform.position = transform.position;
        }
        else
        {
            effect.transform.position = targetHero.transform.position;
        }
        
        effect.SetActive(true);
        GameMng.Ins.damageCalc.HitToHero(this, targetHero);
        StartCoroutine(DeActivateSkillEffect(effect, m_eBossAtkType));
    }

    IEnumerator DeActivateSkillEffect(GameObject a_objEffect, eBossAtkType a_eBossAtkType)
    {
        yield return YieldReturnCaching.WaitForSeconds(5f);
        a_objEffect.SetActive(false);
        --EffectMng.Ins.m_dicBossSkillEffectCount[a_eBossAtkType];
    }
}
