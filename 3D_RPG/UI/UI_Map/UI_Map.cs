using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global_Define;

//중간 사이즈 미니맵 UI
public class UI_Map : MonoBehaviour
{
    #region INSPECTOR

    public LocalMapInfo[] m_arrLocalmapInfo;
    public Image nowMapMarkImg;

    Vector3 markPos = new Vector3(0, 15, 0);
    #endregion

    private void OnEnable()
    {
        for(int i = 0; i < m_arrLocalmapInfo.Length; ++i)
        {
            if(m_arrLocalmapInfo[i].m_eScene == SceneMng.Ins.m_eNowScene)
            {
                nowMapMarkImg.gameObject.transform.SetParent(m_arrLocalmapInfo[i].gameObject.transform);
                nowMapMarkImg.gameObject.transform.localPosition = markPos;
                StartCoroutine(MovingMark());
                break;
            }
        }

    }

    public void OnClickMoveLocalMap(LocalMapInfo a_localMap)
    {
        ItemMng.Ins.SaveItemInfo();
        SceneMng.Ins.SaveSceneToMoveInfo(a_localMap.m_eScene);
    }

    IEnumerator MovingMark()
    {
        float fTime = 0;
        float fYPos = nowMapMarkImg.transform.localPosition.y;
        while (true)
        {
            fTime += 0.5f;
            nowMapMarkImg.transform.localPosition = new Vector3(nowMapMarkImg.transform.localPosition.x, fYPos + Mathf.PingPong(fTime, 10f), nowMapMarkImg.transform.localPosition.z);
            yield return null;
        }
    }

}
