using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class MaterialItemTable : Table<int, MaterialItemData> { }

public class MaterialItemData : TableBase<int>
{

    // TableBase
    public override int key => m_nId;
    public override eTable eTb => eTable.MaterialItem;
    public override string sFileName => "MaterialItem.json";

    // 데이터
    public int      m_nId       { get; private set; }
    public string   m_sItemName { get; private set; }
    public string   m_sItemInfo { get; private set; }


    public MaterialItemData() { }

    public MaterialItemData(string a_sId, string a_sItemName, string a_sItemInfo)
    {
        SetData(a_sId, a_sItemName, a_sItemInfo);
    }

    public void SetData(string a_sId, string a_sItemName, string a_sItemInfo)
    {
        m_nId       = int.Parse(a_sId);
        m_sItemName = a_sItemName;
        m_sItemInfo = a_sItemInfo;
    }

    public MaterialItemData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2]);
    }
}
