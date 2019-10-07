using UnityEngine;
using Global_Define;

public class LocalSceneBase : MonoBehaviour
{

    #region INSPECTOR

    public FollowCameraAxis cameraAxis;

    protected SelectedCharacterData characterData;
    protected GameObject m_objHero;

    #endregion

    protected void Awake()
    {
        characterData = DataMng.Ins.nowSelectedCharacterData;
    }

    protected void SceneInit()
    {
        QuestMng.Ins.QuestListInit();
        QuestMng.Ins.QuestNPCActivate();

        StartCoroutine(QuestMng.Ins.QuestMarkRoatate());
        UIMng.Ins.inventoryUI.InventoryUIInit(GameMng.Ins.hero.characterData.inventory);
        UIMng.Ins.characterStateUI.CharacterStateInit();
        GameMng.Ins.Init();
        DamageMng.Ins.DamagePoolInit();
        DamageMng.Ins.MonsterHpBarPoolInit();
    }

    void Update()
    {
        QuestMng.Ins.miniMapQuestMark.transform.localRotation = Quaternion.Euler(270, cameraAxis.transform.localEulerAngles.y + 90f, 90f);
    }
}
