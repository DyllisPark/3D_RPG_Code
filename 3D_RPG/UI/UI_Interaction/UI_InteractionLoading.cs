using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Global_Define;
using System.Collections;

public class UI_InteractionLoading : MonoBehaviour//, IPopup
{
    #region INSPECTOR
    public Text interactionText;
    public Image animSlider;

    IInteraction m_IInteraction;
    eItemID m_eItemID;
    #endregion
    //public float mActionTime { private get; set; }


    public void SetInteractionLoadingUI(IInteraction a_IInteraction, string a_sInteractionName, eItemID a_eItemID, eLifeAction a_eLifeAction, float a_fTime)
    {
        m_IInteraction = a_IInteraction;
        m_eItemID = a_eItemID;
        if (m_IInteraction == null)
        {
            Debug.LogError("Interaction error");
            return;
        }

        interactionText.text = string.Format("{0}중", a_sInteractionName);
        StartCoroutine(ActionUI(a_fTime, a_eLifeAction.ToString()));
    }

    IEnumerator ActionUI(float a_fTime, string a_sInteractionName)
    {
        ItemMng.Ins.ChangeLifeTool(a_sInteractionName, true);

        float fCheckTime = 0;
        animSlider.fillAmount = 0;
        while (fCheckTime < a_fTime)
        {
            yield return null;

            fCheckTime += Time.deltaTime;
            animSlider.fillAmount = fCheckTime / a_fTime;
        }

        ItemMng.Ins.ChangeLifeTool(a_sInteractionName, false);
        UIMng.Ins.dropItemUI.ActivateDropItem(m_IInteraction);
        UIMng.Ins.dropItemUI.OneDropItemUIUpdate(m_eItemID);
        //EndAction();
    }
    void EndAction()
    {
        m_IInteraction.EndInter();
        UIMng.Ins.dropItemUI.gameObject.ActiveChange();
        UIMng.Ins.dropItemUI.OneDropItemUIUpdate(m_eItemID);
        //m_IInteraction.m_sInteractionName = "획득";
        interactionText.text = "";
        gameObject.SetActive(false);
        m_IInteraction = null;
        GameMng.Ins.hero.m_IInteraction = null;
    }

}
