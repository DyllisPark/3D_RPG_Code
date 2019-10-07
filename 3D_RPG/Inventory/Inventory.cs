using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//인벤토리 데이터만 저장.
public class Inventory 
{
    #region INSPECTOR

    public Dictionary<int, eItemID> m_dicInventoryItem = new Dictionary<int, eItemID>();
    public int m_nGold;

    #endregion
}
