using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public partial class Monster : MonoBehaviour
{
    protected bool IsInChaseRange()
    {
        return Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fChaseRange);
    }

    protected virtual bool IsInAtkRange()
    {
        return Define.IsInRange(targetHero.transform.position, transform.position, monsterInfo.m_fAtkRange);
    }

    protected bool IsInMaximumMoveRange()
    {
        return Define.IsInRange(spawnPos, transform.position, monsterInfo.m_fMaximumMoveRange);
    }

    protected bool IsDead()
    {
        return m_bDeadState;
    }

    protected bool IsPatrol()
    {
        return m_bPatrolState;
    }

    protected bool IsReturnSpawnPos()
    {
        return m_bRetrunSpawnPosState;
    }

    protected bool IsAggressive()
    {
        return m_bAggressiveState;
    }

    protected virtual void Attack() { }
    protected virtual void Evasion() { }
    public virtual void Hit() { }

    protected virtual void Chase()
    {
        transform.LookAt(targetHero.transform);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetHero.transform.position.x, transform.position.y, targetHero.transform.position.z), monsterInfo.m_fMoveSpeed * Time.deltaTime);
        anim.SetBool("IsInChaseRange", IsInChaseRange());
        anim.SetBool("IsInAtkRange", IsInAtkRange());
        m_bPatrolState = false;
        anim.SetBool("IsPatrol", m_bPatrolState);
    }

    protected void Idle()
    {
        if (monsterInfo.m_eMonsterAtkType == eMonsterAtkType.Passive)
            m_bAggressiveState = false;
        //anim.SetBool("IsInChaseRange", IsInChaseRange());
    }

    protected void Dead()
    {
        anim.SetBool("IsDead", IsDead());
        anim.SetTrigger("AIState");
        m_bAIState = false;
        QuestMng.Ins.QuestMonsterCheck(monsterInfo.m_eMonster);
    }

    protected void Patrol()
    {
        anim.SetBool("IsPatrol", m_bPatrolState);
        transform.LookAt(patrolPos);
        transform.position = Vector3.MoveTowards(transform.position, patrolPos, monsterInfo.m_fMoveSpeed * Time.deltaTime);
        if (Define.IsInRange(transform.position,patrolPos,1f))
        {
            m_bPatrolState = false;
            anim.SetBool("IsPatrol", m_bPatrolState);
        }
    }

    protected void ReturnSpawnPos()
    {
        if (monsterInfo.m_eMonsterAtkType == eMonsterAtkType.Passive)
            m_bAggressiveState = false;
        anim.SetBool("IsInChaseRange", m_bRetrunSpawnPosState);
        transform.LookAt(spawnPos);
        transform.position = Vector3.MoveTowards(transform.position, spawnPos, monsterInfo.m_fMoveSpeed * Time.deltaTime);
        if (Define.IsInRange(transform.position, spawnPos, 1f))
        {
            m_bRetrunSpawnPosState = false;
            anim.SetBool("IsInChaseRange", m_bRetrunSpawnPosState);
            anim.SetBool("IsPatrol", m_bPatrolState);
            m_fPatrolTime = 0;
        }
    }

    protected void ActivatePatrolState()
    {
        if (m_fPatrolTime > monsterInfo.m_fPossiblePatrolTime)
        {
            float randXPos = Random.Range(-randomPatrolPos.x / 2, randomPatrolPos.x / 2); //-monsterInfo.m_fPatrolRandomXPos, monsterInfo.m_fPatrolRandomXPos);
            float randZPos = Random.Range(-randomPatrolPos.z / 2, randomPatrolPos.z / 2);
            patrolPos = new Vector3(spawnPos.x + randXPos, transform.position.y, spawnPos.z + randZPos);
            m_bPatrolState = true;
            m_fPatrolTime = 0;
        }
    }

    protected void ActivateReturnSpawnPosState()
    {
        m_bRetrunSpawnPosState = true;
        m_bPatrolState = false;
    }

}
