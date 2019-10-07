using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Global_Define;

//캐릭터 선택UI
public class UI_CharacterSelection : MonoBehaviour
{
    #region INSPECTOR
    public GameObject       m_objCharacterRoot;
    public GameObject       m_objCharacterBtnRoot;
    public GameObject       m_objExceptionUI;
    public Text             exceptionText;

    #endregion


    public void Init()
    {
        if (DataMng.Ins.m_liSelectionCharacter.Count == 0) return;

        foreach(var data in DataMng.Ins.m_liSelectionCharacter)
        {
            GameObject characterBtn = m_objCharacterBtnRoot.Instantiate_asChild("UI/CharacterBtn");
            CharacterSelectionBtnInfo btnInfo = characterBtn.GetComponent<CharacterSelectionBtnInfo>();
            characterBtn.GetComponent<Button>().onClick.AddListener(() => OnClickCharacterBtn(btnInfo));
            
            var campTexture = Resources.Load(string.Format("Prefab/UI/Camp/{0}Camp", data.m_eCamp)) as Texture2D;
            btnInfo.campImg.sprite = Define.Texture2DToSprite(campTexture);
            btnInfo.userNameText.text = data.m_sUserName;
            btnInfo.heroTypeText.text = data.m_eHeroType.GetHeroTypeInfoTb().m_sHeroTypeName;
            btnInfo.levelValueText.text = data.m_eHero.GetHeroTb().m_nLevel.ToString();
            btnInfo.selectedCharacterData = data;            
        }
    }

    public void OnClickCharacterBtn(CharacterSelectionBtnInfo characterBtn)
    {
        GameObject character = m_objCharacterRoot.Instantiate_asChild(string.Format(Path.CUSTOMIZING_CHARACTER_PATH, characterBtn.selectedCharacterData.m_eCamp, characterBtn.selectedCharacterData.m_eGender));
        character.SetActive(true);
        DataMng.Ins.nowSelectedCharacterData = characterBtn.selectedCharacterData;
        character.GetComponent<CustomizingHero>().HeroDataInit(m_objCharacterRoot);
    }

    public void OnClickCreateCharacter()
    {
        
        SceneMng.Ins.ChangeScene(eScene.CreateCharacterScene);
    }

    //게임 시작시 영웅의 스탯 초기화.
    public void OnClickPlay()
    {
        if(DataMng.Ins.nowSelectedCharacterData == null)
        {
            ActiveExceptionUI(string.Format("캐릭터를 선택하세요."));
            StartCoroutine(DeActivateExceptionUI());
            return;
        }
        ActiveExceptionUI(string.Format("게임 생성중.."));
        DataMng.Ins.SetHeroStatData();
        SceneMng.Ins.SaveSceneToMoveInfo(eScene.Local_MarianopleScene);
    }

    public void ActiveExceptionUI(string a_sMessage)
    {
        exceptionText.text = a_sMessage;
        m_objExceptionUI.SetActive(true);
    }

    IEnumerator DeActivateExceptionUI()
    {
        yield return YieldReturnCaching.WaitForSeconds(1f);
        m_objExceptionUI.SetActive(false);
    }

}
