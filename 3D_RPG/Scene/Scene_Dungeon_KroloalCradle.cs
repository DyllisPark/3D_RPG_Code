using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class Scene_Dungeon_KroloalCradle : LocalSceneBase
{
    #region INSPECTOR

    public eScene m_eScene { get { return eScene.Dungeon_KroloalCradleScene; } }
    public GameObject m_objSpawn;

    #endregion

    new void Awake()
    {
        base.Awake();

        UIMng.Ins.LocalNameFadeOut(m_eScene.ToDesc());
        SceneMng.Ins.SetScene(m_eScene);

        m_objHero = m_objSpawn.Instantiate_asChild(string.Format("Character/{0}{1}", characterData.m_eCamp, characterData.m_eGender));
        GameMng.Ins.hero = m_objHero.GetComponent<Hero>();
        GameMng.Ins.hero.HeroDataInit(m_objSpawn);
        m_objHero.SetActive(true);

        SceneInit();
    }
}
