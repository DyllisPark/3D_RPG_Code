using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//캐릭터 생성 후 캐릭터 선택창에 추가되는 캐릭터 버튼 정보.
public class CharacterSelectionBtnInfo : MonoBehaviour
{
    public Image    campImg;
    public Text     userNameText;
    public Text     heroTypeText;
    public Text     levelValueText;

    public SelectedCharacterData selectedCharacterData;
}
