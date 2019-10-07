using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class GemTable : Table<int, GemData> { }

public class GemData : TableBase<int>
{
    // TableBase
    public override int key => m_eId;
    public override eTable eTb => eTable.GemItem;
    public override string sFileName => "GemITem.json";

    // 데이터
    public int          m_eId       { get; private set; }
    public int          m_nPow      { get; private set; }
    public string       m_sItemName { get; private set; }
    public string       m_sItemInfo { get; private set; }


    public GemData() { }

    public GemData(string a_sId, string a_sPow, string a_sItemName, string a_sItemInfo)
    {
        SetData(a_sId, a_sPow, a_sItemName, a_sItemInfo);
    }

    public void SetData(string a_sId, string a_sPow, string a_sItemName, string a_sItemInfo)
    {
        m_eId       = int.Parse(a_sId);
        m_nPow      = int.Parse(a_sPow);
        m_sItemName = a_sItemName;
        m_sItemInfo = a_sItemInfo;
    }

    public GemData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3]);
    }
}
