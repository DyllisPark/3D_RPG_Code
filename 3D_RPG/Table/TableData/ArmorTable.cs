using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class ArmorTable : Table<int, ArmorData> { }

public class ArmorData : TableBase<int>
{

    // TableBase
    public override int key => m_nId;
    public override eTable eTb => eTable.Armor;
    public override string sFileName => "Armor.json";

    // 데이터
    public int          m_nId               { get; private set; }
    public eArmorType   m_eArmorType        { get; private set; }
    public int          m_nHp               { get; private set; }
    public int          m_nMp               { get; private set; }
    public int          m_nDef              { get; private set; }
    public float        m_fCriticalRate     { get; private set; }
    public float        m_fAvoidRate        { get; private set; }
    public string       m_sItemName         { get; private set; }

    public ArmorData() { }

    public ArmorData(string a_sId, string a_sArmorType, string a_sHp, string a_sMp, string a_sDef, string a_sCriticalRate, string a_fAvoidRate, string a_sItemName)
    {
        SetData(a_sId, a_sArmorType, a_sHp, a_sMp, a_sDef, a_sCriticalRate, a_fAvoidRate, a_sItemName);
    }

    public void SetData(string a_sId, string a_sArmorType, string a_sHp, string a_sMp, string a_sDef, string a_sCriticalRate, string a_fAvoidRate, string a_sItemName)
    {
        m_nId               = int.Parse(a_sId);
        m_eArmorType        = (eArmorType)int.Parse(a_sArmorType);
        m_nHp               = int.Parse(a_sHp);
        m_nMp               = int.Parse(a_sMp);
        m_nDef              = int.Parse(a_sDef);
        m_fCriticalRate     = float.Parse(a_sCriticalRate);
        m_fAvoidRate        = float.Parse(a_fAvoidRate);
        m_sItemName         = a_sItemName;
    }

    public ArmorData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7]);
    }
}
