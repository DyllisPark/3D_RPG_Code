using System.Collections;
using UnityEngine;
using Global_Define;

public partial class Hero : MonoBehaviour
{
    void Move()
    {
        if (characterCtrl.isGrounded)
        {
            m_vcMoveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            if (m_vcMoveDir.sqrMagnitude <= 1)
                m_vcMoveDir *= characterData.nowStat.m_fMoveSpeed;
            else
            {
                m_vcMoveDir.Normalize(); 
                m_vcMoveDir *= characterData.nowStat.m_fMoveSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("JumpTrigger");
                m_vcMoveDir.y = characterData.nowStat.m_fJumpSpeed;
            }
            
            if (m_vcMoveDir != Vector3.zero && m_vcMoveDir.y == 0)
            {
                transform.rotation = Quaternion.LookRotation(cameraVec.TransformDirection(m_vcMoveDir));  // 카메라가 바라보는 방향으로 회전
            }
            m_vcMoveDir = cameraVec.TransformDirection(m_vcMoveDir);        //카메라가 바라보는 방향으로 이동

            anim.SetInteger("Jump", (int)m_vcMoveDir.y);
            anim.SetFloat("Velocity", new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).sqrMagnitude);
        }        
        m_vcMoveDir.y -= characterData.nowStat.m_fGravity * Time.deltaTime;
        characterCtrl.Move(m_vcMoveDir * Time.deltaTime);
    }

    void ActivateFromInputKey()
    {
        KeyCodeCheck();
    }

    public void Interaction(IInteraction a_Interaction)
    {
        if (a_Interaction == null) { Debug.LogError("Interaction error"); return; }

        m_IInteraction = a_Interaction;
    }

    public void Interaction(IInteraction a_Interaction, Monster a_InteractionMonster)
    {
        interactionMonster = a_InteractionMonster;
        Interaction(a_Interaction);
    }

    public void InteractionAction()
    {
        if (m_IInteraction != null)
        {
            m_IInteraction.ActionKey();
            if (m_IInteraction.m_sInterPlayerAnim != null)
            {
                anim.SetTrigger(m_IInteraction.m_sInterPlayerAnim);
            }
        }
    }

    void KeyCodeCheck()
    {
        foreach (var keyCode in m_dicKeyCode)
        {
            if (Input.GetKeyDown(keyCode.Key))
            {
                nowKeyCode = keyCode.Key;
                m_dicKeyCode[nowKeyCode]?.Invoke();
                return;
            }
        }
    }

    
    void ActiveSkillAnim(eHeroState a_eHeroState)
    {
        m_eHeroState = a_eHeroState;
        
        anim.SetInteger("HeroState", (int)a_eHeroState);
        if (m_eHeroState == eHeroState.NontargetSkill1 || m_eHeroState == eHeroState.NontargetSkill2 || m_eHeroState == eHeroState.NontargetSkill3)
            weaponTrailRenderer.gameObject.SetActive(true);

        m_bMoveState = false;
        m_bSkillState = false;
    }

    void NontargetSkillActivate(eHeroState a_eHeroState)
    {
        if (m_fCurrentMp >= a_eHeroState.GetHeroActiveSkillTb().m_nSkillUsedMp)
        {
            anim.SetTrigger("NontargetSkillTrigger");
            ActiveSkillAnim(a_eHeroState);
        }

    }

    void TargetSkillActivate(eHeroState a_eHeroState)
    {
        if (m_fCurrentMp >= a_eHeroState.GetHeroActiveSkillTb().m_nSkillUsedMp && targetMonster != null)
        {

            anim.SetTrigger("TargetSkillTrigger");
            ActiveSkillAnim(a_eHeroState);
        }
    }

    //사용 스킬의 범위를 기반으로 몬스터 탐색
    void HitMonsters(eHeroState a_eHeroState)
    {
        HeroActiveSkillData skillData = a_eHeroState.GetHeroActiveSkillTb();
        m_fCurrentMp -= skillData.m_nSkillUsedMp;
        UIMng.Ins.mainUI.MpRateUpdate();

        var hits = Physics.SphereCastAll(transform.position, skillData.m_fSkillRadius, Vector3.up, 0f);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                var monster = hit.transform.gameObject.GetComponent<Monster>();
                monster.rimLightShader.SetActive();
                if(monster.m_bDeadState == false)
                {
                    GameMng.Ins.damageCalc.HitToMonster(this, monster, skillData.m_nSkillPow);
                    monster.m_bAggressiveState = true;
                }
            }
        }
    }


    void AvoidSkillActivate(eHeroState a_eHeroState)
    {
        anim.SetTrigger("AvoidSkillTrigger");
        ActiveSkillAnim(a_eHeroState);
    }

    public void ActivateHandEffect()
    {
        if (m_eHeroState == eHeroState.None) return;

        leftHandEffect = EffectMng.Ins.GetHandEffect(m_eHeroState);
        rightHandEffect = EffectMng.Ins.GetHandEffect(m_eHeroState);
        leftHandTrans.SetChild(leftHandEffect);
        leftHandEffect.SetActive(true);
        rightHandTrans.SetChild(rightHandEffect);
        rightHandEffect.SetActive(true);
    }

    public void ActivateSkillEffect()
    {
        if (m_eHeroState == eHeroState.None) return;

        GameObject effect = EffectMng.Ins.GetSkillEffect(m_eHeroState);
        
        if(m_eHeroState >= eHeroState.NontargetSkill1 && m_eHeroState <= eHeroState.NontargetSkill8)
        {
            effect.transform.position = nontargetSkillTrans.position;            
        }
        else
        {
            //if (targetSkillTrans != null)
            effect.transform.position = targetMonster.attackedSkillPos.position;
        }
        effect.SetActive(true);
        StartCoroutine(DeActivateSkillEffect(effect, m_eHeroState));
    }
    
    //==>애니메이션 이벤트 함수
    public void AnimationEnd()
    {
        if (weaponTrailRenderer != null)
            weaponTrailRenderer.gameObject.SetActive(false);
        anim.SetInteger("HeroState", (int)eHeroState.Idle);
        m_bMoveState = true;
        m_bSkillState = true;
        if(leftHandEffect != null && rightHandEffect != null)
            EffectMng.Ins.SetHandEffectReset(leftHandEffect, rightHandEffect);
        leftHandEffect = null;
        rightHandEffect = null;
    }

    public void Hit()
    {
        HitMonsters(m_eHeroState);
    }
    //<==

    //HP, MP 자동회복
    void HpMpRegenerate()
    {
        m_fRegenerateHpMpTime += Time.deltaTime;
        if (m_fRegenerateHpMpTime > Define.fREGENERATE_LIMITTIME)
        {
            if (m_fCurrentHp < characterData.nowStat.m_nHp)
            {
                m_fCurrentHp += heroTbData.m_nRegenerateHp;
                UIMng.Ins.mainUI.HpRateUpdate();
            }
            if (m_fCurrentMp < characterData.nowStat.m_nMp)
            {
                m_fCurrentMp += heroTbData.m_nRegenerateMp;
                UIMng.Ins.mainUI.MpRateUpdate();
            }
            m_fRegenerateHpMpTime = 0;
        }
    }

    void MonsterTargetting()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.transform.gameObject.CompareTag("Enemy"))
            {
                targetMonster = hit.transform.gameObject.GetComponent<Monster>();
                UIMng.Ins.targettedMonsterUI.TargettedMonsterInfo(targetMonster);
                UIMng.Ins.targettedMonsterUI.gameObject.SetActive(true);
            }
            else
            {
                UIMng.Ins.targettedMonsterUI.gameObject.SetActive(false);
                targetMonster = null;
            }                
        }
    }
    

    IEnumerator DeActivateSkillEffect(GameObject a_objEffect, eHeroState a_eHeroState)
    {
        yield return YieldReturnCaching.WaitForSeconds(m_eHeroState.GetHeroActiveSkillTb().m_fMaintainTime);
        a_objEffect.SetActive(false);
        --EffectMng.Ins.m_dicSkillEffectCount[a_eHeroState];
    }

}
