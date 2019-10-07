using UnityEngine;

public class GridGizMos : MonoBehaviour
{

    public bool m_bGridState = false;

    private void OnDrawGizmos()
    {
        if (m_bGridState)
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawCube(transform.position, new Vector3(50, 1, 50));
    }

}
