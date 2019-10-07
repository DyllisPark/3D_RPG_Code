using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//아이템의 이름, 스탯, 설명 등의 정보 표기UI
public class UI_ItemInfo : MonoBehaviour
{
    #region INSPECTOR

    public Image itemImg;
    public Text itemNametxt;
    public Text[] itemInfoNametxt;
    public Text[] itemInfoValuetxt;
    public GameObject weaponGemUI;
    public Image weaponGemImg;

    #endregion

    public void ItemInfoEnabled(eItemID a_eItemID, eItemType a_eItemType)
    {
        ItemInfoClear();
        int nTextIndex = -1;
        itemImg.sprite = UIMng.Ins.itemAtlas.GetSprite(a_eItemID.ToString());
        if (a_eItemType == eItemType.Weapon)
        {
            WeaponData weaponData = a_eItemID.GetItemTb().m_nItemTbID.GetWeaponTb();
            itemNametxt.text = weaponData.m_sItemName;
            while(nTextIndex < itemInfoNametxt.Length - 2)  //배열의 Index를 맞춰주기 위한 Number
            {
                ++nTextIndex;
                switch (nTextIndex)
                {
                    case 0:
                        itemInfoNametxt[nTextIndex].text = eItemStatName.Hp.ToDesc();
                        itemInfoValuetxt[nTextIndex].text = weaponData.m_nHp.ToString();
                        break;
                    case 1:
                        itemInfoNametxt[nTextIndex].text = eItemStatName.Pow.ToDesc();
                        itemInfoValuetxt[nTextIndex].text = weaponData.m_nPow.ToString();
                        break;
                    case 2:
                        itemInfoNametxt[nTextIndex].text = eItemStatName.AtkSpeed.ToDesc();
                        itemInfoValuetxt[nTextIndex].text = weaponData.m_fAtkSpeed.ToString();
                        break;
                    case 3:
                        itemInfoNametxt[nTextIndex].text = eItemStatName.CriticalRate.ToDesc();
                        itemInfoValuetxt[nTextIndex].text = weaponData.m_fCriticalRate.ToString();
                        break;
                    default: Debug.LogError("arg error"); break;
                }
               
            }
        }
        else if(a_eItemType == eItemType.Material)
        {
            var materialItemData = a_eItemID.GetItemTb().m_nItemTbID.GetMaterialITemTb();
            itemNametxt.text = materialItemData.m_sItemName;
            itemInfoNametxt[0].text = materialItemData.m_sItemInfo;
            itemInfoValuetxt[0].text = "";
        }
        else if(a_eItemType == eItemType.Life)
        {
            var lifeItemData = a_eItemID.GetItemTb().m_nItemTbID.GetToolTb();
            itemNametxt.text = lifeItemData.m_sItemName;
            itemInfoNametxt[0].text = lifeItemData.m_sItemInfo;
            itemInfoValuetxt[0].text = "";
        }
        else if(a_eItemType == eItemType.Gem)
        {
            var gemItemData = a_eItemID.GetItemTb().m_nItemTbID.GetGemItemTb();
            itemNametxt.text = gemItemData.m_sItemName;
            itemInfoNametxt[0].text = eItemStatName.Pow.ToDesc();
            itemInfoValuetxt[0].text = gemItemData.m_nPow.ToString();
            itemInfoNametxt[1].text = gemItemData.m_sItemInfo;
            itemInfoValuetxt[1].text = "";
        }
        else
        {
            ArmorData armorData = a_eItemID.GetItemTb().m_nItemTbID.GetArmorTb();
            itemNametxt.text = armorData.m_sItemName;
            int nWriteIndex = 0;
            while (nTextIndex < itemInfoNametxt.Length - 1) //배열의 Index를 맞춰주기 위한 Number
            {
                ++nTextIndex;
                
                switch (nTextIndex)
                {
                    case 0:
                        if (armorData.m_nHp == 0) continue;
                        itemInfoNametxt[nWriteIndex].text = eItemStatName.Hp.ToDesc();
                        itemInfoValuetxt[nWriteIndex].text = armorData.m_nHp.ToString();
                        break;
                    case 1:
                        if (armorData.m_nMp == 0) continue;
                        itemInfoNametxt[nWriteIndex].text = eItemStatName.Mp.ToDesc();
                        itemInfoValuetxt[nWriteIndex].text = armorData.m_nMp.ToString();
                        break;
                    case 2:
                        if (armorData.m_nDef == 0) continue;
                        itemInfoNametxt[nWriteIndex].text = eItemStatName.Def.ToDesc();
                        itemInfoValuetxt[nWriteIndex].text = armorData.m_nDef.ToString();
                        break;
                    case 3:
                        if (armorData.m_fCriticalRate == 0) continue;
                        itemInfoNametxt[nWriteIndex].text = eItemStatName.CriticalRate.ToDesc();
                        itemInfoValuetxt[nWriteIndex].text = armorData.m_fCriticalRate.ToString();
                        break;
                    case 4:
                        if (armorData.m_fAvoidRate == 0) continue;
                        itemInfoNametxt[nWriteIndex].text = eItemStatName.AvoidRate.ToDesc();
                        itemInfoValuetxt[nWriteIndex].text = armorData.m_fAvoidRate.ToString();
                        break;
                    default: Debug.LogError("arg error"); break;
                }
                ++nWriteIndex;
            }
        }
    }

    public void WeaponGemUpdate(eItemID a_eGemID)
    {
        weaponGemImg.sprite = UIMng.Ins.itemAtlas.GetSprite(a_eGemID.ToDesc()) as Sprite;
    }

    void ItemInfoClear()
    {
        for(int i = 0; i < itemInfoNametxt.Length; ++i)
        {
            itemInfoNametxt[i].text = "";
            itemInfoValuetxt[i].text = "";
        }
    }
}
