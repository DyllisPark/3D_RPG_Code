using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class HeroActiveSkillTable : Table<eHeroState, HeroActiveSkillData> { }

public class HeroActiveSkillData : TableBase<eHeroState>
{

    // TableBase
    public override eHeroState key => m_eId;
    public override eTable eTb => eTable.HeroActiveSkill;
    public override string sFileName => "HeroActiveSkill.json";

    // 데이터
    public eHeroState       m_eId           { get; private set; }
    public float            m_fMaintainTime { get; private set; }
    public int              m_nSkillPow     { get; private set; }
    public float            m_fSkillRadius  { get; private set; }
    public int              m_nSkillUsedMp  { get; private set; }
    public HeroActiveSkillData() { }

    public HeroActiveSkillData(string a_sId, string a_sMaintainTime, string a_sSkillPow, string a_sSkillRadius, string a_sSkillUsedMp)
    {
        SetData(a_sId, a_sMaintainTime, a_sSkillPow, a_sSkillRadius, a_sSkillUsedMp);
    }

    public void SetData(string a_sId, string a_sMaintainTime, string a_sSkillPow, string a_sSkillRdius, string a_sSkillUsedMp)
    {
        m_eId               = (eHeroState)int.Parse(a_sId);
        m_fMaintainTime     = float.Parse(a_sMaintainTime);
        m_nSkillPow         = int.Parse(a_sSkillPow);
        m_fSkillRadius      = float.Parse(a_sSkillRdius);
        m_nSkillUsedMp      = int.Parse(a_sSkillUsedMp);
    }

    public HeroActiveSkillData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4]);
    }
}
