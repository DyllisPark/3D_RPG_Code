using UnityEngine;
using Global_Define;

public class EntryDuegeon : MonoBehaviour, IInteraction
{
    #region INSPECTOR

    public InteractionType m_eIteractionType { get { return InteractionType.Entry_Dungeon; } }
    public bool m_bEnable { get; set; }
    public string m_sInteractionName { get; set; }
    public string m_sInterPlayerAnim { get; private set; } = null;

    Hero hero;

    #endregion

    private void Start()
    {
        hero = GameMng.Ins.hero;
    }

    void OnTriggerEnter(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            hero.Interaction(this);
            ShowInter();
        }
    }

    void OnTriggerExit(Collider a_collider)
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

    public void ShowInter()
    {
        UIMng.Ins.Interactiontxt.text = string.Format("[F] {0}", m_eIteractionType.ToDesc());
        UIMng.Ins.Interactiontxt.gameObject.ActiveChange();
    }

    public void ActionKey()
    {
        ItemMng.Ins.SaveItemInfo();
        SceneMng.Ins.SaveSceneToMoveInfo(eScene.Dungeon_KroloalCradleScene);
    }

    public void NonShowInter()
    {
        UIMng.Ins.Interactiontxt.gameObject.ActiveChange();
    }

    public void EndInter()
    {
        hero.m_bAllActivateState = true;
    }
}
