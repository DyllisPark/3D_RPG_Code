using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//데미지 텍스트 및 몬스터 HP 관리.
public class DamageMng : MonoBehaviour
{

    #region SINGLETON
    static DamageMng _instance = null;

    public static DamageMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(DamageMng)) as DamageMng;
                if (_instance == null)
                {
                    _instance = new GameObject("DamageMng", typeof(DamageMng)).GetComponent<DamageMng>();
                }
            }

            return _instance;
        }
    }

    #endregion

    #region INSPECTOR

    public List<Text>       m_liDamagePool = new List<Text>();
    public List<Monster>    m_liSceneMonsters = new List<Monster>();
    public int              m_nUsingDamageCount = 0;


    #endregion

    public void MonsterHpBarPoolInit()
    {
        for(int i = 0; i < m_liSceneMonsters.Count; ++i)
        {
            GameObject monsterHpBar = UIMng.Ins.monsterHPBarRoot.Instantiate_asChild(Path.MONSTERHPBAR_PATH);
            m_liSceneMonsters[i].monsterHpBar = monsterHpBar.GetComponent<MonsterHPBar>();
            m_liSceneMonsters[i].hpBarImg = m_liSceneMonsters[i].monsterHpBar.hpBarImg;
            monsterHpBar.SetActive(false);
        }
    }

    public void DamagePoolInit()
    {
        DamageTxtAddPool();
    }

    public Text GetDamageText()
    {
        if(m_nUsingDamageCount >= m_liDamagePool.Count)
        {
            DamageTxtAddPool();
        }

        int nPoolIndex = 0;
        while(m_liDamagePool[nPoolIndex].gameObject.activeSelf == true)
        {
            ++nPoolIndex;
        }
        ++m_nUsingDamageCount;
        m_liDamagePool[nPoolIndex].gameObject.SetActive(true);

        return m_liDamagePool[nPoolIndex];
    }

    void DamageTxtAddPool()
    {
        for (int i = 0; i < Define.nPOOLSIZE; ++i)
        {
            m_liDamagePool.Add(DamageClone());
        }
    }
   

    Text DamageClone()
    {
        Text text = UIMng.Ins.damageTextRoot.Instantiate_asChild(Path.DAMAGETEXT_PATH).GetComponent<Text>();
        text.gameObject.SetActive(false);        
        return text;
    }
}
