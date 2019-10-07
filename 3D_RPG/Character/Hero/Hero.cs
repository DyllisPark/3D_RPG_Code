using System.Collections.Generic;
using UnityEngine;
using Global_Define;
using System;

public partial class Hero : MonoBehaviour
{
    #region INSPECTOR

    public Animator             anim;    
    public CharacterController  characterCtrl;
    public GameObject           weaponRoot;
    public GameObject           shieldRoot;
    public Transform            nontargetSkillTrans;
    public Transform            targetSkillTrans;
    public Transform            leftHandTrans;
    public Transform            rightHandTrans;
    public TrailRenderer        weaponTrailRenderer = null;

    public CharacterParts           myCharacterParts;
    public SelectedCharacterData    characterData;
    public Monster                  interactionMonster = null;
    public Monster                  targetMonster = null;

    public eHeroState       m_eHeroState;
    public IInteraction     m_IInteraction = null;
    public bool             m_bAllActivateState = true;
    public float            m_fCurrentHp = 0;
    public float            m_fCurrentMp = 0;
    public float            m_fHpRate { get { return (characterData.nowStat.m_nHp - m_fCurrentHp) / characterData.nowStat.m_nHp; } }
    public float            m_fMpRate { get { return -(m_fCurrentMp/characterData.nowStat.m_nMp); } }
    //public float m_fExpRate { get { return  } }

    GameObject  leftHandEffect;
    GameObject  rightHandEffect;
    Vector3     m_vcMoveDir = Vector3.zero;
    Transform   cameraVec;
    KeyCode     nowKeyCode = new KeyCode();

    Dictionary<eHeroState, GameObject>  m_dicHandEffect  = new Dictionary<eHeroState, GameObject>();
    Dictionary<eHeroState, GameObject>  m_dicSkillEffect = new Dictionary<eHeroState, GameObject>();
    Dictionary<KeyCode, Action>         m_dicKeyCode;
    HeroData    heroTbData;

    bool        m_bMoveState = true;
    bool        m_bSkillState = true;
    float       m_fRegenerateHpMpTime = 0;


    #endregion

    void Start()
    {
        heroTbData = characterData.m_eHero.GetHeroTb();
        m_fCurrentHp = characterData.nowStat.m_nHp;
        m_fCurrentMp = characterData.nowStat.m_nMp;
        UIMng.Ins.mainUI.MainUIInit();
        cameraVec = GameObject.FindGameObjectWithTag("CameraVector").transform;
        SetKeyCode();
    }

    void SetKeyCode()
    {
        m_dicKeyCode = new Dictionary<KeyCode, Action>()
        {
            { KeyCode.Alpha1, () => { NontargetSkillActivate(eHeroState.NontargetSkill1); } },
            { KeyCode.Alpha2, () => { NontargetSkillActivate(eHeroState.NontargetSkill2); } },
            { KeyCode.Alpha3, () => { NontargetSkillActivate(eHeroState.NontargetSkill3); } },
            { KeyCode.Alpha4, () => { NontargetSkillActivate(eHeroState.NontargetSkill4); } },
            { KeyCode.Alpha5, () => { NontargetSkillActivate(eHeroState.NontargetSkill5); } },
            { KeyCode.Alpha6, () => { NontargetSkillActivate(eHeroState.NontargetSkill6); } },
            { KeyCode.Alpha7, () => { NontargetSkillActivate(eHeroState.NontargetSkill7); } },
            { KeyCode.Alpha8, () => { NontargetSkillActivate(eHeroState.NontargetSkill8); } },

            { KeyCode.F1, () => { TargetSkillActivate(eHeroState.TargetSkill1); } },
            { KeyCode.F2, () => { TargetSkillActivate(eHeroState.TargetSkill2); } },
            { KeyCode.F3, () => { TargetSkillActivate(eHeroState.TargetSkill3); } },
            { KeyCode.F4, () => { TargetSkillActivate(eHeroState.TargetSkill4); } },
            { KeyCode.F5, () => { TargetSkillActivate(eHeroState.TargetSkill5); } },

            { KeyCode.Z, () => { AvoidSkillActivate(eHeroState.DodgeLeft); } },
            { KeyCode.X, () => { AvoidSkillActivate(eHeroState.DodgeRight); } },

            { KeyCode.I, () => { UIMng.Ins.inventoryUI.gameObject.ActiveChange(); } },
            { KeyCode.C, () => { UIMng.Ins.characterStateUI.gameObject.ActiveChange(); } },
            { KeyCode.M, () => { UIMng.Ins.mapUI.gameObject.ActiveChange(); } },
            { KeyCode.F, () => { InteractionAction(); } },
            { KeyCode.Q, () => { UIMng.Ins.questList_ExpantionUI.gameObject.ActiveChange(); } },
            { KeyCode.Mouse0, () => { MonsterTargetting(); } },
        };
    }

    void Update()
    {
        if(m_bAllActivateState)
        {
            if (m_bMoveState)
                Move();
            if (m_bSkillState)
            {
                ActivateFromInputKey();
            }
        }
        HpMpRegenerate();
    }

    

    public void HeroDataInit(GameObject a_objHeroRoot)
    {
        characterData = DataMng.Ins.nowSelectedCharacterData;
        myCharacterParts.SetCustomizing(characterData);
    }
}