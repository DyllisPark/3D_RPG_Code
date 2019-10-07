using UnityEngine;
using Global_Define;

//커스터마이징 및 장비 교체 오브젝트를 가진 클래스
public class CharacterParts : MonoBehaviour     
{
    #region INSPECTOR
    
    public SkinnedMeshRenderer[] m_arrCustomazingParts;
    public SkinnedMeshRenderer[] m_arrEquipParts;
    public GameObject m_objRightWeaponRoot;
    public GameObject m_objLeftWeaponRoot;
    public GameObject m_objRightHandWeapon;
    public GameObject m_objLeftHandWeapon;

    #endregion

    //선택한 커스터마이징 기반으로 세팅
    public void SetCustomizing(SelectedCharacterData a_characterData)
    {
        foreach (var custom in a_characterData.m_dicCustomizingData)
        {
            if ((a_characterData.m_eGender == eGender.Female && custom.Key == eCustomizing.FacialHair) || custom.Key == eCustomizing.HandRight) continue;

            if (custom.Key == eCustomizing.HandLeft)
            {
                m_arrCustomazingParts[(int)custom.Key].sharedMesh = custom.Key.GetMultiPartsCustomizeMesh(a_characterData.m_eGender, "Hand", "Left", custom.Value);
                m_arrCustomazingParts[(int)custom.Key + 1].sharedMesh = custom.Key.GetMultiPartsCustomizeMesh(a_characterData.m_eGender, "Hand", "Right", custom.Value);
            }
            else
            {
                m_arrCustomazingParts[(int)custom.Key].sharedMesh = custom.Key.GetOnePartCustomizeMesh(a_characterData.m_eGender, custom.Value);
            }
        }
    }

}
