using UnityEngine;
using Global_Define;

//지역1 씬(마리아노플)
public class Scene_Local_Marianople : LocalSceneBase
{
    #region INSEPCTOR

    public eScene m_eScene { get { return eScene.Local_MarianopleScene; } }
    public GameObject       m_objAngelSpawn;
    public GameObject       m_objDevilSpawn;

    #endregion

    new void Awake()
    {
        base.Awake();

        UIMng.Ins.LocalNameFadeOut(m_eScene.ToDesc());
        SceneMng.Ins.SetScene(m_eScene);

        GameObject spawnCamp;
        if (characterData.m_eCamp == eCamp.Angel) spawnCamp = m_objAngelSpawn;
        else spawnCamp = m_objDevilSpawn;

        GridMng.Ins.Init();        
        m_objHero = spawnCamp.Instantiate_asChild(string.Format("Character/{0}{1}", characterData.m_eCamp, characterData.m_eGender));
        GameMng.Ins.hero = m_objHero.GetComponent<Hero>();
        GameMng.Ins.hero.HeroDataInit(spawnCamp);
        m_objHero.SetActive(true);
        GridMng.Ins.SetFirstGridPos(GameMng.Ins.hero.transform.position);

        SceneInit();
    }

    private void Update()
    {
        GridMng.Ins.CheckGrid(GameMng.Ins.hero.transform.position);
    }
}

