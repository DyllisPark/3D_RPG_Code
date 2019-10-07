using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//퀘스트 리스트 확장된 사이즈 UI
public class UI_QuestList_Expantion : MonoBehaviour
{
    #region INSPECTOR
    public GameObject local_marianopleRoot;
    public GameObject local_whiteForestRoot;
    public GameObject dungeon_kroloalCradleRoot;
    public Text questName;
    public Text questStroy;
    public Text questExplanation;
    public GameObject giveUpBtn;
    public List<UI_ItemSlot> m_liRewardItem = new List<UI_ItemSlot>();
    

    QuestData questData;
    Queue<eItemID> m_qItems = new Queue<eItemID>();
    List<QuestBtn> m_liQuestList = new List<QuestBtn>();
    #endregion

    public void AddQuest(eQuest a_eQuest)
    {
        questData = a_eQuest.GetQuestTb();
        GameObject quest = null;
        switch(questData.m_eLocalSceneName)
        {
            case eScene.Local_MarianopleScene:
                quest = local_marianopleRoot.Instantiate_asChild(Path.QUEST_PATH);
                break;
            case eScene.Local_WhiteForestScene:
                quest = local_whiteForestRoot.Instantiate_asChild(Path.QUEST_PATH);
                break;
            case eScene.Dungeon_KroloalCradleScene:
                quest = dungeon_kroloalCradleRoot.Instantiate_asChild(Path.QUEST_PATH);
                break;
            default: Debug.LogError("Non Local Error"); break;
        }
        if(quest != null)
        {
            var questBtn = quest.GetComponent<QuestBtn>();
            m_liQuestList.Add(questBtn);
            questBtn.GetComponent<Button>().onClick.AddListener(() => OnClickQuest(questBtn));
            questBtn.m_eQuest = a_eQuest;
            questBtn.questNametxt.text = questData.m_sQuestName;
        }
        QuestProgressUpdate();
    }

    public void QuestProgressUpdate()
    {
        foreach (var liQuest in m_liQuestList)
        {
            var data = liQuest.m_eQuest.GetQuestTb();
            if (GameMng.Ins.hero.characterData.m_eQuest != liQuest.m_eQuest)
            {
                liQuest.questNametxt.text = string.Format("[완료]{0}", data.m_sQuestName);
            }
            else
            {
                liQuest.questNametxt.text = string.Format("[진행]{0}", data.m_sQuestName);
            }
        }
    }


    public void OnClickQuest(QuestBtn a_questBtn)
    {
        questData = a_questBtn.m_eQuest.GetQuestTb();
        questName.text = questData.m_sQuestName;
        questStroy.text = questData.m_sQuestStory;
        questExplanation.text = questData.m_sQuestExplanation;
        questData.m_sRewardItems.GetRewardItem(m_qItems);
        
        for (int i = 0; i < m_liRewardItem.Count; ++i)
        {
            if (i < questData.m_nRewardItemCount)
            {
                m_liRewardItem[i].ItemAdd(m_qItems.Dequeue());
            }
            else
            {
                m_liRewardItem[i].itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(eItemID.None.ToDesc());
            }
        }

        if (a_questBtn.m_eQuest == GameMng.Ins.hero.characterData.m_eQuest)
        {
            giveUpBtn.gameObject.SetActive(true);
        }
        else
        {
            giveUpBtn.gameObject.SetActive(false);
        }

    }
}
