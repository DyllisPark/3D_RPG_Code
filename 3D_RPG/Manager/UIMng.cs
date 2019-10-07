using Global_Define;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using DG.Tweening;

public class UIMng : MonoBehaviour
{
    #region SINGLETON
    static UIMng _instance = null;

    public static UIMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(UIMng)) as UIMng;
                if (_instance == null)
                {
                    _instance = new GameObject("UIMng", typeof(UIMng)).GetComponent<UIMng>();
                }
            }

            return _instance;
        }
    }



    #endregion

    #region INSPECTOR
    public UI_Main                  mainUI;
    public UI_QuestMission          questMissionUI;
    public UI_QuestList_Expantion   questList_ExpantionUI;
    public UI_QuestList_Reduction   questList_ReductionUI;
    public UI_Inventory             inventoryUI;
    public UI_CharacterState        characterStateUI;
    public UI_ItemInfo              itemInfoUI;
    public UI_InteractionLoading    interactionLoadingUI;
    public UI_DropItem              dropItemUI;
    public UI_TargetMonster         targettedMonsterUI;
    public UI_Map                   mapUI;
    public UI_EquipGem              equipGemUI;
    public UI_ItemSlot              tempDragItem;

    public Image        localNameBgImg;
    public Text         localNametxt;
    public Text         Interactiontxt;
    public Text         exceptiontxt;
    public SpriteAtlas  itemAtlas;
    public GameObject   damageTextRoot;
    public GameObject   monsterHPBarRoot;
    public GameObject   exceptionUI;

    #endregion


    public void LocalNameFadeOut(string a_sLocalName)
    {
        localNametxt.text = a_sLocalName;
        localNametxt.DOFade(0, 1.5f).OnComplete(() => localNametxt.gameObject.SetActive(false));
        localNameBgImg.DOFade(0, 1.5f).OnComplete(() => localNameBgImg.gameObject.SetActive(false));
    }

    public void OnClickClosedBtn(GameObject a_obj)
    {
        a_obj.SetActive(false);
        GameMng.Ins.hero.m_bAllActivateState = true;
    }    
}
