using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//지역 이동 로딩씬
public class Scene_SceneLoading : MonoBehaviour
{
    #region INSPECTOR

    public Image loadingSceneBgImg;
    public Image loadingBarImg;

    AsyncOperation async;
    bool m_bSceneLoadState = false;
    #endregion

    private void Start()
    {
        loadingSceneBgImg.sprite = SceneMng.Ins.sceneLoadingImgAtlas.GetSprite(SceneMng.Ins.m_eSceneToMoveInfo.ToString()) as Sprite;
        StartCoroutine(SceneLoading());
    }

    IEnumerator SceneLoading()
    {
        async = SceneMng.Ins.GetSceneAsync();
        async.allowSceneActivation = false;
        float fTimer = 0.0f;

        while (async.isDone == false)
        {
            fTimer += Time.deltaTime;
            loadingBarImg.fillAmount = Mathf.Lerp(loadingBarImg.fillAmount, 1f, fTimer);

            if (async.progress >= 0.9f)
            {
                loadingSceneBgImg.fillAmount = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
