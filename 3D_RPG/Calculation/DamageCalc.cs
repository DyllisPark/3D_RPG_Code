using UnityEngine;
using UnityEngine.UI;
using Global_Define;
using DG.Tweening;

//몬스터<->히어로 데미지 계산
public class DamageCalc
{
    bool        m_bCriticalState = false;
    bool        m_bAvoidState = false;
    const float fRANDOMDAMAGE_YPOS = 300f;    
    const float fRANDOMDAMAGE_ADD_YPOS = 1f;
    const float fHERO_RANDOMDAMAGE_ADD_YPOS= 1.2f;
    const float fHERO_RANDOMDAMAGE_MOVE_YPOS= 100f;
    const float fPOW_RANDOMRANGE = 0.1f;
    const float fCRITICAL_POWUP = 1.5f;

    public DamageCalc() { }

    public void HitToMonster(Hero a_hero, Monster a_monster, int a_nSkillPow)
    {
        HeroStat heroStat = a_hero.characterData.nowStat;
        MonsterInfo monsterStat = a_monster.monsterInfo;

        int nHeroPow = (int)Random.Range(heroStat.m_nPow * (1 - fPOW_RANDOMRANGE), heroStat.m_nPow * (1 + fPOW_RANDOMRANGE));

        if(Rand.Percent((int)heroStat.m_fCriticalRate))
        {
            nHeroPow = (int)(nHeroPow * fCRITICAL_POWUP);
            m_bCriticalState = true;
        }
        nHeroPow += a_nSkillPow;        
        monsterStat.m_nHp -= (nHeroPow - monsterStat.m_nDef);

        SetMonsterDamageText(nHeroPow, a_monster, m_bCriticalState);
        m_bCriticalState = false;
    }

    public void HitToHero(Monster a_monster, Hero a_hero)
    {
        HeroStat heroStat = a_hero.characterData.nowStat;
        MonsterInfo monsterStat = a_monster.monsterInfo;

        int nMonsterPow = (int)Random.Range(monsterStat.m_nPow * 0.9f, monsterStat.m_nPow * 1.1f);

        if(Rand.Percent((int)heroStat.m_fAvoidRate))
        {
            m_bAvoidState = true;
        }

        if(m_bAvoidState == false)
        {
            a_hero.m_fCurrentHp -= (nMonsterPow - heroStat.m_nDef);
            UIMng.Ins.mainUI.HpRateUpdate();
        }
        SetHeroDamageText(nMonsterPow, a_hero, m_bAvoidState);
        
        m_bAvoidState = false;
    }

    void SetMonsterDamageText(int a_nDamage, Monster a_monster, bool a_bCriticalState)
    {
        Vector3 damageMovePos = new Vector3(0, Random.Range(fRANDOMDAMAGE_YPOS * 0.8f, fRANDOMDAMAGE_YPOS * 1.2f), 0);
        Vector3 damageAddPos = new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(fRANDOMDAMAGE_ADD_YPOS * 0.8f, fRANDOMDAMAGE_ADD_YPOS * 1.2f), 0);

        Text damagetxt = DamageMng.Ins.GetDamageText();
        damagetxt.color = Define.monsterDamageTextColorInit;

        if (a_bCriticalState)
        {
            damagetxt.color = Define.monsterDamageTextCriticalColor;
            damagetxt.text = string.Format("치명타 {0}", a_nDamage);
        }
        else
            damagetxt.text = string.Format("{0}", a_nDamage);

        var MonsterScreenPos = Camera.main.WorldToScreenPoint(a_monster.transform.position + damageAddPos);
        damagetxt.transform.position = MonsterScreenPos;
             
        damagetxt.transform.DOMove(MonsterScreenPos + damageMovePos, 1f)
            .OnComplete(() => DeActivateDamagetxt(damagetxt));
        a_monster.hpBarImg.fillAmount = a_monster.m_fHpRate;
        
        if(GameMng.Ins.hero.targetMonster == a_monster)
        {
            UIMng.Ins.targettedMonsterUI.TargettedMonsterHPUpdate(a_monster);
        }
    }

    
    void SetHeroDamageText(int a_nDamage, Hero a_hero, bool a_bAvoidState)
    {
        Vector3 damageMovePos = new Vector3(0, Random.Range(fHERO_RANDOMDAMAGE_MOVE_YPOS * 0.8f, fHERO_RANDOMDAMAGE_MOVE_YPOS * 1.2f), 0);
        Vector3 damageAddPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(fHERO_RANDOMDAMAGE_ADD_YPOS * 0.8f, fHERO_RANDOMDAMAGE_ADD_YPOS * 1.2f), 0);

        Text damagetxt = DamageMng.Ins.GetDamageText();
        damagetxt.color = Define.heroDamageTextColorInit;

        if (a_bAvoidState)
            damagetxt.text = string.Format("{0}", "회피");
        else
            damagetxt.text = string.Format("{0}", a_nDamage);

        var heroScreenPos = Camera.main.WorldToScreenPoint(a_hero.transform.position + damageAddPos);
        damagetxt.transform.position = heroScreenPos;

        damagetxt.transform.DOMove(heroScreenPos + damageMovePos, 1f)
           .OnComplete(() => DeActivateDamagetxt(damagetxt));
    }

    void DeActivateDamagetxt(Text a_damagetxt)
    {
        a_damagetxt.gameObject.SetActive(false);
        --DamageMng.Ins.m_nUsingDamageCount;
    }
}
