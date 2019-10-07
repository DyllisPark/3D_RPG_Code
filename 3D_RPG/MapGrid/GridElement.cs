using System.Collections.Generic;
using UnityEngine;
using Global_Define;
public class GridElement : MonoBehaviour
{

    #region INSPRECOTR

    public GridGizMos m_GridGizMos;
    public eGrid m_eGrid { get; private set; }
    public List<GridElement> list_NearGrid = new List<GridElement>(); //인접그리드

    public List<Collider> list_Collider;
    public List<GameObject> list_GameObject;
    public int m_nX { get; private set; }
    public int m_nZ { get; private set; }
    private bool m_bGridState = false;

    #endregion



    public void SetEnumGrid(eGrid a_eGrid)
    {
        m_eGrid = a_eGrid;
    }

    public void SetGridLocation(int ax, int az)
    {
        m_nX = ax;
        m_nZ = az;
    }

    //인접그리드 셋팅 (자신도 포함됨)
    public void SetNearGrid(int a_nNearGridSize)
    {
        var alist_Grid = GridMng.Ins.m_liGrid;
        int maxcount = alist_Grid.Count;

        for (int ax=-a_nNearGridSize; ax <= a_nNearGridSize; ax++)
        {
            for (int az = -a_nNearGridSize; az <= a_nNearGridSize; az++)
            {
                int aax = m_nX + ax;
                int aaz = m_nZ + az;
                if (aax >= 0  && aax < maxcount && aaz >= 0 && aaz < maxcount) 
                {
                    list_NearGrid.Add( alist_Grid[aax][aaz] );
                }
            }
        }
    }

    public void AllEnable()
    {
        foreach (var val in list_NearGrid)
        {
            if (val.m_bGridState == true) //이미 켜져있으면 다음인덱스로
            {
                continue;
            }
            val.ElementEnable();
        }
    }
    public void AllDisable()
    {
        foreach (var val in list_NearGrid)
        {
            if (val.m_bGridState == false) //이미 꺼져있으면 다음인덱스로
            {
                continue;
            }
            val.ElementDisable();
        }
    }

    public void ElementDisable()
    {
        m_bGridState = false;
        m_GridGizMos.m_bGridState = m_bGridState;

       foreach (var val in list_Collider)
       {
           val.enabled = m_bGridState;
       }
       foreach (var val in list_GameObject)
       {
           val.SetActive(m_bGridState);
       }
    }
    public void ElementEnable()
    {
        m_bGridState = true;
        m_GridGizMos.m_bGridState = m_bGridState;


        foreach (var val in list_Collider)
        {
            val.enabled = m_bGridState;
        }
        foreach (var val in list_GameObject)
        {
            val.SetActive(m_bGridState);
        }
    }

    public void Clear()
    {
        m_bGridState = false;
        m_eGrid = eGrid.None;
        list_NearGrid.Clear();
        list_Collider.Clear();
        list_GameObject.Clear();
    }
}
