using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//아이템 장착 및 해제, 스왑 관리.
public class ItemMng : MonoBehaviour
{
    #region SINGLETON
    static ItemMng _instance = null;

    public static ItemMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(ItemMng)) as ItemMng;
                if (_instance == null)
                {
                    _instance = new GameObject("ItemMng", typeof(ItemMng)).GetComponent<ItemMng>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion

    #region INSPECTOR
    public Dictionary<eItemID, GameObject> m_dicWeaponPool = new Dictionary<eItemID, GameObject>();
    public Dictionary<eItemID, GameObject> m_dicToolPool = new Dictionary<eItemID, GameObject>();
    

    [HideInInspector]
    public UI_ItemSlot firstItem;
    [HideInInspector]
    public UI_ItemSlot secondItem;
    

    WeaponData weaponData;

    #endregion

    public void ItemSwapCheck()
    {
        if (firstItem.m_eItemSlotType == eItemSlotType.EquipItem || secondItem.m_eItemSlotType == eItemSlotType.EquipItem)
        {
            if (firstItem.m_eItemSlotType == eItemSlotType.EquipItem && secondItem.m_eItemSlotType == eItemSlotType.EquipItem)
            {
                firstItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(firstItem.m_eItemID.ToDesc()) as Sprite;
                return;
            }

            if (firstItem.m_eItemSlotType == eItemSlotType.EquipItem)       //장비 장착UI에서 아이템을 Drag할 때
            {
                if (firstItem.m_eItemType == secondItem.m_eItemType)
                {
                    //아이템스왑
                    if (firstItem.m_eItemType == eItemType.Weapon)
                    {
                        ClearWeapon(GameMng.Ins.hero.weaponRoot.transform.GetChild(0).gameObject);
                        EquipWeapon(secondItem.m_eItemID, GameMng.Ins.hero.weaponRoot);
                        GameMng.Ins.hero.anim.SetInteger("WeaponState", (int)secondItem.m_eItemID / (int)eItemID.___WeaponCategory___);
                        //Debug.Log("무기스왑");
                    }
                    else if (firstItem.m_eItemType == eItemType.Shield)
                    {
                        ClearWeapon(GameMng.Ins.hero.shieldRoot.transform.GetChild(0).gameObject);
                        EquipWeapon(secondItem.m_eItemID, GameMng.Ins.hero.shieldRoot);
                        //Debug.Log("방패스왑");
                    }
                    else
                    {
                        EquipArmor(secondItem.m_eItemType, secondItem.m_eItemID);
                        //Debug.Log("방어구스왑");
                    }
                    GameMng.Ins.heroStatCalc.ChangeStatCalculation(secondItem.m_eItemID, secondItem.m_eItemType);
                    EquipItemDataUpdate(secondItem, firstItem);
                    ItemDataSwap();
                }
                else if (secondItem.m_eItemID == eItemID.None && secondItem.m_eItemSlotType != eItemSlotType.GemItem && secondItem.m_eItemSlotType != eItemSlotType.UpgradeItem)
                {
                    //아이템해제
                    if (firstItem.m_eItemType == eItemType.Weapon)
                    {
                        ClearWeapon(GameMng.Ins.hero.weaponRoot.transform.GetChild(0).gameObject);
                        GameMng.Ins.hero.anim.SetInteger("WeaponState", (int)eItemID.None);
                        //Debug.Log("무기해제");
                    }
                    else if (firstItem.m_eItemType == eItemType.Shield)
                    {
                        ClearWeapon(GameMng.Ins.hero.shieldRoot.transform.GetChild(0).gameObject);
                        //Debug.Log("방패해제");
                    }
                    else
                    {
                        ClearArmor(firstItem.m_eItemType, firstItem.m_eItemID);
                        //Debug.Log("방어구해제");
                    }
                    GameMng.Ins.heroStatCalc.ChangeStatCalculation(eItemID.None, firstItem.m_eItemType);
                    EquipItemDataUpdate(secondItem, firstItem);
                    ItemDataSwap();
                }
                else
                {
                    firstItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(firstItem.m_eItemID.ToDesc()) as Sprite;
                }
            }
            else                                                        //인벤토리UI에서 아이템을 Drag할 때
            {
                if (firstItem.m_eItemType == secondItem.m_eItemType)
                {
                    if(secondItem.m_eItemID != eItemID.None)
                    {
                        //아이템스왑
                        if (secondItem.m_eItemType == eItemType.Weapon)
                        {
                            ClearWeapon(GameMng.Ins.hero.weaponRoot.transform.GetChild(0).gameObject);
                            EquipWeapon(firstItem.m_eItemID, GameMng.Ins.hero.weaponRoot);
                            GameMng.Ins.hero.anim.SetInteger("WeaponState", (int)firstItem.m_eItemID / (int)eItemID.___WeaponCategory___);
                            //Debug.Log("무기스왑");
                        }
                        else if (secondItem.m_eItemType == eItemType.Shield)
                        {
                            ClearWeapon(GameMng.Ins.hero.shieldRoot.transform.GetChild(0).gameObject);
                            EquipWeapon(firstItem.m_eItemID, GameMng.Ins.hero.shieldRoot);
                            //Debug.Log("방패스왑");
                        }
                        else
                        {
                            EquipArmor(firstItem.m_eItemType, firstItem.m_eItemID);
                            //Debug.Log("방어구스왑");
                        }
                    }
                    else
                    {
                        //아이템장착
                        if (secondItem.m_eItemType == eItemType.Weapon)
                        {
                            EquipWeapon(firstItem.m_eItemID, GameMng.Ins.hero.weaponRoot);
                            GameMng.Ins.hero.anim.SetInteger("WeaponState", (int)firstItem.m_eItemID / (int)eItemID.___WeaponCategory___);
                            //Debug.Log("무기장착");
                        }
                        else if (secondItem.m_eItemType == eItemType.Shield)
                        {
                            EquipWeapon(firstItem.m_eItemID, GameMng.Ins.hero.shieldRoot);
                            //Debug.Log("방패장착");
                        }
                        else
                        {
                            EquipArmor(firstItem.m_eItemType, firstItem.m_eItemID);
                            //Debug.Log("방어구장착");
                        }
                    }
                    
                    GameMng.Ins.heroStatCalc.ChangeStatCalculation(firstItem.m_eItemID, firstItem.m_eItemType);
                    EquipItemDataUpdate(firstItem, secondItem);
                    ItemDataSwap();
                }
                else
                    firstItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(firstItem.m_eItemID.ToDesc()) as Sprite;
              
            }

        }
        else if (firstItem.m_eItemSlotType == eItemSlotType.InventoryItem && secondItem.m_eItemSlotType == eItemSlotType.InventoryItem)
        {
            ItemDataSwap();
            //인벤토리만 교체
        }
        
    }

    void EquipWeaponOrShield(GameObject a_objRoot)
    {
        eItemID eWeaponID = firstItem.m_eItemID;
        WeaponData data = eWeaponID.GetItemTb().m_nItemTbID.GetWeaponTb();

        string path = string.Format(Path.WEAPON_PATH, eWeaponID);
        GameObject weapon = a_objRoot.Instantiate_asChild(path);

        weapon.transform.localPosition = new Vector3(data.m_fXPos, data.m_fYPos, data.m_fZPos);
        weapon.transform.localRotation = Quaternion.Euler(data.m_fXRot, data.m_fYRot, data.m_fZRot);
        weapon.transform.localScale = Vector3.one * data.m_fScale;
    }



    //아이템의 정보만 교환
    void ItemDataSwap()
    {
        int nFirtItemIndex = -1;
        int nSecondItemIndex = -1;
        var dicInvenItem = GameMng.Ins.hero.characterData.inventory.m_dicInventoryItem;

        //인벤토리 아이템슬롯 리스트에서 현재 선택된 두 개의 아이템의 인덱스를 구함.
        for (int i = 0; i < UIMng.Ins.inventoryUI.m_liSlots.Count; ++i)
        {
            if (UIMng.Ins.inventoryUI.m_liSlots[i] == firstItem)
            {
                nFirtItemIndex = i;
            }
            else if (UIMng.Ins.inventoryUI.m_liSlots[i] == secondItem)
            {
                nSecondItemIndex = i;
            }
        }

        //장착슬롯에서 인벤토리로 아이템을 이동했을 때 eItemID 데이터 교환 - 빈 슬롯으로 아이템을 내려놓는 경우도 있음.
        if (firstItem.m_eItemSlotType == eItemSlotType.EquipItem)
        {
            dicInvenItem[nSecondItemIndex] = firstItem.m_eItemID;
        }
        else if (secondItem.m_eItemSlotType == eItemSlotType.EquipItem)
        {
            if (secondItem.m_eItemID != eItemID.None)
            {
                dicInvenItem[nFirtItemIndex] = secondItem.m_eItemID;
            }
            else
                dicInvenItem.Remove(nFirtItemIndex);
        }
        //인벤토리에서 장착슬롯으로 아이템을 이동했을 때, 이미 장착중인 아이템이 있으면 장착 아이템의 Data(고유 Data)를 교환해줌. 아닐 경우 추가.
        else
        {
            if (firstItem.m_eItemID != eItemID.None && secondItem.m_eItemID != eItemID.None)
            {
                var eTempItemID = dicInvenItem[nFirtItemIndex];
                dicInvenItem[nFirtItemIndex] = dicInvenItem[nSecondItemIndex];
                dicInvenItem[nSecondItemIndex] = eTempItemID;
            }
            else if (firstItem.m_eItemID != eItemID.None && secondItem.m_eItemID == eItemID.None)
            {
                dicInvenItem.Add(nSecondItemIndex, firstItem.m_eItemID);
                dicInvenItem.Remove(nFirtItemIndex);
            }
            else if (secondItem.m_eItemID != eItemID.None && firstItem.m_eItemID == eItemID.None)
            {
                dicInvenItem.Add(nFirtItemIndex, secondItem.m_eItemID);
                dicInvenItem.Remove(nSecondItemIndex);
            }
        }

        eItemID eTemp = firstItem.m_eItemID;
        firstItem.m_eItemID = secondItem.m_eItemID;
        secondItem.m_eItemID = eTemp;

        if (firstItem.m_eItemSlotType != eItemSlotType.EquipItem)
            firstItem.m_eItemType = firstItem.m_eItemID.GetItemTb().m_eItemType;
        secondItem.m_eItemType = secondItem.m_eItemID.GetItemTb().m_eItemType;

        bool bTemp = firstItem.m_bSlotState;
        firstItem.m_bSlotState = secondItem.m_bSlotState;
        secondItem.m_bSlotState = bTemp;

        ImageUpdate(firstItem, secondItem);
    }

    void EquipItemDataUpdate(UI_ItemSlot changeItem, UI_ItemSlot existingItem)
    {
        if (changeItem.m_eItemID != eItemID.None)
        {
            if (UIMng.Ins.characterStateUI.m_dicEquipItemSlots.ContainsKey(changeItem.m_eItemType))
            {
                UIMng.Ins.characterStateUI.m_dicEquipItemSlots[changeItem.m_eItemType] = changeItem;
                GameMng.Ins.hero.characterData.m_dicEquipItems[changeItem.m_eItemType] = changeItem.m_eItemID;
            }
            else
            {
                UIMng.Ins.characterStateUI.m_dicEquipItemSlots.Add(changeItem.m_eItemType, changeItem);
                GameMng.Ins.hero.characterData.m_dicEquipItems.Add(changeItem.m_eItemType, changeItem.m_eItemID);
            }
        }
        else
        {
            UIMng.Ins.characterStateUI.m_dicEquipItemSlots.Remove(existingItem.m_eItemType);
            GameMng.Ins.hero.characterData.m_dicEquipItems.Remove(existingItem.m_eItemType);
        }

    }

    void ImageUpdate(UI_ItemSlot a_firstItem, UI_ItemSlot a_secondItem)
    {
        a_firstItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(a_firstItem.m_eItemID.ToDesc()) as Sprite;
        a_secondItem.itemIconImg.sprite = UIMng.Ins.itemAtlas.GetSprite(a_secondItem.m_eItemID.ToDesc()) as Sprite;
    }


    public void EquipWeapon(eItemID a_eItemID, GameObject a_objRoot)
    {
        weaponData = a_eItemID.GetItemTb().m_nItemTbID.GetWeaponTb();
        
        if (m_dicWeaponPool.ContainsKey(a_eItemID))
        {
            m_dicWeaponPool[a_eItemID].transform.parent = a_objRoot.transform;
            m_dicWeaponPool[a_eItemID].SetActive(true);
        }
        else
        {
            string path = string.Format(Path.WEAPON_PATH, a_eItemID);
            GameObject weapon = a_objRoot.Instantiate_asChild(path);
            m_dicWeaponPool.Add(a_eItemID, weapon);
        }

        m_dicWeaponPool[a_eItemID].transform.localPosition = new Vector3(weaponData.m_fXPos, weaponData.m_fYPos, weaponData.m_fZPos);
        m_dicWeaponPool[a_eItemID].transform.localRotation = Quaternion.Euler(weaponData.m_fXRot, weaponData.m_fYRot, weaponData.m_fZRot);
        m_dicWeaponPool[a_eItemID].transform.localScale = Vector3.one * weaponData.m_fScale;

        if(a_eItemID != eItemID.Ax && a_eItemID != eItemID.FishingPole && a_eItemID != eItemID.Sickle)
            GameMng.Ins.hero.weaponTrailRenderer = m_dicWeaponPool[a_eItemID].transform.GetChild(0).GetComponent<TrailRenderer>();
        m_dicWeaponPool[a_eItemID].SetActive(true);
        
    }

    public void ClearWeapon(GameObject a_objWeapon)
    {
        a_objWeapon.SetActive(false);
        a_objWeapon.transform.parent = transform;
    }

    //1개의 ID로 여러개의 오브젝트Mesh를 바꿔줘야하는 경우가 있기 때문에 분류.
    public void EquipArmor(eItemType a_eItemType, eItemID a_eItemID)
    {
        var equipParts = GameMng.Ins.hero.myCharacterParts.m_arrEquipParts;
        var armorData = a_eItemID.GetItemTb().m_nItemTbID.GetArmorTb();
        string path = "";
        string sArmorTypeNumber = "";

        switch(a_eItemType)
        {
            case eItemType.Helmet:
            case eItemType.Top:
            case eItemType.HelmetDeco:
            case eItemType.Bottom:
            case eItemType.Cape:
                path = string.Format(Path.ONEPARTS_ARMOR_PATH, a_eItemType, a_eItemID);
                equipParts[(int)armorData.m_eArmorType].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType].gameObject.SetActive(true);
                break;
            case eItemType.Elbow:
            case eItemType.Shoulder:
            case eItemType.Leg:
            case eItemType.Knee:
                sArmorTypeNumber = a_eItemID.ToString().Substring(a_eItemID.ToString().Length - 1, 1);
                path = string.Format(Path.MULTIPARTS_ARMOR_PATH, a_eItemType, a_eItemType, "Left", sArmorTypeNumber);
                equipParts[(int)armorData.m_eArmorType].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType].gameObject.SetActive(true);
                path = string.Format(Path.MULTIPARTS_ARMOR_PATH, a_eItemType, a_eItemType, "Right", sArmorTypeNumber);
                equipParts[(int)armorData.m_eArmorType + 1].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType + 1].gameObject.SetActive(true);
                break;
            case eItemType.Arm:
                sArmorTypeNumber = a_eItemID.ToString().Substring(a_eItemID.ToString().Length - 1, 1);
                path = string.Format(Path.MULTIPARTS_ARMOR_PATH, a_eItemType, a_eItemType, "UpperLeft", sArmorTypeNumber);
                equipParts[(int)armorData.m_eArmorType].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType].gameObject.SetActive(true);
                path = string.Format(Path.MULTIPARTS_ARMOR_PATH, a_eItemType, a_eItemType, "UpperRight", sArmorTypeNumber);
                equipParts[(int)armorData.m_eArmorType + 1].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType + 1].gameObject.SetActive(true);
                path = string.Format(Path.MULTIPARTS_ARMOR_PATH, a_eItemType, a_eItemType, "LowerLeft", sArmorTypeNumber);
                equipParts[(int)armorData.m_eArmorType + 2].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType + 2].gameObject.SetActive(true);
                path = string.Format(Path.MULTIPARTS_ARMOR_PATH, a_eItemType, a_eItemType, "LowerRight", sArmorTypeNumber);
                equipParts[(int)armorData.m_eArmorType + 3].sharedMesh = Resources.Load(path) as Mesh;
                equipParts[(int)armorData.m_eArmorType + 3].gameObject.SetActive(true);
                break;
            default: Debug.LogError("Wrong Type"); break;
        }
    }

    //커스터마이징으로 얻은 맨몸 위에 아이템 장착용 오브젝트가 따로 있기 때문에 아이템 해제시 오브젝트만 비활성화.
    public void ClearArmor(eItemType a_eItemType, eItemID a_eItemID)
    {
        var equipParts = GameMng.Ins.hero.myCharacterParts.m_arrEquipParts;
        var armorData = a_eItemID.GetItemTb().m_nItemTbID.GetArmorTb();
        switch (a_eItemType)
        {
            case eItemType.Helmet:
            case eItemType.Top:
            case eItemType.HelmetDeco:
            case eItemType.Bottom:
            case eItemType.Cape:
                equipParts[(int)armorData.m_eArmorType].gameObject.SetActive(false);
                break;
            case eItemType.Elbow:
            case eItemType.Shoulder:
            case eItemType.Leg:
            case eItemType.Knee:
                equipParts[(int)armorData.m_eArmorType].gameObject.SetActive(false);
                equipParts[(int)armorData.m_eArmorType + 1].gameObject.SetActive(false);
                break;
            case eItemType.Arm:
                equipParts[(int)armorData.m_eArmorType].gameObject.SetActive(false);
                equipParts[(int)armorData.m_eArmorType + 1].gameObject.SetActive(false);
                equipParts[(int)armorData.m_eArmorType + 2].gameObject.SetActive(false);
                equipParts[(int)armorData.m_eArmorType + 3].gameObject.SetActive(false);
                break;
            default: Debug.LogError("Wrong Type"); break;
        }
    }

    //수확, 낚시, 벌목등 생활컨텐츠용 도구.
    public void ChangeLifeTool(string a_sLifeInteractionName, bool a_bActionState)
    {
        eItemID eToolID = eItemID.None;
        switch (a_sLifeInteractionName)
        {
            case "Harvesting":
                eToolID = eItemID.Sickle;
                break;
            case "Fishing":
                eToolID = eItemID.FishingPole;
                break;
            case "Felling":
                eToolID = eItemID.Ax;
                break;
            default:
                Debug.LogError("Non Action Error"); break;
        }
        if (GameMng.Ins.hero.characterData.m_dicEquipItems.ContainsKey(eItemType.Weapon))
        {
            if(a_bActionState == true)
            {
                GameMng.Ins.hero.weaponRoot.transform.GetChild(0).gameObject.SetActive(false);
                EquipWeapon(eToolID, GameMng.Ins.hero.weaponRoot);
            }
            else
            {
                GameMng.Ins.hero.weaponRoot.transform.GetChild(0).gameObject.SetActive(true);
                ClearWeapon(GameMng.Ins.hero.weaponRoot.transform.GetChild(1).gameObject);
            }
        }
        else
        {
            if (a_bActionState == true)
            {
                EquipWeapon(eToolID, GameMng.Ins.hero.weaponRoot);
            }
            else
            {
                ClearWeapon(GameMng.Ins.hero.weaponRoot.transform.GetChild(0).gameObject);
            }
        }
    }

    public void SaveItemInfo()
    {
        if (GameMng.Ins.hero.weaponRoot.transform.childCount == 0) return;

        GameMng.Ins.hero.weaponRoot.transform.GetChild(0).parent = transform;
    }

    

}
