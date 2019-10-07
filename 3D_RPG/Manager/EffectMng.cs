using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//히어로, 몬스터의 이펙트 관리.
public class EffectMng : MonoBehaviour
{
    #region SINGLETON
    static EffectMng _instance = null;

    public static EffectMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(EffectMng)) as EffectMng;
                if (_instance == null)
                {
                    _instance = new GameObject("EffectMng", typeof(EffectMng)).GetComponent<EffectMng>();
                }
            }

            return _instance;
        }
    }

    #endregion

    #region INSPECTOR

    public Dictionary<eRangedMonsterBullet, int>    m_dicUsingBulletCount       = new Dictionary<eRangedMonsterBullet, int>();
    public Dictionary<eRangedMonsterExplosion, int> m_dicUsingExplosionCount    = new Dictionary<eRangedMonsterExplosion, int>();
    public Dictionary<eHeroState, int>              m_dicSkillEffectCount       = new Dictionary<eHeroState, int>();
    public Dictionary<eHeroState, int>              m_dicHandEffectCount        = new Dictionary<eHeroState, int>();
    public Dictionary<eBossAtkType, int>            m_dicBossSkillEffectCount   = new Dictionary<eBossAtkType, int>();

    Dictionary<eRangedMonsterBullet, List<GameObject>>      m_dicRangedMonsterBullet    = new Dictionary<eRangedMonsterBullet, List<GameObject>>();
    Dictionary<eRangedMonsterExplosion, List<GameObject>>   m_dicRangedMonsterExplosion = new Dictionary<eRangedMonsterExplosion, List<GameObject>>();
    Dictionary<eHeroState, List<GameObject>>                m_dicSkillEffect            = new Dictionary<eHeroState, List<GameObject>>();
    Dictionary<eHeroState, List<GameObject>>                m_dicHandEffect             = new Dictionary<eHeroState, List<GameObject>>();
    Dictionary<eBossAtkType, List<GameObject>>              m_dicBossSkillEffect        = new Dictionary<eBossAtkType, List<GameObject>>();
    

    #endregion


    public GameObject GetHandEffect(eHeroState a_eHeroState)
    {
        return GetEffectObject(a_eHeroState, string.Format(Path.HANDEFFECT_PATH, a_eHeroState), m_dicHandEffect, m_dicHandEffectCount);
    }

    public GameObject GetSkillEffect(eHeroState a_eHeroState)
    {
        return GetEffectObject(a_eHeroState, string.Format(Path.SKILLEFFECT_PATH, a_eHeroState), m_dicSkillEffect, m_dicSkillEffectCount);
    }

    public GameObject GetBullet(eRangedMonsterBullet a_eRangedMonsterBullet)
    {
        return GetEffectObject(a_eRangedMonsterBullet, string.Format(Path.RANGEDMONSTERATK_PATH, a_eRangedMonsterBullet), m_dicRangedMonsterBullet, m_dicUsingBulletCount);
    }

    public GameObject GetExplosion(eRangedMonsterExplosion a_eRangedMonsterExplosion)
    {
        return GetEffectObject(a_eRangedMonsterExplosion, string.Format(Path.RANGEDMONSTERATK_PATH, a_eRangedMonsterExplosion), m_dicRangedMonsterExplosion, m_dicUsingExplosionCount);
    }

    public GameObject GetBossSkill(eBossAtkType a_eBossAtkType)
    {
        return GetEffectObject(a_eBossAtkType, string.Format(Path.BOSSEFFECT_PATH, a_eBossAtkType), m_dicBossSkillEffect, m_dicBossSkillEffectCount);
    }

    public void SetHandEffectReset(GameObject a_objLeft, GameObject a_objRight)
    {
        SetEffectReset(a_objLeft);
        SetEffectReset(a_objRight);
    }

    public void SetEffectReset(GameObject a_objEffect)
    {
        gameObject.transform.SetChild(a_objEffect);
        a_objEffect.SetActive(false);
    }


    GameObject GetEffectObject<T>(T a_eEffect, string a_sPath, Dictionary<T, List<GameObject>> a_dicEffect, Dictionary<T, int> a_dicEffectCount)
    {
        if (a_dicEffect.ContainsKey(a_eEffect) == false
            || a_dicEffectCount[a_eEffect] >= a_dicEffect[a_eEffect].Count)
        {
            AddPool(a_eEffect, a_sPath, a_dicEffect, a_dicEffectCount);
        }

        int nPoolIndex = 0;
        while (a_dicEffect[a_eEffect][nPoolIndex].gameObject.activeSelf == true)
        {
            ++nPoolIndex;
        }
        ++a_dicEffectCount[a_eEffect];
        a_dicEffect[a_eEffect][nPoolIndex].SetActive(true);

        return a_dicEffect[a_eEffect][nPoolIndex];
    }


    void AddPool<T>(T a_eEffect, string a_sPath, Dictionary<T, List<GameObject>> a_dicEffect, Dictionary<T, int> a_dicEffectCount)
    {
        for (int i = 0; i < Define.nPOOLSIZE; ++i)
        {
            if (a_dicEffect.ContainsKey(a_eEffect) == false)
            {
                a_dicEffect.Add(a_eEffect, new List<GameObject>());
                a_dicEffectCount.Add(a_eEffect, 0);
            }
            a_dicEffect[a_eEffect].Add(GetClone(a_eEffect, a_sPath)); 
        }
    }

    GameObject GetClone<T>(T a_eEffect, string a_sPath )
    {
        GameObject effect = gameObject.Instantiate_asChild(a_sPath);
        effect.SetActive(false);
        return effect;
    }
}
