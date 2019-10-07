using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;

namespace Global_Define
{

    public enum eConfig
    {
        [Description("-")]
        None = -1,
        [Description("0")]
        TableVersion,
    }   

    public enum eTable
    {
        [Description("-")]
        None = -1,

        [Description("0")]
        Config,
        [Description("1672714124")]
        UserInherentData,
        [Description("187684760")]
        Hero,
        [Description("1838101065")]
        HeroTypeInfo,
        [Description("1639687877")]
        HeroActiveSkill,
        [Description("55888390")]
        Customizing,
        [Description("32922502")]
        Monster,
        [Description("528997410")]
        Item,
        [Description("1653937630")]
        Armor,
        [Description("521370766")]
        Weapon,
        [Description("1136209943")]
        Tool,
        [Description("434764361")]
        GemItem,
        [Description("1400363045")]
        MaterialItem,
        //[Description("1969148721")]
        //ItemCombination,
        [Description("613913523")]
        Quest,
        [Description("1350931513")]
        QuestPurpose,

        Max,
    }

    public enum eUserID
    {
        None = -1,
        User = 1001,
    }
    
    public enum eCamp
    {
        None = -1,

        Angel,
        Devil,
    }

    public enum eHero
    {
        None = -1,

        Knight = 101,
        MagicKnight = 201,
    }

    public enum eHeroType
    {
        None = -1,

        Knight = 100,
        MagicKnight = 200,
    }

    public enum eHeroState
    {
        None = -1,   

        Idle = 0,

        MoveFoward,
        MoveBackward,
        
        DodgeLeft,
        DodgeRight,

        NontargetSkill1,
        NontargetSkill2,
        NontargetSkill3,
        NontargetSkill4,
        NontargetSkill5,
        NontargetSkill6,
        NontargetSkill7,
        NontargetSkill8,

        TargetSkill1,
        TargetSkill2,
        TargetSkill3,
        TargetSkill4,
        TargetSkill5,

        Felling,
        Fishing,
        Harvesting,

        PickUp,

        Potion,

        MakingSkill1,
        MakingSkill2,
        MakingSkill3,

        Death,

    }

    public enum eHeroActiveSkill
    {
        None = -1,
    }

    public enum eHeroPassiveSKill
    {
        None = -1,

        ___POW___ = 100,

        AttackRange = 110,
        AttackRange1,
        AttackRange2,
        AttackRange3,

        PowUP = 120,
        PowUP1,
        PowUP2,
        PowUP3,

        ___DEF___ = 200,

        Hp = 210,
        Hp1,
        Hp2,
        Hp3,

        DefUP = 220,
        DefUP1,
        DefUP2,
        DefUP3,

        ___SPEED___ = 300,

        DistanceUp = 310,
        DistanceUp1,
        DistanceUp2,
        DistanceUp3,

        SpeedUp = 320,
        SpeedUp1,
        SpeedUp2,
        SpeedUp3,
    }

    public enum eGender
    {
        None            = -1,
        Male,
        Female,
    }

    public enum eCustomizing
    {
        None            = -1,

        Ear             = 0,        //3개
        Hair,                       //13개
        Eyebrow,                    //7개
        FacialHair,                 //18개
        HandLeft,                   //Left, Right 각 18개
        HandRight,                  
        Head,                       //23개

        Max,
    }

    
    public enum eMonster
    {
        None = -1,

        //Big
        ElementalGolem = 10001,
        BigOrk,
        MutantGuy,
        PigButcher,
        Troll,

        //Axe
        Barbarian = 11001,
        SpiritDemon,
        Dwarf,
        RedDemon,
        MechanicalGolem,

        //Sworld
        DarkElf = 12001,
        AncientWarrior,
        Slayer,
        Mystic,
        EvilGod,
        

        //Ranged
        AncientQueen = 20001,
        Medusa,
        ForestWitch,
        ForestGuardian,

        //Boss
        FortGolem = 30001,

    }

