using UnityEngine;
using Global_Define;

//캐릭터 생성씬 진영선택 UI
public class UI_CampSelection : MonoBehaviour
{
    #region INSPECTOR

    public UI_CreateCharacter createCharacterUI;



    #endregion

    public void OnClickCampSelection(GameObject a_objCamp)
    {
        eCamp camp = (eCamp)int.Parse(a_objCamp.name);
    }
}
