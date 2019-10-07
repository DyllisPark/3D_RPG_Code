using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using Global_Define;

public static class ExtentionMethod
{

    #region GET TABLE
    //Get 테이블
    public static UserInherentData GetUserInherentDataTb(this eUserID a_eUserId)
    {
        var userTb = TableMng.Ins.UserTb.GetTable();
        return userTb[a_eUserId];
    }

    public static CustomizingData GetCustomizingTb(this eCustomizing a_eCustomizing)
    {
        var customizeTb = TableMng.Ins.CustomizingTb.GetTable();
        return customizeTb[a_eCustomizing];
    }

    public static HeroData GetHeroTb(this eHero a_eHero)
    {
        var heroTb = TableMng.Ins.HeroTb.GetTable();
        return heroTb[a_eHero];
    }

    public static HeroTypeInfoData GetHeroTypeInfoTb(this eHeroType a_eHeroTypeInfo)
    {
        var heroTypeInfoTb = TableMng.Ins.HeroTypeInfoTb.GetTable();
        return heroTypeInfoTb[a_eHeroTypeInfo];
    }

    public static HeroActiveSkillData GetHeroActiveSkillTb(this eHeroState a_eHeroState)
    {
        var activeSkillTb = TableMng.Ins.HeroActiveSkillTb.GetTable();
        return activeSkillTb[a_eHeroState];
    }

    public static MonsterData GetMonsterTb(this eMonster a_eMonster)
    {
        var monsterTb = TableMng.Ins.MonsterTb.GetTable();
        return monsterTb[a_eMonster];
    }

    public static QuestData GetQuestTb(this eQuest a_eQuest)
    {
        var questTb = TableMng.Ins.QuestTb.GetTable();
        return questTb[a_eQuest];
    }

    public static QuestPurposeData GetQuestPurposeTb(this int a_nId)
    {
        var questPurposeTb = TableMng.Ins.QuestPurposeTb.GetTable();
        return questPurposeTb[a_nId];
    }

    public static ItemData GetItemTb(this eItemID a_eItemID)
    {
        var itemTb = TableMng.Ins.ItemTb.GetTable();
        return itemTb[a_eItemID];
    }

    public static ArmorData GetArmorTb(this int a_nID)
    {
        var armorTb = TableMng.Ins.ArmorTb.GetTable();
        return armorTb[a_nID];
    }

    public static WeaponData GetWeaponTb(this int a_nID)
    {
        var weaponTb = TableMng.Ins.WeaponTb.GetTable();
        return weaponTb[a_nID];
    }

    public static ToolData GetToolTb(this int a_nID)
    {
        var toolTb = TableMng.Ins.ToolTb.GetTable();
        return toolTb[a_nID];
    }

    public static MaterialItemData GetMaterialITemTb(this int a_nID)
    {
        var materialItemTb = TableMng.Ins.MaterialItemTb.GetTable();
        return materialItemTb[a_nID];
    }

    public static GemData GetGemItemTb(this int a_nID)
    {
        var gemItemTb = TableMng.Ins.GemItemTb.GetTable();
        return gemItemTb[a_nID];
    }

    public static ConfigData GetConfigValue(this eConfig a_eConfig)
    {
        var configTb = TableMng.Ins.ConfigTb.GetTable();
        return configTb[a_eConfig.ToString()];
    }

    #endregion


