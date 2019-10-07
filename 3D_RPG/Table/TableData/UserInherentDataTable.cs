using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class UserInherentDataTable : Table<eUserID, UserInherentData> { }

public class UserInherentData : TableBase<eUserID>
{

    // TableBase
    public override eUserID key => m_eId;
    public override eTable eTb => eTable.UserInherentData;
    public override string sFileName => "User.json";

    // 데이터
    public eUserID  m_eId           { get; private set; }
    public int      m_nGold         { get; private set; }
    public eGender  m_eGender       { get; private set; }
    public eWeapon  m_eWeapon       { get; private set; }

    public UserInherentData() { }

    public UserInherentData(string a_sId, string a_sGold, string a_sGender, string a_sWeapon)
    {
        SetData(a_sId, a_sGold, a_sGender, a_sWeapon);
    }

    public void SetData(string a_sId, string a_sGold, string a_sGender, string a_sWeapon)
    {
        m_eId       = (eUserID)int.Parse(a_sId);
        m_nGold     = int.Parse(a_sGold);
        m_eGender   = (eGender)int.Parse(a_sGender);
        m_eWeapon   = (eWeapon)int.Parse(a_sWeapon);
    }

    public UserInherentData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3]);
    }
}
