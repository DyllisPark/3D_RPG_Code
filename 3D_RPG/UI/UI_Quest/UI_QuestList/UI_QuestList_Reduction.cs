using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//인게임 우측 중간지점에 축소된 퀘스트 리스트.
public class UI_QuestList_Reduction : MonoBehaviour
{
    #region INSPECTOR

    public Text questNametxt;
    public Text questExplanationtxt;
    public Text questPurposeCounttxt;

    QuestData questData;
    StringBuilder m_sQuestPurposeCount = new StringBuilder();
    #endregion

    public void QuestList_ReductionUIInit(eQuest a_eQuest)
    {
        questData = a_eQuest.GetQuestTb();
        questNametxt.text = string.Format("[진행]{0}", questData.m_sQuestName); 
        questExplanationtxt.text = questData.m_sQuestExplanation;
        QuestList_ReductionUIUpdate();
    }

    public void QuestList_ReductionUIUpdate()
    {
        m_sQuestPurposeCount.Remove(0, m_sQuestPurposeCount.Length);

        if (questData.m_eQuestPurposeType == eQuestPurPoseType.Gain)
        {
            m_sQuestPurposeCount.Append("- ");
            foreach (var purpose in QuestMng.Ins.m_dicCompleteQuestPurpose_Gain)
            {
                m_sQuestPurposeCount.Append(string.Format("{0} {1}/{2} ",
                    purpose.Key.GetItemTb().m_nItemTbID.GetMaterialITemTb().m_sItemName, QuestMng.Ins.m_dicNowQuestPurpose_Gain[purpose.Key], purpose.Value));
            }
        }
        else if (questData.m_eQuestPurposeType == eQuestPurPoseType.Kill)
        {
            m_sQuestPurposeCount.Append("- ");
            foreach (var purpose in QuestMng.Ins.m_dicCompleteQuestPurpose_Kill)
            {
                m_sQuestPurposeCount.Append(string.Format("{0} {1}/{2} ",
                    purpose.Key.GetMonsterTb().m_sMonsterName, QuestMng.Ins.m_dicNowQuestPurpose_Kill[purpose.Key], purpose.Value));
            }
        }
        else
        {
            m_sQuestPurposeCount.Remove(0, m_sQuestPurposeCount.Length);
        }
        questPurposeCounttxt.text = m_sQuestPurposeCount.ToString();
    }

    public void QuestList_ReductionUIClear()
    {
        questNametxt.text = "";
        questExplanationtxt.text = "";
        questPurposeCounttxt.text = "";
        m_sQuestPurposeCount.Remove(0, m_sQuestPurposeCount.Length);
    }
}
