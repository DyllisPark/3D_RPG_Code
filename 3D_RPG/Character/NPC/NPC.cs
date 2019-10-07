using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using DG.Tweening;

public class NPC : MonoBehaviour, IInteraction
{
    #region INSPECTOR

    public Animator anim;

    public eNPCID           m_eNPCID;
    public eNPCType         m_eNPCType;
    public InteractionType  m_eIteractionType { get { return InteractionType.NPC; } }
    public bool             m_bEnable { get; set; }
    public string           m_sInteractionName{ get; set; }
    public string           m_sInterPlayerAnim { get; private set; } = null;

    protected Hero hero;

    #endregion
    
    void Start()
    {
        hero = GameMng.Ins.hero;
    }

    protected virtual void OnTriggerEnter(Collider a_collider)
    {
        if (a_collider.CompareTag("Player") && m_eNPCID == hero.characterData.m_eQuest.GetQuestTb().m_eNPCID)
        {
            ActivateInteraction();
        }
    }

    private void OnTriggerExit(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            if (hero != null)
            {
                UIMng.Ins.Interactiontxt.gameObject.SetActive(false);
                hero.m_IInteraction = null;
                NonShowInter();
                m_bEnable = false;
            }
        }
    }

    protected void ActivateInteraction()
    {
        UIMng.Ins.Interactiontxt.text = string.Format("[F] {0}", m_eIteractionType.ToDesc());
        UIMng.Ins.Interactiontxt.gameObject.SetActive(true);
        m_bEnable = true;
        transform.DORotateQuaternion(transform.GetLookAtQuaternion(hero.gameObject.transform), 1f);
        hero.Interaction(this);
        ShowInter();
    }

    protected void InteractionActionKey()
    {
        hero.m_bAllActivateState = false;

        if(UIMng.Ins.questMissionUI.gameObject.activeSelf == false)
        {
            anim.SetTrigger("Talk");
        }
        else
        {
            NonShowInter();
        }
    }

    public void ShowInter()
    {
        anim.SetTrigger("HandShake");
    }

    public virtual void ActionKey()
    {
        InteractionActionKey();
        UIMng.Ins.questMissionUI.gameObject.ActiveChange();
    }

    public void NonShowInter()
    {
        hero.m_bAllActivateState = true;
    }

    public void EndInter()
    {
    }

}