    public enum eRangedMonsterBullet
    {
        None = -1,

        Bullet1,
        Bullet2,
        Bullet3,
    }

    public enum eRangedMonsterExplosion
    {
        None = -1,

        Explosion1,
        Explosion2,
        Explosion3,
    }

    public enum eMontserType
    {
        None = -1,

        MeleeMonster,
        RangedMonster,
        Boss,
    }

    public enum eMonsterAtkType
    {
        None = -1,
        [Description("선공")]
        Aggressive,
        [Description("비선공")]
        Passive,
    }

    public enum eBossAtkType
    {
        None = -1,

        MeleeBasicAtk1 = 1,

        RangedBasicAtk1,

        MeleeSkillAtk1,
        MeleeSkillAtk2,

        RangedSkillAtk1,
        RangedSkillAtk2,
    }

    public enum eItemID
    {
        None = -1,

        //WeaponItem
        ___WeaponCategory___ = 100,

        OneHand_Mace = 1,
        OneHand_Sworld,
        TwoHand_Axe,
        TwoHand_Spear,
        TwoHand_Staff,
        TwoHand_Sworld,
        SubHand_Shield,

        Helmet = 10,    
        Top,                        
        HelmetDeco,                 
        Bottom,                     
        ElbowLeft,                  
        ElbowRight,

        ShoulderLeft,               
        ShoulderRight,
        Cape,                       
        ArmUpperLeft,               
        ArmUpperRight,
        ArmLowerLeft,
        ArmLowerRight,
        LegLeft,                    
        LegRight,
        KneeLeft,
        KneeRight,

        OneHand_Mace1 = 101,
        OneHand_Mace2 = 111,
        OneHand_Mace3 = 121,
        OneHand_Mace4 = 131,
        OneHand_Mace5 = 141,

        OneHand_Sworld1 = 201,
        OneHand_Sworld2 = 211,
        OneHand_Sworld3 = 221,
        OneHand_Sworld4 = 231,
        OneHand_Sworld5 = 241,

        TwoHand_Axe1 = 301,
        TwoHand_Axe2 = 311,
        TwoHand_Axe3 = 321,
        TwoHand_Axe4 = 331,
        TwoHand_Axe5 = 341,

        TwoHand_Spear1 = 401,
        TwoHand_Spear2 = 411,
        TwoHand_Spear3 = 421,
        TwoHand_Spear4 = 431,
        TwoHand_Spear5 = 441,

        TwoHand_Staff1 = 501,
        TwoHand_Staff2 = 511,
        TwoHand_Staff3 = 521,
        TwoHand_Staff4 = 531,
        TwoHand_Staff5 = 541,

        TwoHand_Sworld1 = 601,
        TwoHand_Sworld2 = 611,
        TwoHand_Sworld3 = 621,
        TwoHand_Sworld4 = 631,
        TwoHand_Sworld5 = 641,

        SubHand_Shield1 = 701,
        SubHand_Shield2 = 711,
        SubHand_Shield3 = 721,
        SubHand_Shield4 = 731,
        SubHand_Shield5 = 741,


        //ArmorItem
        ___SameArmorCategory___ = 10,
        ___ArmorCategory___ = 100,

        

        Helmet1 = 1001,
        Helmet2 = 1011,
        Helmet3 = 1021,
        Helmet4 = 1031,
        Helmet5 = 1041,

        Top1 = 1101,
        Top2 = 1111,
        Top3 = 1121,
        Top4 = 1131,
        Top5 = 1141,

        HelmetDeco1 = 1201,
        HelmetDeco2 = 1211,
        HelmetDeco3 = 1221,
        HelmetDeco4 = 1231,
        HelmetDeco5 = 1241,

        Bottom1 = 1301,
        Bottom2 = 1311,
        Bottom3 = 1321,
        Bottom4 = 1331,
        Bottom5 = 1341,

