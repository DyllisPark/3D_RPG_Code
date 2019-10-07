using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

public class UI_Customizing : MonoBehaviour
{
    #region INSPECTOR

    public GameObject   m_objCharacterRoot;
    public GameObject   m_objAngelMale;
    public GameObject   m_objAngelFemale;
    public GameObject   m_objDevilMale;
    public GameObject   m_objDevilFemale;
    public Button       maleBtn;
    public Button       femaleBtn;

    public UI_CreateCharacter   createCharacterUI;
    public CharacterParts       m_characterParts;
    public CustomizingHero       m_hero;
    
    public eGender              m_eGender;
    public eCamp                m_eCamp;
    public List<GameObject>     m_liCharacter;
    public Dictionary<eCustomizing, int> m_dicCustomizingData = new Dictionary<eCustomizing, int>();

    Dictionary<eCustomizing, int> m_dicCustomizingMaxCount = new Dictionary<eCustomizing, int>();
    
    #endregion
    
    //캐릭터 생성씬 처음 보여질 커스터마이징 캐릭터 초기화.
    public void CustomizingInit(eGender a_eGender)
    {
        for (eCustomizing e = eCustomizing.Ear; e < eCustomizing.Max; ++e)
        {
            m_dicCustomizingData.Add(e, 1);
            m_dicCustomizingMaxCount.Add(e, e.GetCustomizingTb().m_nPartsCount);
        }
        Define.ChangeButtonColor(maleBtn, femaleBtn);
        m_eGender = a_eGender;
    }

    public void Clear(string a_sObjName)
    {
        foreach(var character in m_liCharacter)
        {
            if(a_sObjName.Equals(character.name) == false)
            {
                character.SetActive(false);
            }
        }
    }

    //성별 선택
    public void OnClickGender(GenderInfo a_gender)
    {
        m_eGender = a_gender.m_eGender;
        m_eCamp = createCharacterUI.m_eCamp;

        if (m_eCamp == eCamp.Angel) { CheckGender(m_objAngelMale, m_objAngelFemale, m_eGender); }
        else                        { CheckGender(m_objDevilMale, m_objDevilFemale, m_eGender); }

        createCharacterUI.m_eGender = m_eGender;
        createCharacterUI.Clear();
        createCharacterUI.m_bGenderSelectionState = true;
    }

    void CheckGender(GameObject a_objMale, GameObject a_objFemale, eGender a_eGender)
    {
        if(a_eGender == eGender.Male)   { ActiveGender(a_objMale, a_objFemale); Define.ChangeButtonColor(maleBtn, femaleBtn); }
        else                            { ActiveGender(a_objFemale, a_objMale); Define.ChangeButtonColor(femaleBtn, maleBtn); }
    }

    void ActiveGender(GameObject a_objNowGender, GameObject a_objRestGender)
    {
        a_objNowGender.SetActive(true);
        a_objRestGender.SetActive(false);
        HeroInfoInit(a_objNowGender);
    }

    public void HeroInfoInit(GameObject a_objNowHero)
    {
        createCharacterUI.m_objNowHero = a_objNowHero;
        m_hero = a_objNowHero.GetComponent<CustomizingHero>();
        m_characterParts = m_hero.myCharacterParts;   
        Clear(a_objNowHero.name);
    }

    public void OnClickCustomizingLeft(CustomizingInfo a_info)
    {
        eCustomizing eCustomaze = a_info.m_eCustomizing;
        
        if (ExceptionCheck(eCustomaze) == true) return;

        if (m_dicCustomizingData[eCustomaze] == 1)
        {
            m_dicCustomizingData[eCustomaze] = m_dicCustomizingMaxCount[eCustomaze];
        }
        else
            --m_dicCustomizingData[eCustomaze];

        ChangeCustomizing(eCustomaze);

    }

    public void OnClickCustomazingRight(CustomizingInfo a_info)
    {
        eCustomizing eCustomaze = a_info.m_eCustomizing;
        if (ExceptionCheck(eCustomaze) == true) return;

        if (m_dicCustomizingData[eCustomaze] == m_dicCustomizingMaxCount[eCustomaze])
        {
            m_dicCustomizingData[eCustomaze] = 1;
        }
        else
        {
            ++m_dicCustomizingData[eCustomaze];
        }

        ChangeCustomizing(eCustomaze);
    }


    void ChangeCustomizing(eCustomizing a_eCustomizing)
    {
        if (a_eCustomizing == eCustomizing.HandLeft)
        {
            m_characterParts.m_arrCustomazingParts[(int)a_eCustomizing].sharedMesh = a_eCustomizing.GetMultiPartsCustomizeMesh(m_eGender, "Hand",
                 "Left", m_dicCustomizingData[a_eCustomizing]);
            m_characterParts.m_arrCustomazingParts[(int)a_eCustomizing + 1].sharedMesh = a_eCustomizing.GetMultiPartsCustomizeMesh(m_eGender, "Hand",
                 "Right", m_dicCustomizingData[a_eCustomizing]);
        }
        else
        {
            m_characterParts.m_arrCustomazingParts[(int)a_eCustomizing].sharedMesh = a_eCustomizing.GetOnePartCustomizeMesh(m_eGender,m_dicCustomizingData[a_eCustomizing]); 
        }
    }

    bool ExceptionCheck(eCustomizing a_eCustomizing)
    {
        if(FemaleDontHaveFacialHair(a_eCustomizing))
        {
            createCharacterUI.ActiveExceptionUI(string.Format("여성은 수염 선택이 불가능합니다."));
            return true;
        }
        return false;
    }

    bool FemaleDontHaveFacialHair(eCustomizing a_eCustomizing)
    {
        return m_eGender == eGender.Female && a_eCustomizing == eCustomizing.FacialHair ? true : false;
    }
}