    #region UTILE METHOD
    //유틸함수
    public static string ToDesc(this System.Enum a_eEnumVal)
    {
        var da = (DescriptionAttribute[])(a_eEnumVal.GetType().GetField(a_eEnumVal.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
        return da.Length > 0 ? da[0].Description : a_eEnumVal.ToString();
    }

    public static Quaternion GetLookAtQuaternion(this Transform a_startPos, Transform a_endPos)
    {
        Vector3 vec = a_endPos.transform.position - a_startPos.position;
        vec.Normalize();
        Quaternion quaternion = Quaternion.LookRotation(vec);
        return quaternion;
    }

    //Gameobjet 생성
    public static GameObject Instantiate_asChild(this GameObject a_objParent, string a_strPrefabName)
    {
        if (string.IsNullOrEmpty(a_strPrefabName) == true)
        {
            Debug.LogError("Prefab is empty");
            return null;
        }

        string FileName_withPath = string.Format(Path.PREFAB_PATH_ADD, a_strPrefabName);

        GameObject objPrefab = Resources.Load(FileName_withPath) as GameObject;

        if (objPrefab == null)
        {
            Debug.LogError(string.Format("logic error - no prefab name {0}", FileName_withPath));
            return null;
        }

        return a_objParent.Instantiate_asChild(objPrefab);
    }

    public static GameObject Instantiate_asChild(this GameObject a_objParent, GameObject a_objPrefab)
    {
        GameObject objChild = GameObject.Instantiate(a_objPrefab) as GameObject;
        SetChild(a_objParent.transform, objChild);
        return objChild;
    }

    public static void SetChild(this Transform a_trParent, GameObject a_objChild)
    {
        if (a_objChild != null)
        {
            a_objChild.transform.SetParent(a_trParent);
            a_objChild.transform.localPosition  = Vector3.zero;
            a_objChild.transform.localRotation  = Quaternion.identity;
            a_objChild.transform.localScale     = Vector3.one;
            a_objChild.gameObject.layer         = a_trParent.gameObject.layer;
        }
    }

    public static void ActiveChange(this GameObject gameObject) => gameObject.SetActive(!gameObject.activeSelf);

    //Get Mesh
    public static Mesh GetOnePartCustomizeMesh(this eCustomizing a_eCustomizing, eGender a_eGender, int a_nIndex)
    {
        string s = string.Format(Path.CUSTOMIZING_ONEPARTS_PATH, a_eGender, a_eCustomizing, a_eCustomizing, a_nIndex);
        return Resources.Load(s) as Mesh;
    }

    public static Mesh GetMultiPartsCustomizeMesh(this eCustomizing a_eCustomizing, eGender a_eGender, string a_sParts, string a_sDir, int a_nIndex)
    {
        string s = string.Format(Path.CUSTOMIZING_MULTIPARTS_PATH, a_eGender, a_eCustomizing, a_sParts, a_sDir, a_nIndex);
        return Resources.Load(s) as Mesh;
    }

    public static void GetRewardItem(this string a_sRewardItems, Queue<eItemID> a_qItems)
    {
        a_qItems.Clear();

        int nStartIndex = 0;
        int nLenth = 0;
        for (int i = 0; i < a_sRewardItems.Length; ++i)
        {
            ++nLenth;
            if (a_sRewardItems[i].Equals(','))
            {
                string sItem = a_sRewardItems.Substring(nStartIndex, nLenth - 1);
                a_qItems.Enqueue((eItemID)(int.Parse(sItem)));
                nStartIndex = i + 1;
                nLenth = 0;
            }
        }
    }

    //구글 스프레드시트에서 string으로 받은 아이템의 리스트를 개별의 아이템으로 얻는 함수
    public static void GetQuestPurpose<T>(this string a_sPurpose, string a_sCount, Dictionary<T, int> a_dicQuestPurpose)
    {
        int nPurPoseItemStartIndex = 0;
        int nItemCountStartIndex = 0;
        int nPurposItemsLenth = 0;
        int nItemCountLenth = 0;

        for (int i = 0; i < a_sPurpose.Length; ++i)
        {
            ++nPurposItemsLenth;
            if (a_sPurpose[i].Equals(','))
            {
                string sItem = a_sPurpose.Substring(nPurPoseItemStartIndex, nPurposItemsLenth - 1);

                for (int j = nItemCountStartIndex; j < a_sCount.Length; ++j)
                {
                    ++nItemCountLenth;
                    if (a_sCount[j].Equals(','))
                    {
                        string sCount = a_sCount.Substring(nItemCountStartIndex, nItemCountLenth - 1);

                        if (a_dicQuestPurpose.ContainsKey((T)(Enum.Parse(typeof(T),sItem))) == false)
                        {
                            a_dicQuestPurpose.Add((T)(Enum.Parse(typeof(T), sItem)), int.Parse(sCount));
                            QuestMng.Ins.m_dicNowQuestPurpose_Gain.Add((eItemID)(int.Parse(sItem)), 0);
                            nItemCountStartIndex = j + 1;
                            nItemCountLenth = 0;
                            break;
                        }
                    }
                }
                nPurPoseItemStartIndex = i + 1;
                nPurposItemsLenth = 0;
            }
        }
    }

    public static void GetQuestPurposeTest<T>(this string a_sPurpose, string a_sCount, Dictionary<T, int> a_dicQuestPurpose, Dictionary<T, int> a_dicQuestPurposeCount)
    {
        string[] sPurposeResult = a_sPurpose.Split(new char[] { ',' } );
        string[] sCountResult   = a_sCount.Split(new char[] { ',' });

        for(int i = 0; i < sPurposeResult.Length - 1; ++i)      //마지막 값은 빈 문자열이기 때문에 제외.
        {
            if (a_dicQuestPurpose.ContainsKey((T)(Enum.Parse(typeof(T), sPurposeResult[i]))) == false)
            {
                a_dicQuestPurpose.Add((T)(Enum.Parse(typeof(T), sPurposeResult[i])), int.Parse(sCountResult[i]));
                a_dicQuestPurposeCount.Add((T)(Enum.Parse(typeof(T), sPurposeResult[i])), 0);
            }
        }
    }

    #endregion
}