        Elbow1 = 1401,      //Right는 동일한 ID로 리소스를 로딩하기 때문에 따로 Enum값이 필요 없음.
        Elbow2 = 1411,
        Elbow3 = 1421,
        Elbow4 = 1431,
        Elbow5 = 1441,

        Shoulder1 = 1501,
        Shoulder2 = 1511,
        Shoulder3 = 1521,
        Shoulder4 = 1531,
        Shoulder5 = 1541,

        Cape1 = 1601,
        Cape2 = 1611,
        Cape3 = 1621,
        Cape4 = 1631,
        Cape5 = 1641,

        Arm1 = 1701,
        Arm2 = 1711,
        Arm3 = 1721,
        Arm4 = 1731,
        Arm5 = 1741,

        Leg1 = 1801,
        Leg2 = 1811,
        Leg3 = 1821,
        Leg4 = 1831,
        Leg5 = 1841,

        Knee1 = 1901,
        Knee2 = 1911,
        Knee3 = 1921,
        Knee4 = 1931,
        Knee5 = 1941,


        //LifeItem
        Sickle      = 2001,
        Ax          = 2101,
        FishingPole = 2201,

        //GemItem
        Gem_Fire   = 3001,
        Gem_White     = 3101,
        Gem_Blue    = 3201,
        Gem_Purple   = 3301,
        Gem_Red  = 3401,
        //PotionItem
        HpPotion_Small  = 4001,
        HpPotion_Medium = 4101,
        HpPotion_Large  = 4201,
        MpPotion_Small  = 4301,
        MpPotion_Medium = 4401,
        MpPotion_Large  = 4501,
        //MaterialItem 
        Fish1 = 5001,
        Rice1 = 5101,
        Wood1 = 5201,

        Skin = 5301,


    }    

    public enum eItemSlotType
    {
        None = -1,

        InventoryItem,
        EquipItem,
        QuestItem,
        DropItem,
        UpgradeItem,
        GemItem,
    }
    
    public enum eItemType
    {
        None = -1,

        Weapon = 1,
        Shield,

        Helmet = 10,
        Top,
        HelmetDeco,
        Bottom,
        Elbow,
        Shoulder,
        Cape,
        Arm,
        Leg,
        Knee,

        Life = 20,

        Gem = 30,

        Potion = 40,

        Material = 50,
    }

    public enum eArmorType
    {
        None                = -1,

        Helmet = 0,    
        Top,                        
        HelmetDeco,                 
        Bottom,                     
        ElbowLeft,                  
        ElbowRight,

        ShoulderLeft,               
        ShoulderRight,
        Cape,                       
        ArmUpperLeft,               
        ArmUpperRight,
        ArmLowerLeft,
        ArmLowerRight,
        LegLeft,                    
        LegRight,
        KneeLeft,
        KneeRight,
    }
    
    public enum eItemStatName
    {
        None = -1,

        [Description("생명력")]
        Hp,
        [Description("마력")]
        Mp,
        [Description("공격력")]
        Pow,
        [Description("방어력")]
        Def,
        [Description("공격속도")]
        AtkSpeed,
        [Description("치명타율")]
        CriticalRate,
        [Description("회피율")]
        AvoidRate,
    }


   
    public enum eWeapon
    {
        None            = -1,

        ___SameWeaponCategory___ = 10,
        ___WeaponCategory___ = 100,

        OneHand_Mace = 1,
        OneHand_Sworld,
        TwoHand_Axe,
        TwoHand_Joust,
        TwoHand_Staff,
        TwoHand_Sworld,
        SubHand_Shield,

        OneHand_Mace1 = 101,       
        OneHand_Mace2 = 111,
        OneHand_Mace3 = 121,
        OneHand_Mace4 = 131,
        OneHand_Mace5 = 141,

        OneHand_Sworld1 = 201,
        OneHand_Sworld2 = 211,
        OneHand_Sworld3 = 221,
        OneHand_Sworld4 = 231,
        OneHand_Sworld5 = 241,

