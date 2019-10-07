using UnityEngine;
using Global_Define;

//생활 컨텐츠용 오브젝트 클래스(현재 나무, 낚시 가능한 물, 벼
public class MaterialObject : MonoBehaviour, IInteraction
{
    #region INSPECTOR
    public eItemID m_eItemID;
    public eLifeAction m_eLifeAction;
    public InteractionType m_eIteractionType { get { return InteractionType.LifeContents; } }
    public bool m_bEnable { get; set; }
    public string m_sInteractionName { get; set; }
    public string m_sInterPlayerAnim { get; private set; } = null;

    Hero hero;
    #endregion


    private void Start()
    {
        m_bEnable = true;
        hero = GameMng.Ins.hero;
        m_sInteractionName = m_eLifeAction.ToDesc();
        switch (m_eLifeAction)
        {
            case eLifeAction.Felling:
                m_sInterPlayerAnim = "Felling";
                break;
            case eLifeAction.Fishing:
                m_sInterPlayerAnim = "Fishing";
                break;
            case eLifeAction.Harvesting:
                m_sInterPlayerAnim = "Harvesting";
                break;
            case eLifeAction.Repairing:
                m_sInterPlayerAnim = "Repairing";
                break;
            case eLifeAction.Sawing:
                m_sInterPlayerAnim = "Sawing";
                break;
            case eLifeAction.None:
            default:
                Debug.LogError("set eLifeAction");
                break;
        }
    }

    private void OnTriggerEnter(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            hero.Interaction(this);
            ShowInter();
        }
    }
    private void OnTriggerExit(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            NonShowInter();
        }
    }


    public void ShowInter()
    {
        UIMng.Ins.Interactiontxt.text = string.Format("[F] {0}하기", m_sInteractionName);
        UIMng.Ins.Interactiontxt.gameObject.ActiveChange();
    }

    public void ActionKey()
    {
        hero.m_bAllActivateState = false;
        //UIMng.Ins.Interactiontxt.gameObject.ActiveChange();
        UIMng.Ins.interactionLoadingUI.gameObject.ActiveChange();
        //var mUI_InteractionLoading = UIMng.GetPopup<UI_InteractionLoading>();
        //mUI_InteractionLoading.gameObject.SetActive(true);
        UIMng.Ins.interactionLoadingUI.SetInteractionLoadingUI(this, m_sInteractionName, m_eItemID ,m_eLifeAction, 5f);
        //Camera.main.fieldOfView = fieldOfViewAction;
        //Camera.main.transform.DOLookAt(transform.position, 1f);

    }

    public void NonShowInter()
    {
        UIMng.Ins.Interactiontxt.gameObject.ActiveChange();
    }

    public void EndInter()
    {
        hero.m_bAllActivateState= true;
        //var mUI_InteractionLoading = UIMng.GetPopup<UI_ItemLootBox>();
        //mUI_InteractionLoading.gameObject.SetActive(true);
        //mUI_InteractionLoading.gameObject.transform.DOLocalMoveX(fCONVERSATION_POS, 2f);

        //Camera.main.fieldOfView = fieldOfViewOrigin;
    }
}
