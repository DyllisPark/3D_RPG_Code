using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class CustomizingTable : Table<eCustomizing, CustomizingData> { }

public class CustomizingData : TableBase<eCustomizing>
{

    // TableBase
    public override eCustomizing key => m_eId;
    public override eTable eTb => eTable.Customizing;
    public override string sFileName => "Customazing.json";

    // 데이터
    public eCustomizing m_eId { get; private set; }
    public int m_nPartsCount { get; private set; }

    public CustomizingData() { }

    public CustomizingData(string a_sId, string a_sPartCount)
    {
        SetData(a_sId, a_sPartCount);
    }

    public void SetData(string a_sId, string a_sPartCount)
    {
        m_eId = (eCustomizing)int.Parse(a_sId);
        m_nPartsCount = int.Parse(a_sPartCount);
    }

    public CustomizingData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1]);
    }
}
