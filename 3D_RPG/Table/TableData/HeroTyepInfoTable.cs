using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class HeroTypeInfoTable : Table<eHeroType, HeroTypeInfoData> { }

public class HeroTypeInfoData : TableBase<eHeroType>
{

    // TableBase
    public override eHeroType key => m_eId;
    public override eTable eTb => eTable.HeroTypeInfo;
    public override string sFileName => "HeroTypeInfo.json";

    // 데이터
    public eHeroType        m_eId               { get; private set; }
    public string           m_sHeroTypeName     { get; private set; }
    public string           m_sHeroTypeExplain  { get; private set; }

    public HeroTypeInfoData() { }

    public HeroTypeInfoData(string a_sId, string a_sHeroTypeName, string a_sHeroTypeExplain)
    {
        SetData(a_sId, a_sHeroTypeName, a_sHeroTypeExplain);
    }

    public void SetData(string a_sId, string a_sHeroTypeName, string a_sHeroTypeExplain)
    {
        m_eId               = (eHeroType)int.Parse(a_sId);
        m_sHeroTypeName     = a_sHeroTypeName;
        m_sHeroTypeExplain  = a_sHeroTypeExplain;
    }

    public HeroTypeInfoData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2]);
    }
}
