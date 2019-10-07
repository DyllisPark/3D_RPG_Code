using UnityEngine;
using Global_Define;

//히어로 스탯 계산
public class HeroStatCalc
{
    Hero hero;

    public HeroStatCalc() { }

    public void ChangeStatCalculation(eItemID a_eItemID, eItemType a_eItemType)
    {
        hero = GameMng.Ins.hero;
        if(a_eItemType == eItemType.Weapon || a_eItemType == eItemType.Shield)
        {
            if (hero.characterData.m_dicEquipItems.ContainsKey(a_eItemType))
            {
                WeaponData existWeapnData = hero.characterData.m_dicEquipItems[a_eItemType].GetItemTb().m_nItemTbID.GetWeaponTb();
                hero.characterData.nowStat.m_nHp -= existWeapnData.m_nHp;
                hero.characterData.nowStat.m_nPow -= existWeapnData.m_nPow;
                hero.characterData.nowStat.m_fCriticalRate -= existWeapnData.m_fCriticalRate;
            }

            if (a_eItemID != eItemID.None)
            {
                WeaponData newWeaponData = a_eItemID.GetItemTb().m_nItemTbID.GetWeaponTb();
                hero.characterData.nowStat.m_nHp += newWeaponData.m_nHp;
                hero.characterData.nowStat.m_nPow += newWeaponData.m_nPow;
                hero.characterData.nowStat.m_fCriticalRate += newWeaponData.m_fCriticalRate;
                hero.characterData.nowStat.m_fAtkSpeed = newWeaponData.m_fAtkSpeed;
            }
            
        }        
        else
        {
            if (hero.characterData.m_dicEquipItems.ContainsKey(a_eItemType))
            {
                ArmorData existArmorData = hero.characterData.m_dicEquipItems[a_eItemType].GetItemTb().m_nItemTbID.GetArmorTb();
                hero.characterData.nowStat.m_nHp -= existArmorData.m_nHp;
                hero.characterData.nowStat.m_nMp -= existArmorData.m_nMp;
                hero.characterData.nowStat.m_nDef -= existArmorData.m_nDef;
                hero.characterData.nowStat.m_fCriticalRate -= existArmorData.m_fCriticalRate;
                hero.characterData.nowStat.m_fAvoidRate -= existArmorData.m_fAvoidRate;
            }

            if (a_eItemID != eItemID.None)
            {
                ArmorData newArmorData = a_eItemID.GetItemTb().m_nItemTbID.GetArmorTb();
                hero.characterData.nowStat.m_nHp += newArmorData.m_nHp;
                hero.characterData.nowStat.m_nMp += newArmorData.m_nMp;
                hero.characterData.nowStat.m_nDef += newArmorData.m_nDef;
                hero.characterData.nowStat.m_fCriticalRate += newArmorData.m_fCriticalRate;
                hero.characterData.nowStat.m_fAvoidRate += newArmorData.m_fAvoidRate;
            }
        }

        UIMng.Ins.characterStateUI.HeroStatUpdate();
        hero.m_fCurrentHp = hero.characterData.nowStat.m_nHp;
        hero.m_fCurrentMp = hero.characterData.nowStat.m_nMp;
    }
}
