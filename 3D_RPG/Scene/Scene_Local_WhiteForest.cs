using UnityEngine;
using Global_Define;

//지역2 씬(하얀숲)
public class Scene_Local_WhiteForest : LocalSceneBase
{
    #region INSPECTOR

    public eScene m_eScene { get { return eScene.Local_WhiteForestScene; } }
    public GameObject m_objHeroRoot;

    #endregion

    new void Awake()
    {
        base.Awake();

        UIMng.Ins.LocalNameFadeOut(m_eScene.ToDesc());
        SceneMng.Ins.SetScene(m_eScene);

        m_objHero = m_objHeroRoot.Instantiate_asChild(string.Format("Character/{0}{1}", characterData.m_eCamp, characterData.m_eGender));
        GameMng.Ins.hero = m_objHero.GetComponent<Hero>();
        GameMng.Ins.hero.HeroDataInit(m_objHeroRoot);
        m_objHero.SetActive(true);

        SceneInit();
    }
}






/*
var a = JsonSaveTest.Ins.LoadJsonDataFile<SelectedCharacterData>(Application.dataPath, "HeroInfo");
hero = heroRoot.Instantiate_asChild(string.Format("Character/Character{0}", a.m_eGender));
var b = hero.GetComponent<Hero>().myCharacterParts;

for(eCustomizing e = eCustomizing.Ear; e < eCustomizing.Max; ++e)
{
    if (e == eCustomizing.Hand)
    {
        b.m_arrCustomazingParts[(int)e].sharedMesh = heroRoot.Instantiate_asChild(string.Format("{0}/{1}/{2}/{3}Left{4}", Path.CUSTOMIZINGPARTS_PATH, a.m_eGender, e
        , e, a.m_liCustomizingData[(int)e])).GetComponent<SkinnedMeshRenderer>().sharedMesh;
        b.m_arrCustomazingParts[(int)e + 1].sharedMesh = heroRoot.Instantiate_asChild(string.Format("{0}/{1}/{2}/{3}Right{4}", Path.CUSTOMIZINGPARTS_PATH, a.m_eGender, e
        , e, a.m_liCustomizingData[(int)e])).GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }
    else
    {
        b.m_arrCustomazingParts[(int)e].sharedMesh = heroRoot.Instantiate_asChild(string.Format("Character/CustomizingParts/{0}/{1}/{2}{3}",
        a.m_eGender, e, e, a.m_liCustomizingData[(int)e])).GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }
}*/
