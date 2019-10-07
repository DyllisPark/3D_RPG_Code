using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class WeaponTable : Table<int, WeaponData> { }

public class WeaponData : TableBase<int>
{

    // TableBase
    public override int key => m_nId;
    public override eTable eTb => eTable.Weapon;
    public override string sFileName => "Weapon.json";

    // 데이터
    public int          m_nId           { get; private set; }
    public eWeaponType  m_eWeaponType   { get; private set; }
    public int          m_nHp           { get; private set; }
    public int          m_nPow          { get; private set; }
    public float        m_fAtkSpeed     { get; private set; }
    public float        m_fCriticalRate { get; private set; }
    public float        m_fXPos         { get; private set; }
    public float        m_fYPos         { get; private set; }
    public float        m_fZPos         { get; private set; }
    public float        m_fXRot         { get; private set; }
    public float        m_fYRot         { get; private set; }
    public float        m_fZRot         { get; private set; }
    public float        m_fScale        { get; private set; }
    public string       m_sItemName     { get; private set; }

    public WeaponData() { }

    public WeaponData(string a_sId, string a_sWeaponType, string a_sHp, string a_sPow, string a_sAtkSpeed, string a_sCriticalRate, string a_sXPos,
        string a_sYPos, string a_sZPos, string a_sXRot, string a_sYRot, string a_sZRot, string a_sScale, string a_sItemName)
    {
        SetData(a_sId, a_sWeaponType, a_sHp, a_sPow, a_sAtkSpeed, a_sCriticalRate, a_sXPos, a_sYPos, a_sZPos, a_sXRot, a_sYRot, a_sZRot, a_sScale, a_sItemName);
    }

    public void SetData(string a_sId, string a_sWeaponType, string a_sHp, string a_sPow, string a_sAtkSpeed, string a_sCriticalRate, string a_sXPos,
        string a_sYPos, string a_sZPos, string a_sXRot, string a_sYRot, string a_sZRot, string a_sScale, string a_sItemName)
    {
        m_nId           = int.Parse(a_sId);
        m_eWeaponType   = (eWeaponType)int.Parse(a_sWeaponType);
        m_nHp           = int.Parse(a_sHp);
        m_nPow          = int.Parse(a_sPow);
        m_fAtkSpeed     = float.Parse(a_sAtkSpeed);
        m_fCriticalRate = float.Parse(a_sCriticalRate);
        m_fXPos         = float.Parse(a_sXPos);
        m_fYPos         = float.Parse(a_sYPos);
        m_fZPos         = float.Parse(a_sZPos);
        m_fXRot         = float.Parse(a_sXRot);
        m_fYRot         = float.Parse(a_sYRot);
        m_fZRot         = float.Parse(a_sZRot);
        m_fScale        = float.Parse(a_sScale);
        m_sItemName     = a_sItemName;
    }

    public WeaponData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9], s[10], s[11], s[12], s[13]);
    }
}
