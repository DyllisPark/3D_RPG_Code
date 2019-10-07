using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//캐릭터 선택씬
public class Scene_CharacterSelection : MonoBehaviour
{
    #region INSPECTOR

    public UI_CharacterSelection characterSelectionUI;

    public eScene   m_eScene { get { return eScene.CharacterSelectionScene; } }

    #endregion


    private void Start()
    {
        SceneMng.Ins.SetScene(m_eScene);
        characterSelectionUI.Init();
    }
}
