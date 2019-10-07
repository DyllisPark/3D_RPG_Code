using UnityEngine;
using Ricimi;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using Global_Define;

public class SceneMng : MonoBehaviour
{
    #region SINGLETON
    static SceneMng _instance = null;

    public static SceneMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(SceneMng)) as SceneMng;
                if (_instance == null)
                {
                    _instance = new GameObject("SceneMng", typeof(SceneMng)).GetComponent<SceneMng>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion


    #region INSPECTOR

    public SpriteAtlas sceneLoadingImgAtlas;

    public eScene       m_eNowScene;
    public eScene       m_eSceneToMoveInfo;
    
    #endregion

    public void ChangeScene(eScene a_eScene)
    {
        System.GC.Collect();
        SceneManager.LoadScene((int)a_eScene);
    }

    //지역을 이동할 때마다 로딩UI의 배경화면 교체를 위함. 하나의 로딩씬.
    public void SaveSceneToMoveInfo(eScene a_eScene)
    {        
        m_eSceneToMoveInfo = a_eScene;
        SceneManager.LoadScene((int)eScene.SceneLoadingScene);
    }

    public void SetScene(eScene a_eScene)
    {
        m_eNowScene = a_eScene;
    }

    public void FadeIn(System.Action a_fpCallback)
    {
        Transition.FadeAction(0.5f, Color.black, a_fpCallback);
    }

    //지역을 이동할 때마다 로딩UI의 배경화면 이미지 교체를 위함. 하나의 로딩씬.
    public AsyncOperation GetSceneAsync()
    {
        if (m_eSceneToMoveInfo == eScene.None) return null;
        AsyncOperation async = SceneManager.LoadSceneAsync((int)m_eSceneToMoveInfo);

        return async;
    }

}