        TwoHand_Axe1 = 301,
        TwoHand_Axe2 = 311,
        TwoHand_Axe3 = 321,
        TwoHand_Axe4 = 331,
        TwoHand_Axe5 = 341,

        TwoHand_Joust1 = 401,
        TwoHand_Joust2 = 411,
        TwoHand_Joust3 = 421,
        TwoHand_Joust4 = 431,
        TwoHand_Joust5 = 441,

        TwoHand_Staff1 = 501,
        TwoHand_Staff2 = 511,
        TwoHand_Staff3 = 521,
        TwoHand_Staff4 = 531,
        TwoHand_Staff5 = 541,

        TwoHand_Sworld1 = 601,
        TwoHand_Sworld2 = 601,
        TwoHand_Sworld3 = 601,
        TwoHand_Sworld4 = 601,
        TwoHand_Sworld5 = 601,

        SubHand_Shield1 = 701,
        SubHand_Shield2 = 711,
        SubHand_Shield3 = 721,
        SubHand_Shield4 = 731,
        SubHand_Shield5 = 741,

    }


    public enum eWeaponType
    {
        None        = -1,

        One_Handed  = 0,
        Two_Handed,
        Shield,
    }

    public enum eGemItem
    {
        None = -1,
    }

    public enum eItemCombination
    {
        None = -1,
    }

    public enum eNPCID
    {
        None = -1,
        
        NPC101 = 101,
        NPC102,
        NPC103,
        NPC104,
        NPC105,
        NPC106,
        NPC107,
        NPC108,
        NPC109,
        NPC110,
    }

    public enum eNPCType
    {
        None = -1,

        QuestNPC,
        WeaponMerchantNPC,
        ArmorMerchantNPC,
        PotionMerchantNPC,
    }

    public enum eQuest
    {
        None = -1,

        ___QuestCategory___ = 100,

        Quest1_1 = 1 * ___QuestCategory___,
        Quest1_2,

        Quest2_1 = 2 * ___QuestCategory___,
        Quest2_2,

        Quest3_1 = 3 * ___QuestCategory___,
        Quest3_2,

        Quest4_1 = 4 * ___QuestCategory___,
        Quest4_2,

        Quest5_1 = 5 * ___QuestCategory___,
        Quest5_2,

        Quest6_1 = 6 * ___QuestCategory___,
        Quest6_2,

        Quest7_1 = 7 * ___QuestCategory___,
        Quest7_2,
    }
    
    public enum eQuestState
    {
        None = -1,

        QuestReceive = 1,
        QuestProceeding,
        QuestComplete,
    }

    public enum eQuestPurPoseType
    {
        None = -1,

        Talk = 1,
        Gain,
        Kill,
    }

    public enum eDropItemType
    {
        None = -1,

        Armor,
        Weapon,
        GemItem,
        EtcItem,
    }

    public enum eScene
    {
        None = -1,

        StartScene = 0,
        CharacterSelectionScene,
        CreateCharacterScene,
        [Description("마리아노플")]
        Local_MarianopleScene,
        [Description("하얀 숲")]
        Local_WhiteForestScene,
        [Description("파괴의 요람")]
        Dungeon_KroloalCradleScene,
        TableLoadingScene,
        SceneLoadingScene,

        Max,
    }

    public enum InteractionType
    {
        None = -1,

        [Description("줍기")]
        MonsterDrop,
        [Description("대화하기")]
        NPC,
        [Description("던전 입장")]
        Entry_Dungeon,
        LifeContents,

        Max,
    }

    public enum eLifeAction
    {
        None,
        [Description("벌목")]
        Felling,
        [Description("낚시")]
        Fishing,
        [Description("수확")]
        Harvesting,
        [Description("수리")]
        Repairing,
        [Description("톱질")]
        Sawing,
        [Description("줍기")]
        PickUp,
        Max,
    }

    public enum eGrid
    {
        None = -1,
        Grid0_0,
        Grid0_1,
        Grid0_2,
        Grid0_3,
        Grid0_4,
        Grid0_5,

