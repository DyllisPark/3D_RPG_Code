using UnityEngine;
using UnityEngine.UI;
using Global_Define;
using UnityEngine.EventSystems;


//아이템이 표기될 공용 슬롯 UI (드랍 아이템의 슬롯, 인벤토리 아이템의 슬롯, 장착 아이템의 슬롯 등)
public class UI_ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region INSPECTOR

    public Image itemIconImg;

    public eItemID m_eItemID;
    public eItemSlotType m_eItemSlotType;
    public eItemType m_eItemType;
    public bool m_bSlotState = false;

    Vector3 originPos;

    #endregion

    public void ItemAdd(eItemID a_eItemID)
    {
        m_eItemID = a_eItemID;
        m_eItemType = a_eItemID.GetItemTb().m_eItemType;
        itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(m_eItemID.ToDesc()) as Sprite;
        m_bSlotState = true;
    }


    //아이템 최초 드래그 접근시 별도의 Temp 아이템 슬롯으로 아이템 이동을 표현.
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_eItemID != eItemID.None && m_eItemSlotType != eItemSlotType.QuestItem && m_eItemSlotType != eItemSlotType.DropItem)
        {
            originPos = transform.localPosition;
            ItemMng.Ins.firstItem = this;
            GetComponent<CanvasGroup>().blocksRaycasts = false;     //Drag할 Item Raycast를 통과시켜서 Swap할 Item의 OnPointerEnte가 원활하게 작동하게끔.

            Sprite temp = UIMng.Ins.tempDragItem.itemIconImg.sprite;
            UIMng.Ins.tempDragItem.itemIconImg.sprite = itemIconImg.sprite;
            itemIconImg.sprite = temp;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIMng.Ins.tempDragItem.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = originPos;
        if (ItemMng.Ins.secondItem != null && ItemMng.Ins.firstItem != null)
        {
            ItemMng.Ins.ItemSwapCheck();
            UIMng.Ins.equipGemUI.SlotCheck(ItemMng.Ins.firstItem, ItemMng.Ins.secondItem);
        }
        else if (ItemMng.Ins.secondItem == null && ItemMng.Ins.firstItem == null) return;
        else
        {
            ItemMng.Ins.firstItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(ItemMng.Ins.firstItem.m_eItemID.ToDesc()) as Sprite;
        }

        UIMng.Ins.tempDragItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite("None") as Sprite;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        ItemMng.Ins.firstItem = null;
        ItemMng.Ins.secondItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(m_eItemSlotType == eItemSlotType.DropItem)
        {
            UIMng.Ins.inventoryUI.InventoryUIAddItem(m_eItemID);
            for(int i = 0; i < UIMng.Ins.dropItemUI.m_liDropItems.Count; ++i)
            {
                if(UIMng.Ins.dropItemUI.m_liDropItems[i].dropItemSlot.m_eItemID == m_eItemID)
                {
                    UIMng.Ins.dropItemUI.m_liDropItems[i].gameObject.SetActive(false);
                }
            }
            UIMng.Ins.itemInfoUI.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ItemMng.Ins.firstItem != null)
            ItemMng.Ins.secondItem = this;
        else
        {
            if (m_eItemID != eItemID.None)
            {
                UIMng.Ins.itemInfoUI.ItemInfoEnabled(m_eItemID, m_eItemType);
                UIMng.Ins.itemInfoUI.gameObject.SetActive(true);
                UIMng.Ins.itemInfoUI.transform.position = new Vector3(transform.position.x + 10f, transform.position.y + 11f, 0);
                if(m_eItemType == eItemType.Weapon)
                {
                    UIMng.Ins.itemInfoUI.weaponGemUI.SetActive(true);
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemMng.Ins.secondItem = null;
        if(UIMng.Ins.itemInfoUI.weaponGemUI.activeSelf == true)
            UIMng.Ins.itemInfoUI.weaponGemUI.SetActive(false);
        UIMng.Ins.itemInfoUI.gameObject.SetActive(false);
    }

    public void Clear()
    {
        m_eItemID = eItemID.None;
        m_eItemType = eItemType.None;
        itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite("None") as Sprite;
    }


}
