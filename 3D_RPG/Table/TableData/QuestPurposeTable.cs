using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class QuestPurposeTable : Table<int, QuestPurposeData> { }

public class QuestPurposeData : TableBase<int>
{
    // TableBase
    public override int key => m_nId;
    public override eTable eTb => eTable.QuestPurpose;
    public override string sFileName => "QuestPurpose.json";

    // 데이터
    public int                  m_nId                   { get; private set; }
    public eQuestPurPoseType    m_eQuestPurposeType     { get; private set; }
    public string               m_sPurposeItems         { get; private set; }
    public string               m_sPurposeItemCount     { get; private set; }
    public string               m_sPurposeMonsters      { get; private set; }
    public string               m_sPurposeMonsterCount  { get; private set; }
    public int                  m_nPurposeCount         { get; private set; }

    public QuestPurposeData() { }

    public QuestPurposeData(string a_sId, string a_sQuestPurposeType, string a_sPurposeItems, string a_sPurposeItemCount, string a_sPurposeMonsters, string a_sPurposeMonsterCount, string a_sPurposeCount)
    {
        SetData(a_sId, a_sQuestPurposeType, a_sPurposeItems, a_sPurposeItemCount, a_sPurposeMonsters, a_sPurposeMonsterCount, a_sPurposeCount);
    }

    public void SetData(string a_sId, string a_sQuestPurposeType, string a_sPurposeItems, string a_sPurposeItemCount, string a_sPurposeMonsters, string a_sPurposeMonsterCount, string a_sPurposeCount)
    {
        m_nId                   = int.Parse(a_sId);
        m_eQuestPurposeType     = (eQuestPurPoseType)int.Parse(a_sQuestPurposeType);
        m_sPurposeItems         = a_sPurposeItems;
        m_sPurposeItemCount     = a_sPurposeItemCount;
        m_sPurposeMonsters      = a_sPurposeMonsters;
        m_sPurposeMonsterCount  = a_sPurposeMonsterCount;
        m_nPurposeCount         = int.Parse(a_sPurposeCount);
    }

    public QuestPurposeData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6]);
    }
}
