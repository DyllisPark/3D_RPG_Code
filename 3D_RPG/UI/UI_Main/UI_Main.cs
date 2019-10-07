using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//Hp, Mp, Exp등 메인 프레임 UI 표기
public class UI_Main : MonoBehaviour
{
    #region INSPECTOR

    public Image hpImg;
    public Image mpImg;
    public Image expImg;

    #endregion

    public void MainUIInit()
    {
        HpRateUpdate();
        MpRateUpdate();
    }

    public void HpRateUpdate()
    {
        hpImg.material.SetFloat("OffsetUV_Y_1", GameMng.Ins.hero.m_fHpRate);
    }

    public void MpRateUpdate()
    {
        mpImg.material.SetFloat("OffsetUV_Y_1", GameMng.Ins.hero.m_fMpRate);
    }

    public void ExpRateUpdate()
    {
        //expImg.fillAmount = GameMng.Ins.hero.m_fExpRate);
    }

}
