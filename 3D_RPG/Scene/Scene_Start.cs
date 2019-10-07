using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Global_Define;
using DG.Tweening;

public class Scene_Start : MonoBehaviour
{
    public eScene m_eScene { get { return eScene.StartScene; } }
    public VideoPlayer video;
    public Text startInfotxt;

    void Start()
    {
        SceneMng.Ins.SetScene(m_eScene);
        video.Play();
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        while(true)
        {
            startInfotxt.DOFade(0, 1);
            yield return YieldReturnCaching.WaitForSeconds(1f);
            startInfotxt.DOFade(1, 1);
            yield return YieldReturnCaching.WaitForSeconds(1f);
        }
    }

    public void OnClickGameStart()
    {
        SceneMng.Ins.ChangeScene(eScene.TableLoadingScene);
    }
}
