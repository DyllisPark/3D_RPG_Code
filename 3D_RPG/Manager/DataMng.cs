using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//캐릭터 고유의 데이터 관리.
public class DataMng : MonoBehaviour
{
    #region SINGLETON
    static DataMng _instance = null;

    public static DataMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(DataMng)) as DataMng;
                if (_instance == null)
                {
                    _instance = new GameObject("DataMng", typeof(DataMng)).GetComponent<DataMng>();
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

    public List<SelectedCharacterData>  m_liSelectionCharacter = new List<SelectedCharacterData>();
    public SelectedCharacterData        nowSelectedCharacterData = null;

    #endregion


    public void CharacterAdd(SelectedCharacterData characterData)
    {
        SelectedCharacterData data = new SelectedCharacterData();
        data.Copy(characterData);
        m_liSelectionCharacter.Add(data);
    }

    public void SetHeroStatData()
    {
        if (nowSelectedCharacterData == null)
        {
            Debug.LogError("Data is null");
            return;
        }
        SelectedCharacterData characterData = nowSelectedCharacterData;
        var heroTbData = characterData.m_eHero.GetHeroTb();
        characterData.originStat.m_eHero = characterData.m_eHero;
        characterData.originStat.m_eHeroType = characterData.m_eHeroType;
        characterData.originStat.m_nLevel = heroTbData.m_nLevel;
        characterData.originStat.m_nHp = heroTbData.m_nHp;
        characterData.originStat.m_nMp = heroTbData.m_nMp;
        characterData.originStat.m_nPow = heroTbData.m_nPow;
        characterData.originStat.m_nDef = heroTbData.m_nDef;
        characterData.originStat.m_fAtkSpeed = heroTbData.m_fAtkSpeed;
        characterData.originStat.m_fMoveSpeed = heroTbData.m_fMoveSpeed;
        characterData.originStat.m_fJumpSpeed = heroTbData.m_fJumpSpeed;
        characterData.originStat.m_fGravity = heroTbData.m_fGravity;
        characterData.originStat.m_fCriticalRate = heroTbData.m_fCriticalRate;
        characterData.originStat.m_fAvoidRate = heroTbData.m_fAvoidRate;
        characterData.nowStat.Copy(characterData.originStat);
    }
}
