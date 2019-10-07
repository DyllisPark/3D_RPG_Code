using UnityEngine;
using Global_Define;
using DG.Tweening;

public class GemNPC : NPC
{
    public override void ActionKey()
    {
        InteractionActionKey();
        UIMng.Ins.equipGemUI.EquipGemUIOnOff();
    }

    protected override void OnTriggerEnter(Collider a_collider)
    {
        if (a_collider.CompareTag("Player"))
        {
            ActivateInteraction();
        }
    }
}
