using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using AI_BehaviourTree;
using DG.Tweening;

public class RoamingNPC : MonoBehaviour
{
    #region INSPECTOR

    public Animator anim;
    public Transform startPos;
    public Transform endPos;
    public float m_fNPCRoamingSpeed;
    public float m_fHeroInRange;

    Vector3 destinationPos = Vector3.zero;
    Root ai;
    Hero hero;
    bool m_fArrivalDestination = false;
    #endregion
    void Start()
    {
        ai = BT.Root();
        hero = GameMng.Ins.hero;
        destinationPos = endPos.position;
        SetBehaviourTree();
    }

    void Update()
    {
        ai.Tick();
    }

    bool IsInRange_Hero()
    {
        return Define.IsInRange(transform.position, hero.transform.position, m_fHeroInRange);
    }

    bool IsNotInRange_DestinationPos()
    {
        return (!Define.IsInRange(transform.position, startPos.position, 1f) && !Define.IsInRange(transform.position, endPos.position, 1f));
    }

    void SetBehaviourTree()
    {
        ai.OpenBranch(
            BT.Selector().OpenBranch(
                BT.Sequence().OpenBranch(
                    BT.Condition_Method(IsInRange_Hero), BT.Call(StopMoveLookAtHero)),
                BT.Sequence().OpenBranch(
                    BT.Condition_Method(IsNotInRange_DestinationPos), BT.Call(Move)),
                BT.Call(RotationMoveForNextPos)
                    )
                );
    }

    void Move()
    {
        anim.SetBool("Idle", false);
        transform.DOLookAt(destinationPos, 0.5f, AxisConstraint.None, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, destinationPos, m_fNPCRoamingSpeed * Time.deltaTime);
    }

    void RotationMoveForNextPos()
    {
        if (Define.IsInRange(transform.position, startPos.position, 1f))
            destinationPos = endPos.position;
        else
            destinationPos = startPos.position;
        transform.DOLookAt(destinationPos, 0.5f, AxisConstraint.None, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, destinationPos, m_fNPCRoamingSpeed * Time.deltaTime);

    }

    void StopMoveLookAtHero()
    {
        anim.SetBool("Idle", true);
        transform.DOLookAt(hero.transform.position, 0.5f, AxisConstraint.None, Vector3.up);   
    }
}
