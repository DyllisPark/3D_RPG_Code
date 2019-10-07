using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//인벤토리 UI
public class UI_Inventory : MonoBehaviour
{
    #region INSPECTOR

    public List<UI_ItemSlot> m_liSlots = new List<UI_ItemSlot>();
    public GameObject m_objSlotRoot;

    Inventory inventoryData;
    const int nINVENTORY_COUNT = 56;
    #endregion



    public void InventoryUIInit(Inventory inventory)
    {
        for (int i = 0; i < nINVENTORY_COUNT; ++i)
        {
            GameObject obj = m_objSlotRoot.Instantiate_asChild("UI/ItemSlot");
            m_liSlots.Add(obj.GetComponent<UI_ItemSlot>());
        }

        inventoryData = inventory;
        int nSlotIndex = 0;
        foreach (var itemID in inventoryData.m_dicInventoryItem)
        {
            SetItemUI(itemID.Key, itemID.Value);
            ++nSlotIndex;
        }
    }

    //아이템 추가시 인벤토리 갱신, 캐릭터 고유의 인벤토리 데이터 갱신.
    public void InventoryUIAddItem(eItemID a_eItemID)       
    {
        for (int i = 0; i < m_liSlots.Count; ++i)
        {
            if (m_liSlots[i].m_bSlotState == false)
            {
                m_liSlots[i].ItemAdd(a_eItemID);
                if(QuestMng.Ins.m_dicNowQuestPurpose_Gain.ContainsKey(a_eItemID) == true)
                {
                    ++QuestMng.Ins.m_dicNowQuestPurpose_Gain[a_eItemID];
                    UIMng.Ins.questList_ReductionUI.QuestList_ReductionUIUpdate();
                }

                if (inventoryData.m_dicInventoryItem.ContainsKey(i))
                {
                    inventoryData.m_dicInventoryItem[i] = a_eItemID;
                    return;
                }
                inventoryData.m_dicInventoryItem.Add(i, a_eItemID);
                return;
            }
        }
    }

    public void InventoryDataUpdate()
    {
        for (int i = 0; i < m_liSlots.Count; ++i)
        {
            if (m_liSlots[i].m_eItemID == eItemID.None) continue;
            inventoryData.m_dicInventoryItem[i] = m_liSlots[i].m_eItemID;
        }
    }

    void SetItemUI(int a_nSlotIndex, eItemID a_eItemID)
    {
        m_liSlots[a_nSlotIndex].ItemAdd(a_eItemID);
    }
}


