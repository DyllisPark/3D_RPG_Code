using System.Collections.Generic;
using UnityEngine;
using Global_Define;

//드랍되는 아이템 표기 UI
public class UI_DropItem : MonoBehaviour
{
    #region INSPECTOR
    
    public GameObject           dropItemRoot;
    public List<DropItemInfo>   m_liDropItems = new List<DropItemInfo>();

    Queue<eItemID>  m_qDropItems = new Queue<eItemID>();
    const int       nDROPITEM_MAXCOUNT = 3;

    #endregion

    private void OnEnable()
    {
        m_liDropItems.Clear();
    }

    public void ActivateDropItem(IInteraction a_IInteraction)
    {
        a_IInteraction.EndInter();
        gameObject.ActiveChange();
        UIMng.Ins.Interactiontxt.text = "";
        UIMng.Ins.interactionLoadingUI.gameObject.SetActive(false);
    }

    public void MultiDropItemUIUpdate(string a_sDropItems, int a_nDropItemCount)
    {
        if(m_liDropItems.Count < a_nDropItemCount)
        {
            while(m_liDropItems.Count < a_nDropItemCount)
            {
                GameObject dropItem = dropItemRoot.Instantiate_asChild(Path.DROPITEM_PATH);
                var dropItemInfo = dropItem.GetComponent<DropItemInfo>();
                m_liDropItems.Add(dropItemInfo);
            }
        }
        else
        {
            for(int i = a_nDropItemCount; i < m_liDropItems.Count; ++i)
            {
                m_liDropItems[i].Clear();
            }
        }

        a_sDropItems.GetRewardItem(m_qDropItems);
        for(int i = 0; i < a_nDropItemCount; ++i)
        {
            m_liDropItems[i].SetDropItem(m_qDropItems.Dequeue());    
        }
    }

    public void OneDropItemUIUpdate(eItemID a_eItemID)
    {
        if (m_liDropItems.Count == 0)
        {
            GameObject dropItem = dropItemRoot.Instantiate_asChild(Path.DROPITEM_PATH);
            var dropItemInfo = dropItem.GetComponent<DropItemInfo>();
            dropItemInfo.SetDropItem(a_eItemID);
            m_liDropItems.Add(dropItemInfo);
        }
        else
        {
            m_liDropItems[0].SetDropItem(a_eItemID);
            m_liDropItems[0].gameObject.SetActive(true);
        }
        for(int i = 1; i < m_liDropItems.Count; ++i)
        {
            m_liDropItems[i].gameObject.SetActive(false);
        }

    }

    public void OnClickDropItemReceiveBtn()
    {
        foreach (var item in m_liDropItems)
        {
            if(item.gameObject.activeSelf == true)
                UIMng.Ins.inventoryUI.InventoryUIAddItem(item.dropItemSlot.m_eItemID);
            item.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
        GameMng.Ins.hero.m_IInteraction = null;

        if(GameMng.Ins.hero.interactionMonster != null)
        {
            GameMng.Ins.hero.interactionMonster.gameObject.SetActive(false);
            GameMng.Ins.hero.interactionMonster.monsterHpBar.gameObject.SetActive(false);
            GameMng.Ins.hero.interactionMonster = null;

            if (GameMng.Ins.hero.interactionMonster == GameMng.Ins.hero.targetMonster)
                UIMng.Ins.targettedMonsterUI.gameObject.SetActive(false);
        }
    }
}
