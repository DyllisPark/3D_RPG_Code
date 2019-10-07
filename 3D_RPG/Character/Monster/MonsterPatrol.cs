using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonoBehaviour
{
    #region INSPECTOR

    public float m_fPatrolXPos = 7f;
    public float m_fPatrolZPos = 7f;
    [HideInInspector]
    public Vector3 randomPatrolPos;
    Vector3 spawnPos;

    #endregion

    private void Start()
    {
        spawnPos = transform.position + new Vector3(0, 0.2f, 0);
        randomPatrolPos = new Vector3(m_fPatrolXPos, 0, m_fPatrolZPos);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 50f/255f);
        Gizmos.DrawCube(transform.position + new Vector3(0, 0.2f, 0), new Vector3(m_fPatrolXPos, 0, m_fPatrolZPos));
    }
}
