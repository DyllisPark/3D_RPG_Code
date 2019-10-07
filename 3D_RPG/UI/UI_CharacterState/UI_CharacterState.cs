using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//인게임 캐릭터 상태창 UI
public class UI_CharacterState : MonoBehaviour
{
    #region INSPECTOR

    public Text playerNameText;
    public Text heroTypeText;
    public Text levelText;
    public Text statHPText;
    public Text statPowText;
    public Text statCriticalRateText;
    public Text statAttackSpeedText;
    public Text statMPText;
    public Text statDefText;
    public Text statAvoidRateText;
    public Text statMoveSpeedText;

    public UI_ItemSlot[] itemSlots;
    public Dictionary<eItemType, UI_ItemSlot> m_dicEquipItemSlots = new Dictionary<eItemType, UI_ItemSlot>();

    SelectedCharacterData   characterData;
    HeroStat                heroStat;

    #endregion
    
    private void OnEnable()
    {
        HeroStatUpdate();
    }

    public void CharacterStateInit()
    {
        for (int i = 0; i < itemSlots.Length; ++i)
        {
            m_dicEquipItemSlots.Add(itemSlots[i].m_eItemType, itemSlots[i]);
        }
        foreach (var equipItem in GameMng.Ins.hero.characterData.m_dicEquipItems)
        {
            m_dicEquipItemSlots[equipItem.Key].m_eItemID = equipItem.Value;
            m_dicEquipItemSlots[equipItem.Key].itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(equipItem.Value.ToString());
            if (equipItem.Key == eItemType.Weapon || equipItem.Key == eItemType.Shield)
            {
                ItemMng.Ins.EquipWeapon(equipItem.Value, GameMng.Ins.hero.weaponRoot);
                GameMng.Ins.hero.anim.SetInteger("WeaponState", (int)equipItem.Value / (int)eItemID.___WeaponCategory___);
            }
            else
            {
                ItemMng.Ins.EquipArmor(equipItem.Key, equipItem.Value);
            }
        }
    }

    public void HeroStatUpdate()
    {
        characterData   = GameMng.Ins.hero.characterData;
        heroStat        = GameMng.Ins.hero.characterData.nowStat;

        playerNameText.text         = characterData.m_sUserName;
        heroTypeText.text           = characterData.m_eHeroType.GetHeroTypeInfoTb().m_sHeroTypeName;
        levelText.text              = heroStat.m_nLevel.ToString();
        statHPText.text             = heroStat.m_nHp.ToString();
        statPowText.text            = heroStat.m_nPow.ToString();
        statCriticalRateText.text   = string.Format("{0:f1}%", heroStat.m_fCriticalRate);
        statAttackSpeedText.text    = string.Format("{0:f1}", heroStat.m_fAtkSpeed);
        statMPText.text             = heroStat.m_nMp.ToString();
        statDefText.text            = heroStat.m_nDef.ToString();
        statAvoidRateText.text      = string.Format("{0:f1}%", heroStat.m_fAvoidRate);
        statMoveSpeedText.text      = string.Format("{0:f1}", heroStat.m_fMoveSpeed);
    }
}
