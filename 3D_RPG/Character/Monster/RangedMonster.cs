using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI_BehaviourTree;
using Global_Define;

public class RangedMonster : Monster
{
    #region INSPECTOR

    public GameObject   bulletPoint;

    float   m_fEvasionTime = 0f;
    bool    m_bAtkFailState = false;
    #endregion

    new void Start()
    {
        base.Start();
        spawnPos = transform.position;
        ai = BT.Root();
        SetBehaviourTree();
    }

    void Update()
    {
        DoUpdate();
        m_fEvasionTime += Time.deltaTime;
    }


    void SetBehaviourTree()
    {
        if (monsterInfo.m_eMonsterAtkType == eMonsterAtkType.Aggressive)
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
                                    BT.Condition_Method(IsEvasion), BT.Call(Evasion)),
                                BT.Sequence().OpenBranch(
                                    BT.Condition_Method(IsInAtkRange), BT.Call(Attack)),
                                BT.Call(Chase))),
                        BT.Selector().OpenBranch(
                            BT.Sequence().OpenBranch(
                                BT.Condition_Method(IsPatrol), BT.Call(Patrol)),
                            BT.Selector().OpenBranch(
                                BT.RandomAction(m_nArrRandomActionPercent).OpenBranch(
                                    BT.Call(Idle),
                                    BT.Call(ActivatePatrolState))
                                )
                            )
                        )
                    ),
                BT.Call(ActivateReturnSpawnPosState))
            );
        }
        else
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
                            BT.Condition_Method(IsAggressive),
                            BT.Sequence().OpenBranch(
                                BT.Condition_Method(IsInChaseRange),
                                BT.Selector().OpenBranch(
                                    BT.Sequence().OpenBranch(
                                        BT.Condition_Method(IsInAtkRange), BT.Call(Attack)),
                                    BT.Call(Chase)))),
                        BT.Selector().OpenBranch(
                            BT.Sequence().OpenBranch(
                                BT.Condition_Method(IsPatrol), BT.Call(Patrol)),
                            BT.Selector().OpenBranch(
                                BT.RandomAction(m_nArrRandomActionPercent).OpenBranch(
                                    BT.Call(Idle),
                                    BT.Call(ActivatePatrolState))
                                )
                            )
                        )
                    ),
                BT.Call(ActivateReturnSpawnPosState))
            );
        }
    }


    bool IsEvasion()
    {
        return Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fEvasionRange) && m_fEvasionTime > monsterInfo.m_fEvasionTime;
    }

    //히어로가 일정 범위내로 접근하면 뒤로 회피.
    protected override void Evasion()
    {
        m_fEvasionTime = 0;
        anim.SetTrigger("Evasion");
    }

    protected override void Attack()
    {
        anim.SetFloat("AtkSpeed", m_fAtkTime);
        if (m_fAtkTime > monsterInfo.m_fAtkSpeed)
        {
            transform.LookAt(targetHero.transform);
            anim.SetInteger("KindOfAttack", Random.Range(1, Define.nRANGEDMONSTER_KIND_OF_ATTACK + 1));
            anim.SetBool("IsInAtkRange", IsInAtkRange());
            anim.SetBool("IsInChaseRange", IsInChaseRange());
            m_fAtkTime = 0;
        }

    }
    
    //==> 애니메이션 이벤트 함수
    public override void Hit()
    {
        StartCoroutine(FireBullet());
    }
    //<==
    IEnumerator FireBullet()
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
                
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, targetHero.transform.position + Define.addPosForHeroBody, monsterInfo.m_fBulletSpeed* Time.deltaTime);
            yield return null;
        }

        StartCoroutine(BulletExplosion(bullet));
        if(m_bAtkFailState == false)
            GameMng.Ins.damageCalc.HitToHero(this, targetHero);

        bullet.SetActive(false);
        EffectMng.Ins.gameObject.transform.SetChild(bullet);
        --EffectMng.Ins.m_dicUsingBulletCount[monsterInfo.m_eRangedMonsterBullet];
        m_bAtkFailState = false;
    }

    IEnumerator BulletExplosion(GameObject a_bullet)
    {
        GameObject explosion = EffectMng.Ins.GetExplosion(monsterInfo.m_eRangedMonsterExlposion);
        explosion.transform.position = a_bullet.transform.position;
        explosion.SetActive(true);
        yield return YieldReturnCaching.WaitForSeconds(1f);
        explosion.SetActive(false);
        EffectMng.Ins.gameObject.transform.SetChild(explosion);
        --EffectMng.Ins.m_dicUsingExplosionCount[monsterInfo.m_eRangedMonsterExlposion];
    }
}
