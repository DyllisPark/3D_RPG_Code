using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class ToolTable : Table<int, ToolData> { }

public class ToolData : TableBase<int>
{

    // TableBase
    public override int key => m_nId;
    public override eTable eTb => eTable.Tool;
    public override string sFileName => "Tool.json";

    // 데이터
    public int      m_nId       { get; private set; }
    public float    m_fXPos     { get; private set; }
    public float    m_fYPos     { get; private set; }
    public float    m_fZPos     { get; private set; }
    public float    m_fXRot     { get; private set; }
    public float    m_fYRot     { get; private set; }
    public float    m_fZRot     { get; private set; }
    public float    m_fScale    { get; private set; }
    public string   m_sItemName { get; private set; }
    public string   m_sItemInfo { get; private set; }

    public ToolData() { }

    public ToolData(string a_sId, string a_sXPos, string a_sYPos, string a_sZPos, string a_sXRot, string a_sYRot, string a_sZRot, string a_sScale, string a_sItemName,
        string a_sItemInfo)
    {
        SetData(a_sId, a_sXPos, a_sYPos, a_sZPos, a_sXRot, a_sYRot, a_sZRot, a_sScale, a_sItemName, a_sItemInfo);
    }

    public void SetData(string a_sId, string a_sXPos, string a_sYPos, string a_sZPos, string a_sXRot, string a_sYRot, string a_sZRot, string a_sScale, string a_sItemName,
        string a_sItemInfo)
    {
        m_nId           = int.Parse(a_sId);
        m_fXPos         = float.Parse(a_sXPos);
        m_fYPos         = float.Parse(a_sYPos);
        m_fZPos         = float.Parse(a_sZPos);
        m_fXRot         = float.Parse(a_sXRot);
        m_fYRot         = float.Parse(a_sYRot);
        m_fZRot         = float.Parse(a_sZRot);
        m_fScale        = float.Parse(a_sScale);
        m_sItemName     = a_sItemName;
        m_sItemInfo     = a_sItemInfo;
    }

    public ToolData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9]);
    }
}
