using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class QuestMng : MonoBehaviour
{
    #region SINGLETON
    static QuestMng _instance = null;

    public static QuestMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(QuestMng)) as QuestMng;
                if (_instance == null)
                {
                    _instance = new GameObject("QuestMng", typeof(QuestMng)).GetComponent<QuestMng>();
                }
            }

            return _instance;
        }
    }


    #endregion

    #region INSEPCTOR
    
    public MeshFilter   questMark;
    public MeshFilter   miniMapQuestMark;
    public MeshFilter   receiveQuestMark;
    public MeshFilter   completeQuestMark;

    public NPC[] NPCs;
    public NPC nowQuestNPC;

    public Dictionary<eItemID, int>     m_dicCompleteQuestPurpose_Gain = new Dictionary<eItemID, int>();
    public Dictionary<eMonster, int>    m_dicCompleteQuestPurpose_Kill = new Dictionary<eMonster, int>();
    public Dictionary<eItemID, int>     m_dicNowQuestPurpose_Gain = new Dictionary<eItemID, int>();
    public Dictionary<eMonster, int>    m_dicNowQuestPurpose_Kill = new Dictionary<eMonster, int>();    

    QuestData   questData;
    eQuest      m_eQuest;
    bool        m_bQuestNPCState = false;

    #endregion


    public void QuestListInit()
    {
        var questList = GameMng.Ins.hero.characterData.m_liQuestList;
        foreach (var quest in questList)
        {
            UIMng.Ins.questList_ExpantionUI.AddQuest(quest);
        }
        if(questList.Count != 0&& questList[questList.Count - 1].GetQuestTb().m_eQuestState != eQuestState.QuestComplete)
            UIMng.Ins.questList_ReductionUI.QuestList_ReductionUIInit(questList[questList.Count - 1]);
    }

    //퀘스트의 상태가 변할 때 활성화될 엔피씨 갱신.
    public void QuestNPCActivate()
    {
        m_eQuest = GameMng.Ins.hero.characterData.m_eQuest;
        questData = m_eQuest.GetQuestTb();
        for(int i = 0; i < NPCs.Length; ++i)
        {
            if(NPCs[i].m_eNPCID == questData.m_eNPCID)
            {
                m_bQuestNPCState = true;
                nowQuestNPC = NPCs[i];
                questMark.gameObject.transform.parent = NPCs[i].transform;
                questMark.transform.localPosition = new Vector3(0, 2.5f, 0);
                miniMapQuestMark.transform.position = NPCs[i].transform.position + new Vector3(0, 15f, 0);
                
                questMark.mesh = receiveQuestMark.mesh;
                miniMapQuestMark.mesh = receiveQuestMark.mesh;
                break;
            }
        }
        if(m_bQuestNPCState == false)
        {
            questMark.gameObject.SetActive(false);
            miniMapQuestMark.gameObject.SetActive(false);
        }
        if (GameMng.Ins.hero.characterData.m_eQuest.GetQuestTb().m_eQuestState == eQuestState.QuestComplete)
        {
            QuestNPCUpdate();
        }
        m_bQuestNPCState = false;
    }

    public void QuestNPCUpdate()
    {
        questMark.mesh = completeQuestMark.mesh;
        miniMapQuestMark.mesh = completeQuestMark.mesh;
    }

    public IEnumerator QuestMarkRoatate()
    {
        while (true)
        {
            questMark.gameObject.transform.Rotate(Vector3.up);
            yield return null;
        }
    }

    //퀘스트 종료시에 테이블에서 받은 퀘스트 완료에 필요한 아이템 리스트 삭제.
    public void QuestInfoClear()
    {
        m_dicCompleteQuestPurpose_Gain.Clear();
        m_dicNowQuestPurpose_Gain.Clear();
        m_dicCompleteQuestPurpose_Kill.Clear();
        m_dicNowQuestPurpose_Kill.Clear();
    }

    //퀘스트 데이터 갱신.
    public void AddQuestList(eQuest a_eQuest)
    {
        GameMng.Ins.hero.characterData.m_liQuestList.Add(a_eQuest);
        UIMng.Ins.questList_ExpantionUI.AddQuest(a_eQuest);
    }

    //죽은 몬스터가 퀘스트 몬스터인지 체크.
    public void QuestMonsterCheck(eMonster a_eMonster)
    {
        if (m_dicNowQuestPurpose_Kill.ContainsKey(a_eMonster) == false) return;

        ++m_dicNowQuestPurpose_Kill[a_eMonster];
        UIMng.Ins.questList_ReductionUI.QuestList_ReductionUIUpdate();
    }
}
