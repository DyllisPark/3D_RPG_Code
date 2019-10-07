using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class MonsterTable : Table<eMonster, MonsterData> { }

public class MonsterData : TableBase<eMonster>
{
    // TableBase
    public override eMonster key => m_eId;
    public override eTable eTb => eTable.Monster;
    public override string sFileName => "Monster.json";

    // 데이터
    public eMonster                 m_eId                       { get; private set; }
    public eMontserType             m_eMonsterType              { get; private set; }
    public eMonsterAtkType          m_eMonsterAtkType           { get; private set; }
    public int                      m_nHp                       { get; private set; }
    public int                      m_nPow                      { get; private set; }
    public int                      m_nDef                      { get; private set; }
    public float                    m_fAtkSpeed                 { get; private set; }
    public float                    m_fMoveSpeed                { get; private set; }
    public float                    m_fChaseRange               { get; private set; }
    public float                    m_fAtkRange                 { get; private set; }
    public float                    m_fEvasionRange             { get; private set; }
    public float                    m_fMaximumMoveRange         { get; private set; }
    public float                    m_fPatrolRandomXPos         { get; private set; }
    public float                    m_fPatrolRandomZPos         { get; private set; }
    public float                    m_fGiveExp                  { get; private set; }
    public eItemID                  m_eDropMaterialItem         { get; private set; }
    public string                   m_sRandomDropItems          { get; private set; }
    public int                      m_nDropItemCount            { get; private set; }
    public float                    m_fPossiblePatrolTime       { get; private set; }
    public float                    m_fEvasionTime              { get; private set; }
    public float                    m_fBulletSpeed              { get; private set; }
    public eRangedMonsterBullet     m_eRangedMonsterBullet      { get; private set; }
    public eRangedMonsterExplosion  m_eRangedMonsterExplosion   { get; private set; }
    public string                   m_sMonsterName              { get; private set; }
    public float                    m_fMeleeAtkRange            { get; private set; }
    public float                    m_fRangedAtkRange           { get; private set; }
    //public int              m_nDropGold_Min     { get; private set; }
    //public int              m_nDropGold_Max     { get; private set; }

    public MonsterData() { }

    public MonsterData(string a_sId, string a_sMontserType, string a_sMontserAtkType, string a_sHp, string a_sPow, string a_sDef, string a_sAtkSpeed, string a_sMoveSpeed,
        string a_sChaseRange, string a_sAtkRange, string a_sEvasionRange, string a_sMaximumMoveRange, string a_sPatrolRandomXPos, string a_sPatrolRandomZPos, string a_sGiveExp,
        string a_sDropMaterialItem, string a_sRandomDropItems, string a_sDropItemCount, string a_sPossiblePatrolTime, string a_sEvasionTime, string a_sBulletSpeed,
        string a_sRangedMonsterBullet, string a_sRangedMonsterExplosion, string a_sMonsterName, string a_sMeleeAtkRange, string a_sRangedAtkRange)
    {
        SetData(a_sId, a_sMontserType, a_sMontserAtkType, a_sHp, a_sPow, a_sDef, a_sAtkSpeed, a_sMoveSpeed, a_sChaseRange, a_sAtkRange, a_sEvasionRange,
            a_sMaximumMoveRange, a_sPatrolRandomXPos, a_sPatrolRandomZPos, a_sGiveExp, a_sDropMaterialItem, a_sRandomDropItems, a_sDropItemCount, 
            a_sPossiblePatrolTime, a_sEvasionTime, a_sBulletSpeed, a_sRangedMonsterBullet, a_sRangedMonsterExplosion, a_sMonsterName, a_sMeleeAtkRange, a_sRangedAtkRange);
    }

    public void SetData(string a_sId, string a_sMontserType, string a_sMontserAtkType, string a_sHp, string a_sPow, string a_sDef, string a_sAtkSpeed, string a_sMoveSpeed,
        string a_sChaseRange, string a_sAtkRange, string a_sEvasionRange, string a_sMaximumMoveRange, string a_sPatrolRandomXPos, string a_sPatrolRandomZPos, string a_sGiveExp,
        string a_sDropMaterialItem, string a_sRandomDropItems, string a_sDropItemCount, string a_sPossiblePatrolTime, string a_sEvasionTime, string a_sBulletSpeed,
        string a_sRangedMonsterBullet, string a_sRangedMonsterExplosion, string a_sMonsterName, string a_sMeleeAtkRange, string a_sRangedAtkRange)
    {
        m_eId                       = (eMonster)int.Parse(a_sId);
        m_eMonsterType              = (eMontserType)int.Parse(a_sMontserType);
        m_eMonsterAtkType           = (eMonsterAtkType)int.Parse(a_sMontserAtkType);
        m_nHp                       = int.Parse(a_sHp);        
        m_nPow                      = int.Parse(a_sPow);
        m_nDef                      = int.Parse(a_sDef);
        m_fAtkSpeed                 = float.Parse(a_sAtkSpeed);
        m_fMoveSpeed                = float.Parse(a_sMoveSpeed);
        m_fChaseRange               = float.Parse(a_sChaseRange);
        m_fAtkRange                 = float.Parse(a_sAtkRange);
        m_fEvasionRange             = float.Parse(a_sEvasionRange);
        m_fMaximumMoveRange         = float.Parse(a_sMaximumMoveRange);
        m_fPatrolRandomXPos         = float.Parse(a_sPatrolRandomXPos);
        m_fPatrolRandomZPos         = float.Parse(a_sPatrolRandomZPos);
        m_fGiveExp                  = float.Parse(a_sGiveExp);
        m_eDropMaterialItem         = (eItemID)int.Parse(a_sDropMaterialItem);
        m_sRandomDropItems          = a_sRandomDropItems;
        m_nDropItemCount            = int.Parse(a_sDropItemCount);
        m_fPossiblePatrolTime       = float.Parse(a_sPossiblePatrolTime);
        m_fEvasionTime              = float.Parse(a_sEvasionTime);
        m_fBulletSpeed              = float.Parse(a_sBulletSpeed);
        m_eRangedMonsterBullet      = (eRangedMonsterBullet)int.Parse(a_sRangedMonsterBullet);
        m_eRangedMonsterExplosion   = (eRangedMonsterExplosion)int.Parse(a_sRangedMonsterExplosion);
        m_sMonsterName              = a_sMonsterName;
        m_fMeleeAtkRange            = float.Parse(a_sMeleeAtkRange);
        m_fRangedAtkRange           = float.Parse(a_sRangedAtkRange);
    }

    public MonsterData(string[] s)
    {
        SetData(s);
    }

    public override void SetData(string[] s)
    {
        SetData(s[0], s[1], s[2], s[3], s[4], s[5], s[6], s[7], s[8], s[9], s[10], s[11]
            , s[12], s[13], s[14], s[15], s[16], s[17], s[18], s[19], s[20], s[21], s[22], s[23], s[24], s[25]);
    }
}
