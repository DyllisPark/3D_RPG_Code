using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//NPC Interaction시 나타나는 퀘스트 UI
public class UI_QuestMission : MonoBehaviour
{
    #region INSPECTOR

    public Text questName;
    public Text questStroy;
    public Text questExplanation;
    public GameObject acceptBtn;
    public GameObject refusalBtn;
    public GameObject receiveBtn;

    public QuestData questData;
    public List<UI_ItemSlot> m_liRewardItem = new List<UI_ItemSlot>();
    public eQuest m_eQuest;

    bool m_bQuestCompleteState;
    bool m_bGetQuestPurposeItem = false;
    bool m_bGetQuestMonster     = false;
    Queue<eItemID> m_qItems = new Queue<eItemID>();

    #endregion

    void QuestInfo()
    {
        m_eQuest                = GameMng.Ins.hero.characterData.m_eQuest;
        questData               = m_eQuest.GetQuestTb();
        questName.text          = questData.m_sQuestName;
        questStroy.text         = questData.m_sQuestStory;
        questExplanation.text   = questData.m_sQuestExplanation;
        questData.m_sRewardItems.GetRewardItem(m_qItems);
    }

    //현재 퀘스트의 진행도, 퀘스트 타입에 따라 수락, 거절, 보상받기 버튼 활성화 조절.
    private void OnEnable()
    {
        QuestInfo();
        m_bQuestCompleteState = true;

        for (int i = 0; i < m_liRewardItem.Count; ++i)
        {
            if(i <questData.m_nRewardItemCount)
            {
                m_liRewardItem[i].ItemAdd(m_qItems.Dequeue());
            }
            else
            {
                m_liRewardItem[i].Clear();//.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(eItemID.None.ToDesc());
            }
        }

        QuestCompleteCheck();
        if (questData.m_eQuestState == eQuestState.QuestReceive || questData.m_eQuestState == eQuestState.QuestProceeding)
        {
            acceptBtn.SetActive(true);
            refusalBtn.SetActive(true);
            receiveBtn.SetActive(false);
            
        }
        else                    
        {
            if (m_bQuestCompleteState == true)
            {
                acceptBtn.SetActive(false);
                refusalBtn.SetActive(false);
                receiveBtn.SetActive(true);
            }
            else
            {
                acceptBtn.SetActive(false);
                refusalBtn.SetActive(false);
                receiveBtn.SetActive(false);
            }
        }
    }

    //채집, 처치 등의 퀘스트의 경우 퀘스트 완료 조건 체크.
    public void QuestCompleteCheck()
    {
        eQuestPurPoseType ePurposeType = m_eQuest.GetQuestTb().m_eQuestPurposeType;
        QuestPurposeData questpurposeData = null;

        if (m_eQuest.GetQuestTb().m_nPurposeTbID != 0)
        {
            questpurposeData = m_eQuest.GetQuestTb().m_nPurposeTbID.GetQuestPurposeTb();
        }

        if (questpurposeData != null)
        {
            if (ePurposeType == eQuestPurPoseType.Gain)
            {
                if (m_bGetQuestPurposeItem == false)
                {
                    questpurposeData.m_sPurposeItems.GetQuestPurposeTest(questpurposeData.m_sPurposeItemCount, QuestMng.Ins.m_dicCompleteQuestPurpose_Gain, QuestMng.Ins.m_dicNowQuestPurpose_Gain);
                    m_bGetQuestPurposeItem = true;
                }

                foreach (var item in QuestMng.Ins.m_dicCompleteQuestPurpose_Gain)
                {
                    if (QuestMng.Ins.m_dicNowQuestPurpose_Gain.ContainsKey(item.Key) == false || item.Value > QuestMng.Ins.m_dicNowQuestPurpose_Gain[item.Key])
                    {
                        m_bQuestCompleteState = false;
                        break;
                    }
                }
            }
            else if (ePurposeType == eQuestPurPoseType.Kill)
            {
                if (m_bGetQuestMonster == false)
                {
                    questpurposeData.m_sPurposeMonsters.GetQuestPurposeTest(questpurposeData.m_sPurposeMonsterCount, QuestMng.Ins.m_dicCompleteQuestPurpose_Kill, QuestMng.Ins.m_dicNowQuestPurpose_Kill);
                    m_bGetQuestMonster = true;
                }

                foreach (var item in QuestMng.Ins.m_dicCompleteQuestPurpose_Kill)
                {
                    if (QuestMng.Ins.m_dicNowQuestPurpose_Kill.ContainsKey(item.Key) == false || item.Value > QuestMng.Ins.m_dicNowQuestPurpose_Kill[item.Key])
                    {
                        m_bQuestCompleteState = false;
                        break;
                    }
                }
            }            
        }
    }


    //퀘스트 수락시 퀘스트 리스트에 추가, 퀘스트의 다음 진행을 위한 NPC 설정.
    public void OnClickQuestAccept()    
    {        
        QuestMng.Ins.AddQuestList(m_eQuest);
        UIMng.Ins.questList_ReductionUI.QuestList_ReductionUIInit(m_eQuest);
        ++GameMng.Ins.hero.characterData.m_eQuest;
        QuestMng.Ins.QuestNPCActivate();

        if(GameMng.Ins.hero.characterData.m_eQuest.GetQuestTb().m_eQuestState == eQuestState.QuestComplete)
        {
            QuestMng.Ins.QuestNPCUpdate();
        }        
        GameMng.Ins.hero.InteractionAction();
        UIMng.Ins.Interactiontxt.gameObject.SetActive(false);
        GameMng.Ins.hero.m_IInteraction = null;
    }

    public void OnClickQuestRefusal()
    {
        GameMng.Ins.hero.InteractionAction();
    }

    public void OnClickQuestRewardReceive()     
    {
        AddRewardItem();
        QuestMng.Ins.AddQuestList(GameMng.Ins.hero.characterData.m_eQuest);
        GameMng.Ins.hero.characterData.m_eQuest = (eQuest)(((int)GameMng.Ins.hero.characterData.m_eQuest / (int)eQuest.___QuestCategory___ + 1) * (int)eQuest.___QuestCategory___);

        QuestMng.Ins.QuestNPCActivate();
        QuestMng.Ins.QuestInfoClear();
        GameMng.Ins.hero.InteractionAction();
        UIMng.Ins.questList_ExpantionUI.QuestProgressUpdate();
        UIMng.Ins.questList_ReductionUI.QuestList_ReductionUIClear();
        UIMng.Ins.Interactiontxt.gameObject.SetActive(false);
        GameMng.Ins.hero.m_IInteraction = null;
    }

    void AddRewardItem()
    {
        string sRewardItems = GameMng.Ins.hero.characterData.m_eQuest.GetQuestTb().m_sRewardItems;

        sRewardItems.GetRewardItem(m_qItems);

        int nQCount = m_qItems.Count;

        for(int i = 0; i < nQCount; ++i)
        {
            UIMng.Ins.inventoryUI.InventoryUIAddItem(m_qItems.Dequeue());
        }
    }
}
