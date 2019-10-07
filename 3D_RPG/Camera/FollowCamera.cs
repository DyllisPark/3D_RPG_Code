using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;

public class FollowCamera : MonoBehaviour
{
    #region INSPECTOR

    public float m_fMoveSpeed = 0.2f;

    Transform   targetTrans;
    Vector3     cameraPos;


    #endregion


    private void Start()
    {
        targetTrans = GameMng.Ins.hero.gameObject.transform;
    }

    private void Update()
    {
        cameraPos = transform.position;
        transform.position += (targetTrans.position - cameraPos) * m_fMoveSpeed;
    }
}
