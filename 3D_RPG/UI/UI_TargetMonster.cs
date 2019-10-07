using UnityEngine;
using UnityEngine.UI;
using Global_Define;

public class UI_TargetMonster : MonoBehaviour
{
    #region INSPECTOR

    public Text monsterNametxt;
    public Text monsterAggressivetxt;
    public Image monsterHpImg;

    #endregion


    public void TargettedMonsterInfo(Monster a_monster)
    {
        monsterNametxt.text = a_monster.monsterInfo.m_sMonsterName;
        monsterAggressivetxt.text = a_monster.monsterInfo.m_eMonsterAtkType.ToDesc();
        monsterHpImg.fillAmount = a_monster.m_fHpRate;
    }

    public void TargettedMonsterHPUpdate(Monster a_monster)
    {
        monsterHpImg.fillAmount = a_monster.m_fHpRate;
    }
}