        Grid1_0,
        Grid1_1,
        Grid1_2,
        Grid1_3,
        Grid1_4,
        Grid1_5,

        Grid2_0,
        Grid2_1,
        Grid2_2,
        Grid2_3,
        Grid2_4,
        Grid2_5,

        Grid3_0,
        Grid3_1,
        Grid3_2,
        Grid3_3,
        Grid3_4,
        Grid3_5,

        Grid4_0,
        Grid4_1,
        Grid4_2,
        Grid4_3,
        Grid4_4,
        Grid4_5,

        Grid5_0,
        Grid5_1,
        Grid5_2,
        Grid5_3,
        Grid5_4,
        Grid5_5,
        Max,
    }


    //Interface
    public interface ICopy<T>
    {
        void Copy(T data);
    }

    public interface IRangedAtkBullet
    {
        IEnumerator FireBullet();
        IEnumerator BulletExplosion(GameObject a_bullet);
    }

    public interface IInteraction
    {
        InteractionType m_eIteractionType { get; } 
        bool m_bEnable { get; set; }
        string m_sInteractionName { get; set; }
        string m_sInterPlayerAnim { get; }
        void ShowInter();
        void ActionKey();
        void EndInter();
        void NonShowInter();
    }
    
    //로딩 경로
    public static class Path
    {
        public const string PREFAB_PATH = "Prefab/";
        public const string PREFAB_PATH_ADD = "Prefab/{0}";

        public const string CUSTOMIZING_CHARACTER_PATH = "Character/Customizing{0}{1}";
        public const string CUSTOMIZING_ONEPARTS_PATH = "Mesh/Character/CustomizingParts/{0}/{1}/{2}{3}";
        public const string CUSTOMIZING_MULTIPARTS_PATH = "Mesh/Character/CustomizingParts/{0}/{1}/{2}{3}{4}";

        public const string ONEPARTS_ARMOR_PATH = "Mesh/Character/InGameParts/{0}/{1}";
        public const string MULTIPARTS_ARMOR_PATH = "Mesh/Character/InGameParts/{0}/{1}{2}{3}";

        public const string HANDEFFECT_PATH = "Effect/HandEffect/{0}_Hand";
        public const string SKILLEFFECT_PATH = "Effect/SkillEffect/{0}_Skill";
        public const string RANGEDMONSTERATK_PATH = "Effect/MonsterEffect/{0}";
        public const string BOSSEFFECT_PATH = "Effect/BossEffect/{0}";
        public const string GEMEFFECT_PATH = "Effect/GemEffect/{0}";

        public const string ITEMUI_PATH = "UI/ITEM/{0}";
        public const string DROPITEM_PATH = "UI/DropItem";
        public const string QUEST_PATH = "UI/Quest";
        public const string DAMAGETEXT_PATH = "UI/DamageText";
        public const string MONSTERHPBAR_PATH = "UI/MonsterHPBar";

        public const string WEAPON_PATH = "Weapon/{0}";
        public const string PREFAB_GRID_PATH = "Grid/{0}";
    }

    //유틸함수 및 객체, 변수
    public static class Define
    {
        public static Color activationBtnColor              = new Color(1f, 1f, 1f);
        public static Color deactivationBtnColor            = new Color(0.4f, 0.4f, 0.4f);
        public static Color monsterDamageTextColorInit      = new Color(1, 1, 0, 1);
        public static Color heroDamageTextColorInit         = new Color(1, 0, 0, 1);
        public static Color monsterDamageTextCriticalColor  = new Color(1, 150f / 255f, 0, 1);

        public static Vector3 addPosForHeroBody             = new Vector3(0, 1.2f, 0);

        public static float fieldOfViewOrigin = 60f;
        public static float fieldOfViewAction = 31f;
        public static float fCONVERSATION_POS = 500f;
        public static float fBulletMaintainMaxTime = 3f;

