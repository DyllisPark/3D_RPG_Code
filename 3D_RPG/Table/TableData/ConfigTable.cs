using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;


public class ConfigTable : Table<string, ConfigData> { }

public class ConfigData : TableBase<string>
{

    // TableBase
    public override string key => sConfigID;
    public override eTable eTb => eTable.Config;
    public override string sFileName => "Config.json";

    // 데이터
    public string sConfigID { get; private set; } //Version
    public float fValue { get; private set; } //실제 버전 값

    public ConfigData() { }

    public ConfigData(string a_sId, string a_sValue)
    {
        SetData(a_sId, a_sValue);
    }

    public ConfigData(string[] s)
    {
    	SetData(s);
    }

    public void SetData(string a_sId, string a_sValue)
    {
        sConfigID = a_sId;
        fValue = float.Parse(a_sValue);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1]);
    }
}
