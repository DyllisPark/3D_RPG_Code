using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//캐릭터 생성씬
public class Scene_CreateCharacter : MonoBehaviour
{
    #region INSPECTOR

    public UI_CreateCharacter createCharacterUI;

    public eScene m_eScene { get { return eScene.CreateCharacterScene; } }

    #endregion

    private void Start()
    {
        SceneMng.Ins.SetScene(m_eScene);
        CustomizingInit();
    }

    void CustomizingInit()
    {
        var hdata = eHero.Knight.GetHeroTb();
        var userData = eUserID.User.GetUserInherentDataTb();
        eGender gender = userData.m_eGender;

        createCharacterUI.Init(gender);
    }

    
}
