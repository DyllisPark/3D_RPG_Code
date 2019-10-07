using UnityEngine;
using UnityEngine.UI;
using Global_Define;
using AI_BehaviourTree;

public partial  class Monster : MonoBehaviour, IInteraction
{
    #region INSPECTOR

    public Animator anim;
    public eMonster     m_eMonster;
    public bool         m_bDeadState { get { return monsterInfo.m_nHp <= 0; } }
    public bool         m_bAggressiveState = false;
    public MonsterInfo  monsterInfo;
    public MonsterHPBar monsterHpBar = null;
    public Image        hpBarImg = null;
    public Vector3      hpBarAddPos = new Vector3(-60, 200f, 0);
    public Vector3      randomPatrolPos = Vector3.zero;
    public Transform    hpBarPos;
    public Transform    attackedSkillPos;
    public RenderShader rimLightShader;

    public MonsterPatrol    monsterPatrol;
    public InteractionType  m_eIteractionType { get { return InteractionType.MonsterDrop; } }
    public eLifeAction      m_eLifeAction = eLifeAction.PickUp;
    public bool             m_bEnable { get; set; }    
    public string           m_sInteractionName { get; set; }
    public string           m_sInterPlayerAnim { get; private set; } = null;
    public float            m_fHpRate { get { return monsterInfo.m_nHp / m_fMaxHp; } }

    protected Root ai;
    protected Hero      targetHero;
    protected float     m_fMaxHp;
    protected float     m_fAtkTime = 0;
    protected float     m_fPatrolTime = 0;
    protected bool      m_bAIState = true;
    protected bool      m_bPatrolState = false;
    protected bool      m_bRetrunSpawnPosState = false;    
    protected Vector3   spawnPos = Vector3.zero;
    protected Vector3   patrolPos = Vector3.zero;
    protected readonly int[] m_nArrRandomActionPercent = new int[] { 90, 10 };

    #endregion

    protected void Start()
    {
        targetHero = GameMng.Ins.hero;
        MonsterInit();        
        m_sInteractionName = m_eLifeAction.ToDesc();
        m_sInterPlayerAnim = m_eLifeAction.ToString();
    }

    protected void DoUpdate()
    {
        if (m_bAIState)
        {
            ai.Tick();
            m_fAtkTime += Time.deltaTime;
            m_fPatrolTime += Time.deltaTime;
        }
        else
        {
            //몬스터 죽었을 때 루팅 활성화 인터렉션.
            if (Define.IsInRange(transform.position, targetHero.transform.position, 2f))
            {
                if (m_bEnable == false)
                {
                    m_bEnable = true;
                    targetHero.Interaction(this, this);
                    ShowInter();
                }
            }
            else
            {
                if (targetHero.interactionMonster == this)
                    NonShowInter();
                m_bEnable = false;
            }
        }

        //몬스터 HP UI 위치 조정.
        if (monsterHpBar != null && hpBarImg != null)
        {
            var monsterScreenPos = Camera.main.WorldToScreenPoint(hpBarPos.position);
            if (Define.IsInRange(targetHero.transform.position, hpBarPos.position, 20f))
            {
                monsterHpBar.gameObject.SetActive(true);
                monsterHpBar.transform.position = new Vector3(monsterScreenPos.x, monsterScreenPos.y, 0);
            }
            else
            {
                monsterHpBar.gameObject.SetActive(false);
            }
        }
    }

    void MonsterInit()
    {
        monsterInfo = new MonsterInfo();
        MonsterData data = m_eMonster.GetMonsterTb();
        monsterInfo.SetMonsterInfo
            (
                m_eMonster,
                data.m_eMonsterType,
                data.m_eMonsterAtkType,
                data.m_nHp,
                data.m_nPow,
                data.m_nDef,
                data.m_fMoveSpeed,
                data.m_fAtkSpeed,
                data.m_fChaseRange,
                data.m_fAtkRange,
                data.m_fEvasionRange,
                data.m_fMaximumMoveRange,
                data.m_fPatrolRandomXPos,
                data.m_fPatrolRandomZPos,
                data.m_fGiveExp,
                data.m_eDropMaterialItem,
                data.m_sRandomDropItems,
                data.m_nDropItemCount,
                data.m_fPossiblePatrolTime,
                data.m_fEvasionTime,
                data.m_fBulletSpeed,
                data.m_eRangedMonsterBullet,
                data.m_eRangedMonsterExplosion,
                data.m_sMonsterName,
                data.m_fMeleeAtkRange,
                data.m_fRangedAtkRange
            );
        m_fMaxHp = monsterInfo.m_nHp;
        randomPatrolPos = monsterPatrol.randomPatrolPos;
    }

    public void ShowInter()
    {
        UIMng.Ins.Interactiontxt.text = string.Format("[F] {0}", m_eIteractionType.ToDesc());
        UIMng.Ins.Interactiontxt.gameObject.SetActive(true);
    }

    public void ActionKey()
    {
        UIMng.Ins.dropItemUI.ActivateDropItem(this);
        UIMng.Ins.dropItemUI.MultiDropItemUIUpdate(monsterInfo.m_sRandomDropItems, monsterInfo.m_nDropItemCount);
    }

    public void EndInter()
    {
        targetHero.m_bAllActivateState = true;
    }

    public void NonShowInter()
    {
        UIMng.Ins.Interactiontxt.gameObject.SetActive(false);
        m_bEnable = false;
    }
}
