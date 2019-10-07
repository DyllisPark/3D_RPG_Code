using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//캐릭터 생성씬 UI
public class UI_CreateCharacter : MonoBehaviour
{
    #region INSPECTOR

    public GameObject       m_objCharacterRoot;
    public GameObject       m_objExceptionUI;
    [HideInInspector]
    public GameObject       m_objNowHero;
    public InputField       inputUserName;
    public Text             exceptionText;
    public Button           angelCampBtn;
    public Button           devilCampBtn;

    public UI_Customizing       customizingUI;
    public UI_ClassSelection    classSelectionUI;

    public bool m_bClassSelectionState;
    public bool m_bGenderSelectionState;
    public bool m_bInputPlayerNameState;
    public bool m_bCampSelectionState;

    public eCamp        m_eCamp;
    public eGender      m_eGender;
    public eHeroType    m_eHeroType;

    #endregion



    public void Init(eGender gender)
    {
        customizingUI.CustomizingInit(gender);
        angelCampBtn.image.color = Define.deactivationBtnColor;
        devilCampBtn.image.color = Define.deactivationBtnColor;
        m_bClassSelectionState  = false;
        m_bGenderSelectionState = false;
        m_bInputPlayerNameState = false;
        m_bCampSelectionState   = false;
        m_objNowHero = null;
    }

    //진영 선택시.
    public void OnClickCampSelection(GameObject a_objCamp)
    {
        m_eCamp = (eCamp)int.Parse(a_objCamp.name);
        customizingUI.gameObject.SetActive(true);
        classSelectionUI.gameObject.SetActive(true);

        if (m_eCamp == eCamp.Angel)
        {
            m_objNowHero = customizingUI.m_objAngelMale; m_objNowHero.SetActive(true);
            Define.ChangeButtonColor(angelCampBtn, devilCampBtn);
        }
        else
        {
            m_objNowHero = customizingUI.m_objDevilMale; m_objNowHero.SetActive(true);
            Define.ChangeButtonColor(devilCampBtn, angelCampBtn);
        }

        customizingUI.HeroInfoInit(m_objNowHero);
        Clear();
        m_bCampSelectionState = true;
    }

    public void Clear()
    {
        customizingUI.Clear(m_objNowHero.name);
        classSelectionUI.Clear();
        m_bClassSelectionState = false;
    }

    //조건 만족시 캐릭터 생성.
    public void OnClickCreateCharacter()
    {
        if (inputUserName.text != "") m_bInputPlayerNameState = true;

        if (m_bCampSelectionState && m_bClassSelectionState && m_bInputPlayerNameState)
        {
            CreateCharacterDataSave();
            ActiveExceptionUI(string.Format("캐릭터 생성중.."));
            SceneMng.Ins.ChangeScene(eScene.CharacterSelectionScene);
        }
        else
        {
            if      (!m_bCampSelectionState)    ActiveExceptionUI(string.Format("진영을 선택하세요."));
            else if (!m_bClassSelectionState)   ActiveExceptionUI(string.Format("직업을 선택하세요."));
            else                                ActiveExceptionUI(string.Format("캐릭터 이름을 입력하세요."));
        }
    }

    void CreateCharacterDataSave()
    {
        SelectedCharacterData data = new SelectedCharacterData(inputUserName.text, (eHero)m_eHeroType + 1, m_eHeroType, m_eGender, m_eCamp, eQuest.Quest1_1, customizingUI.m_dicCustomizingData);
        DataMng.Ins.CharacterAdd(data);}


    public void ActiveExceptionUI(string a_sMessage)
    {
        exceptionText.text = a_sMessage;
        StartCoroutine(ExceptionUI());
    }

    IEnumerator ExceptionUI()
    {
        m_objExceptionUI.SetActive(true);
        yield return YieldReturnCaching.WaitForSeconds(1f);
        m_objExceptionUI.SetActive(false);
    }
}
