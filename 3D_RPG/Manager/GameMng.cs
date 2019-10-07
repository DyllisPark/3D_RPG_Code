using UnityEngine;
using Global_Define;

public class GameMng : MonoBehaviour
{
    #region SINGLETON
    static GameMng _instance = null;

    public static GameMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameMng)) as GameMng;
                if (_instance == null)
                {
                    _instance = new GameObject("GameMng", typeof(GameMng)).GetComponent<GameMng>();
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion


    #region INSPECTOR

    public Hero hero;
    public HeroStatCalc heroStatCalc;
    public DamageCalc damageCalc;
    
    #endregion


    public void Init()
    {
        heroStatCalc = new HeroStatCalc();
        damageCalc = new DamageCalc();
    }

    public GameObject GetEquipWeapon()
    {
        return hero.weaponRoot.transform.GetChild(0).gameObject;
    }
}
