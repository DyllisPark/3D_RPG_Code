using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class HeroTable : Table<eHero, HeroData> { }

public class HeroData : TableBase<eHero>
{

    // TableBase
    public override eHero key           => m_eId;
    public override eTable eTb          => eTable.Hero;
    public override string sFileName    => "Hero.json";

    // 데이터
    public eHero     m_eId             { get; private set; }
    public eHeroType m_eHeroType       { get; private set; }
    public int       m_nLevel          { get; private set; }
    public float     m_fExp            { get; private set; }
    public int       m_nHp             { get; private set; }
    public int       m_nMp             { get; private set; }
    public int       m_nPow            { get; private set; }
    public int       m_nDef            { get; private set; }
    public float     m_fAtkSpeed       { get; private set; }
    public float     m_fMoveSpeed      { get; private set; }
    public float     m_fJumpSpeed      { get; private set; }
    public float     m_fGravity        { get; private set; }
    public float     m_fCriticalRate   { get; private set; }
    public float     m_fAvoidRate      { get; private set; }
    public int       m_nRegenerateHp   { get; private set; }
    public int       m_nRegenerateMp   { get; private set; }


    public HeroData() { }

    public HeroData(string a_sId, string a_sHeroType, string a_sLevel, string a_sExp, string a_sHp, string a_sMp, string a_sPow, string a_sDef, string a_sAtkSpeed,
        string a_sMoveSpeed, string a_sJumpSpeed, string a_sGravity, string a_sCriticalRate, string a_fAvoidRate, string a_sRegenerateHp, string a_sRegenerateMp)
    {
        SetData(a_sId, a_sHeroType, a_sLevel, a_sExp, a_sHp, a_sMp, a_sPow, a_sDef, a_sAtkSpeed, a_sMoveSpeed, a_sJumpSpeed, a_sGravity, a_sCriticalRate, a_fAvoidRate, a_sRegenerateHp, a_sRegenerateMp);
    }

    public void SetData(string a_sId, string a_sHeroType, string a_sLevel, string a_sExp, string a_sHp, string a_sMp, string a_sPow, string a_sDef, string a_sAtkSpeed,
        string a_sMoveSpeed, string a_sJumpSpeed, string a_sGravity, string a_sCriticalRate, string a_fAvoidRate, string a_sRegenerateHp, string a_sRegenerateMp)
    {
        m_eId             = (eHero)int.Parse(a_sId);
        m_eHeroType       = (eHeroType)int.Parse(a_sHeroType);
        m_nLevel          = int.Parse(a_sLevel);
        m_fExp            = float.Parse(a_sExp);
        m_nHp             = int.Parse(a_sHp);
        m_nMp             = int.Parse(a_sMp);
        m_nPow            = int.Parse(a_sPow);
        m_nDef            = int.Parse(a_sDef);
        m_fAtkSpeed       = float.Parse(a_sAtkSpeed);
        m_fMoveSpeed      = float.Parse(a_sMoveSpeed);
        m_fJumpSpeed      = float.Parse(a_sJumpSpeed);
        m_fGravity        = float.Parse(a_sGravity);
        m_fCriticalRate   = float.Parse(a_sCriticalRate);
        m_fAvoidRate      = float.Parse(a_fAvoidRate);
        m_nRegenerateHp   = int.Parse(a_sRegenerateHp);
        m_nRegenerateMp   = int.Parse(a_sRegenerateMp);
    }

    public HeroData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9], s[10], s[11], s[12], s[13], s[14], s[15]);
    }
}
