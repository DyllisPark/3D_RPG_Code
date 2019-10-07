using System.Collections;
using UnityEngine;
using Global_Define;


public class UI_EquipGem : MonoBehaviour
{
    #region INSPECTOR
    public UI_ItemSlot upgradeItemSlot;
    public UI_ItemSlot gemItemSlot;
    public UI_ItemSlot selectedGemSlot;
    #endregion

    public void SlotCheck(UI_ItemSlot firstItem, UI_ItemSlot secondItem)
    {
        if (firstItem.m_eItemType == eItemType.Gem && secondItem.m_eItemSlotType == eItemSlotType.GemItem)
        {
            if(upgradeItemSlot.m_eItemType == eItemType.None) { return; }

            SetSlotData(gemItemSlot, firstItem);
            selectedGemSlot = firstItem;
        }
        else if (firstItem.m_eItemType == eItemType.Weapon && firstItem.m_eItemSlotType == eItemSlotType.EquipItem && secondItem.m_eItemSlotType == eItemSlotType.UpgradeItem)
        {
            SetSlotData(upgradeItemSlot, firstItem);
        }
    }


    void SetSlotData(UI_ItemSlot settedSlot, UI_ItemSlot firstItem)
    {
        settedSlot.m_eItemID = firstItem.m_eItemID;
        settedSlot.m_eItemType = firstItem.m_eItemType;
        settedSlot.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(settedSlot.m_eItemID.ToDesc());
    }

    public void OnClickUpgrade()
    {
        if(upgradeItemSlot.m_eItemID != eItemID.None && gemItemSlot.m_eItemID != eItemID.None)
        {
            var weapon = GameMng.Ins.GetEquipWeapon();
            GameObject weaponEffect = weapon.Instantiate_asChild(string.Format(Path.GEMEFFECT_PATH, gemItemSlot.m_eItemID));
            var effectUpdater = weaponEffect.GetComponent<PSMeshRendererUpdater>();
            effectUpdater.UpdateMeshEffect(weapon);
            UIMng.Ins.itemInfoUI.WeaponGemUpdate(gemItemSlot.m_eItemID);
            UIMng.Ins.exceptiontxt.text = "강화 성공!";
            selectedGemSlot.Clear();
            SlotDataClear();
        }
        else
        {
            if(upgradeItemSlot.m_eItemID == eItemID.None)
            {
                UIMng.Ins.exceptiontxt.text = "강화할 무기를 선택해주세요.";
            }
            else if(gemItemSlot.m_eItemID == eItemID.None)
            {
                UIMng.Ins.exceptiontxt.text = "강화할 젬을 선택해주세요.";
            }
        }
        UIMng.Ins.exceptionUI.ActiveChange();
        StartCoroutine(DeactiveExceptionUI());
    }

    public void OnClickCancel()
    {
        SlotDataClear();
    }

    public void OnClickClose()
    {
        GameMng.Ins.hero.m_bAllActivateState = true;
        EquipGemUIOnOff();
    }

    public void EquipGemUIOnOff()
    {
        UIMng.Ins.equipGemUI.gameObject.ActiveChange();
        UIMng.Ins.inventoryUI.gameObject.ActiveChange();
        UIMng.Ins.characterStateUI.gameObject.ActiveChange();
    }

    void SlotDataClear()
    {
        upgradeItemSlot.Clear();
        gemItemSlot.Clear();
    }

    IEnumerator DeactiveExceptionUI()
    {
        yield return YieldReturnCaching.WaitForSeconds(0.5f);
        UIMng.Ins.exceptionUI.ActiveChange();
    }
    
    

}
