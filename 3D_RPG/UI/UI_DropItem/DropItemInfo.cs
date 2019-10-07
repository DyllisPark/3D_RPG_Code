using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//몬스터 처치, 생활 컨텐츠 시에 드랍되는 아이템 정보 세팅
public class DropItemInfo : MonoBehaviour
{
    #region INSPECTOR
    public UI_ItemSlot dropItemSlot;
    public Text itemNametxt;

    #endregion

    public void SetDropItem(eItemID a_eItemID)
    {
        dropItemSlot.m_eItemID = a_eItemID;
        dropItemSlot.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(a_eItemID.ToString());
        dropItemSlot.m_eItemType = a_eItemID.GetItemTb().m_eItemType;

        if (dropItemSlot.m_eItemType == eItemType.None) { Debug.LogError("Non ItemType Error"); return; }

        if (dropItemSlot.m_eItemType == eItemType.Weapon || dropItemSlot.m_eItemType == eItemType.Shield)
        {
            itemNametxt.text = a_eItemID.GetItemTb().m_nItemTbID.GetWeaponTb().m_sItemName;
        }
        else if (dropItemSlot.m_eItemType == eItemType.Material)
        {
            //재료아이템 테이블.
            itemNametxt.text = a_eItemID.GetItemTb().m_nItemTbID.GetMaterialITemTb().m_sItemName;
        }
        else if (dropItemSlot.m_eItemType == eItemType.Gem)
        {
            //Gem 테이블
            itemNametxt.text = a_eItemID.GetItemTb().m_nItemTbID.GetGemItemTb().m_sItemName;
        }
        else
        {
            itemNametxt.text = a_eItemID.GetItemTb().m_nItemTbID.GetArmorTb().m_sItemName;
        }
    }

    public void Clear()
    {
        dropItemSlot.Clear();
        itemNametxt.text = "";
    }

}
