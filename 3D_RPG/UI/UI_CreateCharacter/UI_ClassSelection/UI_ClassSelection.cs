using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//캐릭터 생성씬 직업 선택UI
public class UI_ClassSelection : MonoBehaviour
{
    #region INSPECTOR

    public UI_CreateCharacter       createCharacterUI;
    public GameObject               classInfoUI;
    public Text                     classNameText;
    public Text                     classInfoText;
    public MeshFilter               knightWeapon;
    public MeshFilter               magicKngihtWeapon;

    public eHeroType m_eHeroType;


    #endregion


    public void Clear()
    {
        classInfoUI.SetActive(false);
    }

    public void OnClickClassSelection(ButtonUIInfo a_buttonInfo)
    {
        m_eHeroType = a_buttonInfo.m_eHeroType;
        ClassBasicWeapon();
        var heroTypeInfoData = m_eHeroType.GetHeroTypeInfoTb();
        classNameText.text = heroTypeInfoData.m_sHeroTypeName;
        classInfoText.text = heroTypeInfoData.m_sHeroTypeExplain;
        createCharacterUI.m_bClassSelectionState = true;
        createCharacterUI.m_eHeroType = m_eHeroType;
        classInfoUI.SetActive(true);
    }

    void ClassBasicWeapon()
    {
        GameObject weapon = createCharacterUI.m_objNowHero.GetComponent<CharacterParts>().m_objRightHandWeapon;
        switch (m_eHeroType)
        {
            case eHeroType.Knight:
                weapon.GetComponent<MeshFilter>().sharedMesh
                    = knightWeapon.sharedMesh;
                break;

            case eHeroType.MagicKnight:
                weapon.GetComponent<MeshFilter>().sharedMesh
                    = magicKngihtWeapon.sharedMesh;
                break;
            default:
                Debug.LogError("eHeroType is null");
                break;
        }
        weapon.SetActive(true);
    }


}