        public const int nRANDOM_POOL = 1000;
        public const int nPOOLSIZE = 10;
        public const int nMIDIUMPOOLSIZE = 4;
        public const int nSMALLPOOLSIZE = 2;
        public const int nMELLEMONSTER_KIND_OF_ATTACK = 5;
        public const int nRANGEDMONSTER_KIND_OF_ATTACK= 5;
        public const float fREGENERATE_LIMITTIME = 1.5f;

        public static bool GetBoolChage(bool a_bState)
        {
            return a_bState == true ? false : true;
        }
        
        public static bool IsInRange(Vector3 a_vcPos1, Vector3 a_vcPos2, float a_fRange)
        {
            float fX = a_vcPos1.x - a_vcPos2.x;
            float fY = a_vcPos1.y - a_vcPos2.y;
            float fZ = a_vcPos1.z - a_vcPos2.z;

            return ((a_fRange * a_fRange) > (fZ * fZ + fX * fX + fZ * fZ));
        }

        public static float IsRange(Vector3 a_vcPos1, Vector3 a_vcPos2)
        {
            float fX = a_vcPos1.x - a_vcPos2.x;
            float fY = a_vcPos1.y - a_vcPos2.y;
            float fZ = a_vcPos1.z - a_vcPos2.z;

            return (fZ * fZ + fX * fX + fZ * fZ);
        }


        public static void ChangeButtonColor(Button a_activationBtn, Button a_deactivationBtn)
        {
            a_activationBtn.image.color     = activationBtnColor;
            a_deactivationBtn.image.color   = deactivationBtnColor;
        }

        public static Sprite Texture2DToSprite(Texture2D texture)
        {
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        }
    }

    public static class Rand // 만분율 0~9999까지 저장
    {
        private static int m_nIndex = 0;
        private static int[] m_nArr = new int[Define.nRANDOM_POOL];

        static Rand()
        {
            for (int i = 0; i < Define.nRANDOM_POOL; ++i)
            {
                m_nArr[i] = UnityEngine.Random.Range(0, 10000);
            }
        }

        private static int nIndex
        {
            get
            {
                int nTemp = m_nIndex++;

                if (m_nIndex >= Define.nRANDOM_POOL) { m_nIndex = 0; }

                return nTemp;
            }
        }

        public static int Random() { return m_nArr[nIndex]; }
        public static float Percent() { return m_nArr[nIndex] * 0.0001f; }
        public static bool Percent(int a_nPercent) { return m_nArr[nIndex] <= (a_nPercent * 100); }
      
    }

    public static class YieldReturnCaching
    {
        static Dictionary<uint, WaitForSeconds> m_dicMapTime = new Dictionary<uint, WaitForSeconds>();

        public static WaitForSeconds WaitForSeconds(float a_fSeconds)
        {
            if (a_fSeconds <= 0.0f) { Debug.LogError("arg error"); return null; }

            uint nVal = 1;

            if (a_fSeconds >= 0.001f)
            {
                nVal = (uint)(a_fSeconds * 1000);
            }

            return WaitForMilliSeconds(nVal);
        }

        public static WaitForSeconds WaitForMilliSeconds(uint a_nMilliSecond)
        {
            if (m_dicMapTime.TryGetValue(a_nMilliSecond, out WaitForSeconds instance) == false)
            {
                instance = new WaitForSeconds(a_nMilliSecond / (float)1000);
                m_dicMapTime.Add(a_nMilliSecond, instance);
            }

            return instance;
        }
    }
   

    //구조체 클래스
    public class HeroStat : ICopy<HeroStat>
    {
        public eHero        m_eHero;
        public eHeroType    m_eHeroType;
        public int          m_nLevel;
        public float        m_fExp;
        public int          m_nHp;
        public int          m_nMp;
        public int          m_nPow;
        public int          m_nDef;
        public float        m_fAtkSpeed;
        public float        m_fMoveSpeed;
        public float        m_fJumpSpeed;
        public float        m_fGravity;
        public float        m_fCriticalRate;
        public float        m_fAvoidRate;

