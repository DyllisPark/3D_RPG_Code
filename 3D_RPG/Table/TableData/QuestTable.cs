using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class QuestTable : Table<eQuest, QuestData> { }

public class QuestData : TableBase<eQuest>
{
    // TableBase
    public override eQuest key => m_eId;
    public override eTable eTb => eTable.Quest;
    public override string sFileName => "Quest.json";

    // 데이터
    public eQuest               m_eId               { get; private set; }
    public string               m_sRewardItems      { get; private set; }
    public int                  m_nRewardItemCount  { get; private set; }
    public eQuestPurPoseType    m_eQuestPurposeType { get; private set; }
    public int                  m_nPurposeTbID      { get; private set; }
    public eQuestState          m_eQuestState       { get; private set; }
    public eNPCID               m_eNPCID            { get; private set; }
    public string               m_sQuestName        { get; private set; }
    public string               m_sQuestStory       { get; private set; }
    public string               m_sQuestExplanation { get; private set; }
    public eScene               m_eLocalSceneName   { get; private set; }

    public QuestData() { }

    public QuestData(string a_sId, string a_sRewardItems, string a_sRewardItemCount, string a_sQuestPurposeType, string a_sPurposeTbID, string a_sQuestState, string a_sNPCID,
        string a_sQuestName, string a_sQuestStory, string a_sQuestExplanation, string a_sLocalSceneName)
    {
        SetData(a_sId, a_sRewardItems, a_sRewardItemCount, a_sQuestPurposeType, a_sPurposeTbID, a_sQuestState, a_sNPCID, a_sQuestName, a_sQuestStory, a_sQuestExplanation, a_sLocalSceneName);
    }

    public void SetData(string a_sId, string a_sRewardItems, string a_sRewardItemCount, string a_sQuestPurposeType, string a_sPurposeTbID, string a_sQuestState, string a_sNPCID,
        string a_sQuestName, string a_sQuestStory, string a_sQuestExplanation, string a_sLocalSceneName)
    {
        m_eId                   = (eQuest)int.Parse(a_sId);
        m_sRewardItems          = a_sRewardItems;
        m_nRewardItemCount      = int.Parse(a_sRewardItemCount);
        m_eQuestPurposeType     = (eQuestPurPoseType)int.Parse(a_sQuestPurposeType);
        m_nPurposeTbID          = int.Parse(a_sPurposeTbID);
        m_eQuestState           = (eQuestState)int.Parse(a_sQuestState);
        m_eNPCID                = (eNPCID)int.Parse(a_sNPCID);
        m_sQuestName            = a_sQuestName;
        m_sQuestStory           = a_sQuestStory;
        m_sQuestExplanation     = a_sQuestExplanation;
        m_eLocalSceneName       = (eScene)int.Parse(a_sLocalSceneName);
    }

    public QuestData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9], s[10]);
    }
}
