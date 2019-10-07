using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class ItemTable : Table<eItemID, ItemData> { }

public class ItemData : TableBase<eItemID>
{

    // TableBase
    public override eItemID key => m_eId;
    public override eTable eTb => eTable.Item;
    public override string sFileName => "Item.json";

    // 데이터
    public eItemID      m_eId       { get; private set; }
    public int          m_nItemTbID   { get; private set; }
    public eItemType    m_eItemType { get; private set; }


    public ItemData() { }

    public ItemData(string a_sId, string a_sItemTbID, string a_sItemType)
    {
        SetData(a_sId, a_sItemTbID, a_sItemType);
    }

    public void SetData(string a_sId, string a_sItemTbID, string a_sItemType)
    {
        m_eId           = (eItemID)int.Parse(a_sId);
        m_nItemTbID     = int.Parse(a_sItemTbID);
        m_eItemType     = (eItemType)int.Parse(a_sItemType);
    }

    public ItemData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2]);
    }
}