        public HeroStat() { }

        public HeroStat(eHero a_eHero, eHeroType a_eHeroType, int a_nLevel, float a_fExp, int a_nHp, int a_nMp, int a_nPow, int a_nDef, 
            float a_fAtkSpeed, float a_fMoveSpeed, float a_sJumpSpeed ,float a_fGravity, float a_fCriticalRate, float a_fAvoidRate)
        {
            m_eHero             = a_eHero;
            m_eHeroType         = a_eHeroType;
            m_nLevel            = a_nLevel;
            m_fExp              = a_fExp;
            m_nHp               = a_nHp;
            m_nMp               = a_nMp;
            m_nPow              = a_nPow;
            m_nDef              = a_nDef;
            m_fAtkSpeed         = a_fAtkSpeed;
            m_fMoveSpeed        = a_fMoveSpeed;
            m_fJumpSpeed        = a_sJumpSpeed;
            m_fGravity          = a_fGravity;
            m_fCriticalRate     = a_fCriticalRate;
            m_fAvoidRate        = a_fAvoidRate;
        }

        public void Copy(HeroStat stat)
        {
            m_eHero             = stat.m_eHero;
            m_eHeroType         = stat.m_eHeroType;
            m_nLevel            = stat.m_nLevel;
            m_fExp              = stat.m_fExp;
            m_nHp               = stat.m_nHp;
            m_nMp               = stat.m_nMp;
            m_nPow              = stat.m_nPow;
            m_nDef              = stat.m_nDef;
            m_fAtkSpeed         = stat.m_fAtkSpeed;
            m_fMoveSpeed        = stat.m_fMoveSpeed;
            m_fJumpSpeed        = stat.m_fJumpSpeed;
            m_fGravity          = stat.m_fGravity;
            m_fCriticalRate     = stat.m_fCriticalRate;
            m_fAvoidRate        = stat.m_fAvoidRate;

        }
    }

    [System.Serializable]
    public class SelectedCharacterData : ICopy<SelectedCharacterData>
    {
        public string           m_sUserName;
        public eHero            m_eHero;
        public eHeroType        m_eHeroType;
        public eGender          m_eGender;
        public eCamp            m_eCamp;
        public eQuest           m_eQuest;
        public Inventory        inventory;
        public Dictionary<eItemType, eItemID> m_dicEquipItems;
        public Dictionary<eCustomizing, int> m_dicCustomizingData = new Dictionary<eCustomizing, int>();
        public List<eQuest> m_liQuestList;
        public HeroStat originStat;
        public HeroStat nowStat;

        public SelectedCharacterData() { }

        public SelectedCharacterData(string a_sUserName, eHero a_eHero, eHeroType a_eHeroType, eGender a_eGender, eCamp a_eCamp, eQuest a_eQuest,Dictionary<eCustomizing, int> a_dicCustomizingData)
        {
            m_sUserName             = a_sUserName;
            m_eHero                 = a_eHero;
            m_eHeroType             = a_eHeroType;
            m_eGender               = a_eGender;
            m_eCamp                 = a_eCamp;
            m_eQuest                = a_eQuest;
            m_dicCustomizingData    = a_dicCustomizingData;
            originStat              = new HeroStat();
            nowStat                 = new HeroStat();
            inventory               = new Inventory();
            m_liQuestList           = new List<eQuest>();
            m_dicEquipItems         = new Dictionary<eItemType, eItemID>();
        }

        public void Copy(SelectedCharacterData data)
        {
            m_sUserName             = data.m_sUserName;
            m_eHero                 = data.m_eHero;
            m_eHeroType             = data.m_eHeroType;
            m_eGender               = data.m_eGender;
            m_eCamp                 = data.m_eCamp;
            m_eQuest                = data.m_eQuest;
            m_dicCustomizingData    = data.m_dicCustomizingData;
            originStat              = data.originStat;
            nowStat                 = data.nowStat;
            inventory               = data.inventory;
            m_liQuestList           = data.m_liQuestList;
            m_dicEquipItems         = data.m_dicEquipItems;
        }
 
    }

