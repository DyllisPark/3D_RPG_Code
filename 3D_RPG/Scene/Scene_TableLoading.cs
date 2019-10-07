using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//게임 최초 시작시 테이블 로딩씬
public class Scene_TableLoading : MonoBehaviour
{
    #region INSPECTOR

    public Image loadingBarImg;

    #endregion


    private void Start()
    {
        if (TableMng.Ins.m_bTableDownLoadState)
        {
            SceneMng.Ins.ChangeScene(eScene.CharacterSelectionScene);
            return;
        }

        StartCoroutine(TableDown());
    }

    IEnumerator TableDown()
    {
        TableDownloader.Ins.Download();
        
        while(TableDownloader.Ins.m_nNowDownloadCount < TableDownloader.Ins.m_nSuccessCount)
        {
            yield return null;
            loadingBarImg.fillAmount = (float)TableDownloader.Ins.m_nNowDownloadCount / TableDownloader.Ins.m_nSuccessCount;
        }
        yield return YieldReturnCaching.WaitForSeconds(0.5f);
        SceneMng.Ins.ChangeScene(eScene.CharacterSelectionScene);
    }
}
