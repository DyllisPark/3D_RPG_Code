using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//커스터마이징 보여주기용 영웅. 실제 인게임 영웅와 별개.
public class CustomizingHero : MonoBehaviour
{
    #region INSPECTOR

    public CharacterParts myCharacterParts;

    SelectedCharacterData   characterData;
    HeroStat                originStat  = new HeroStat();
    HeroStat                nowStat     = new HeroStat();

    #endregion

 
    public void HeroDataInit(GameObject a_objHeroRoot)
    {
        characterData = DataMng.Ins.nowSelectedCharacterData;
        myCharacterParts.SetCustomizing(characterData);
    }

}