    public class MonsterInfo 
    {
        public eMonster                 m_eMonster;
        public eMontserType             m_eMonsterType;
        public eMonsterAtkType          m_eMonsterAtkType;
        public int                      m_nHp;
        public int                      m_nPow;
        public int                      m_nDef;
        public float                    m_fMoveSpeed;
        public float                    m_fAtkSpeed;
        public float                    m_fChaseRange;
        public float                    m_fAtkRange;
        public float                    m_fEvasionRange;
        public float                    m_fMaximumMoveRange;
        public float                    m_fPatrolRandomXPos;
        public float                    m_fPatrolRandomZPos;
        public float                    m_fGiveExp;
        public eItemID                  m_eDropMaterialItem;
        public string                   m_sRandomDropItems;  
        public int                      m_nDropItemCount;
        public float                    m_fPossiblePatrolTime;
        public float                    m_fEvasionTime;
        public float                    m_fBulletSpeed;
        public eRangedMonsterBullet     m_eRangedMonsterBullet;
        public eRangedMonsterExplosion  m_eRangedMonsterExlposion;
        public string                   m_sMonsterName;
        public float                    m_fMeleeAtkRange;
        public float                    m_fRangedAtkRange;

        public MonsterInfo() { }


        public void SetMonsterInfo(eMonster a_eMonster, eMontserType a_eMonsterType, eMonsterAtkType a_eMonsterAtkType, int a_nHp, int a_nPow, int a_nDef, float a_fMoveSpeed, float a_fAtkSpeed,
            float a_fChaseRange, float a_fAtkRange, float a_fEvasionRange, float a_fMaximumMoveRange, float a_fPatrolRandomXPos, float a_fPatrolRandomZPos, float a_fGiveExp, eItemID a_eDropMateriallItem, 
            string a_sRandomDrpItems, int a_nDropItemCount, float a_fPossiblePatrolTime, float a_fEvasionTime, float a_fBulletSpeed, eRangedMonsterBullet a_eRangedMonsterBullet,
            eRangedMonsterExplosion a_eRangedMonsterExplosion, string a_sMonsterName, float a_fMeleeAtkRange, float a_fRangedAtkRange)
        {
            m_eMonster                  = a_eMonster;
            m_eMonsterType              = a_eMonsterType;
            m_eMonsterAtkType           = a_eMonsterAtkType;
            m_nHp                       = a_nHp;
            m_nPow                      = a_nPow;
            m_nDef                      = a_nDef;
            m_fMoveSpeed                = a_fMoveSpeed;
            m_fAtkSpeed                 = a_fAtkSpeed;
            m_fChaseRange               = a_fChaseRange;
            m_fAtkRange                 = a_fAtkRange;
            m_fEvasionRange             = a_fEvasionRange;
            m_fMaximumMoveRange         = a_fMaximumMoveRange;
            m_fPatrolRandomXPos         = a_fPatrolRandomXPos;
            m_fPatrolRandomZPos         = a_fPatrolRandomZPos;
            m_fGiveExp                  = a_fGiveExp;
            m_eDropMaterialItem         = a_eDropMateriallItem;
            m_sRandomDropItems          = a_sRandomDrpItems;
            m_nDropItemCount            = a_nDropItemCount;
            m_fPossiblePatrolTime       = a_fPossiblePatrolTime;
            m_fEvasionTime              = a_fEvasionTime;
            m_fBulletSpeed              = a_fBulletSpeed;
            m_eRangedMonsterBullet      = a_eRangedMonsterBullet;
            m_eRangedMonsterExlposion   = a_eRangedMonsterExplosion;
            m_sMonsterName              = a_sMonsterName;
            m_fMeleeAtkRange            = a_fMeleeAtkRange;
            m_fRangedAtkRange           = a_fRangedAtkRange;
        }
    }
}
