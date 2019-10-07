using UnityEngine;
using AI_BehaviourTree;
using Global_Define;

public class MeleeMonster : Monster
{
    #region INSPECTOR
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
    }


    void SetBehaviourTree()
    {
        //선공 몬스터 AI
        if(monsterInfo.m_eMonsterAtkType == eMonsterAtkType.Aggressive)
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
        //비선공 몬스터 AI
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

    protected override void Attack()
    {
        anim.SetFloat("AtkSpeed", m_fAtkTime);
        if (m_fAtkTime > monsterInfo.m_fAtkSpeed)
        {
            transform.LookAt(targetHero.transform);
            anim.SetInteger("KindOfAttack", Random.Range(1, Define.nMELLEMONSTER_KIND_OF_ATTACK + 1));
            anim.SetBool("IsInChaseRange", IsInChaseRange());
            anim.SetBool("IsInAtkRange", IsInAtkRange());

            var hits = Physics.SphereCastAll(transform.position, 2f, Vector3.up, 0f);
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    GameMng.Ins.damageCalc.HitToHero(this, targetHero);
                }
            }
            m_fAtkTime = 0;
        }
    }

}
