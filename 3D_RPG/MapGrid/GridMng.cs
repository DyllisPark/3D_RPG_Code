using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global_Define;


public class GridMng : MonoBehaviour
{
    #region SINGLETON
    static GridMng _instance = null;

    public static GridMng Ins
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GridMng)) as GridMng;
                if (_instance == null)
                {
                    _instance = new GameObject("GridMng", typeof(GridMng)).GetComponent<GridMng>();
                }
            }

            return _instance;
        }
    }

    #endregion


    #region INSPECTOR
    public List<GameObject> m_liAllGameObject; //맵안에 있는 모든 오브젝트
    public GameObject m_objGridRoot;
    public List<List<GridElement>> m_liGrid = new List<List<GridElement>>();

    const int nGRID_COUNT = 12;   // nGRID_COUNT * nGRID_COUNT
    const int nGRID_SCALE = 50; //그리드의 실제 가로세로 사이즈
    const int nNEAR_GRID_SIZE = 1; //인접그리드 개수(칸) (팔방)
    #endregion


    public void Init()
    {
        int nX = 0;
        int nZ = 0;
        for (int ax = 0; ax < nGRID_COUNT; ax++)
        {
            m_liGrid.Add(new List<GridElement>()); //x좌표
            nX = ax * nGRID_SCALE;
            for (int az = 0; az < nGRID_COUNT; az++)
            {
                nZ = az * nGRID_SCALE;
                Vector3 vpos = new Vector3(nX, 0, nZ);
                GridElement grid = SetGrid(ax,az, vpos);
                grid.SetGridLocation(ax, az);
                m_liGrid[ax].Add(grid);
            }
        }

        //모든 GridElement가 셋팅된 다음에 인접노드 셋팅
        SetNearGrid();        

        int nfor = 0;
        //모든 오브젝트 그리드좌표안에 해당하는 리스트에 추가
        foreach (var obj in m_liAllGameObject)
        {
            nfor++;
            Vector3 objpos = obj.transform.position;
       
            for (int ax = nNEAR_GRID_SIZE; ax < nGRID_COUNT; ax++)
            {
                for (int az = nNEAR_GRID_SIZE; az < nGRID_COUNT; az++)
                {
                    if (objpos.x <= ax* nGRID_SCALE
                        && objpos.z <= az* nGRID_SCALE
                        && objpos.x >= (ax- nNEAR_GRID_SIZE) * nGRID_SCALE
                        && objpos.z >= (az- nNEAR_GRID_SIZE) * nGRID_SCALE)
                    {
                        var lis = m_liGrid[ax][az];
                        var col = obj.GetComponent<Collider>();
                        if (col != null)
                        {
                            col.enabled = false;
                            obj.transform.SetParent(lis.gameObject.transform);
                            lis.list_Collider.Add(col);
                        }
                        else
                        {
                            lis.list_GameObject.Add(obj);
                        }
                    }
                }
            }
        }
    }

    private void SetNearGrid()
    {
        foreach (var grid in m_liGrid)
        {
            foreach (var element in grid)
            {
                element.SetNearGrid(nNEAR_GRID_SIZE);
            }
        }
    }
    //그리드 셋팅
    public GridElement SetGrid(int ax, int az, Vector3 a_GridPos)
    {
        string s = string.Format(Path.PREFAB_GRID_PATH, "Grid");
        var gridobj = m_objGridRoot.Instantiate_asChild(s);
        gridobj.name = string.Format("GRID_({0},{1})",ax,az);
        gridobj.transform.position = a_GridPos;

        GridElement val = gridobj.GetComponent<GridElement>();

        return val;
    }

    private GridElement PreGrid = null;
    private GridElement CurGrid = null;

    //처음으로 활성화될 그리드요소 체크
    public void SetFirstGridPos(Vector3 a_PlayerPos)
    {
        for(int ax=1; ax < nGRID_COUNT; ax++)
        {
            for(int az = 1; az < nGRID_COUNT; az++)
            {
                if (a_PlayerPos.x <= ax * nGRID_SCALE && a_PlayerPos.z <= az * nGRID_SCALE
                    && a_PlayerPos.x >= (ax - nNEAR_GRID_SIZE) * nGRID_SCALE
                    && a_PlayerPos.z >= (az - nNEAR_GRID_SIZE) * nGRID_SCALE)
                {
                    CurGrid = m_liGrid[ax][az];
                    CurGrid.AllEnable();
                }
            }
        }
    }
    //플레이어가 위치한 그리드체크 (인접한 그리드만 비교)
    public void CheckGrid(Vector3 a_PlayerPos) 
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        PreGrid = CurGrid;
        foreach (var val in CurGrid.list_NearGrid)
        {
            if (a_PlayerPos.x <= val.m_nX * nGRID_SCALE && a_PlayerPos.z <= val.m_nZ * nGRID_SCALE
                && a_PlayerPos.x >= (val.m_nX - nNEAR_GRID_SIZE) * nGRID_SCALE
                && a_PlayerPos.z >= (val.m_nZ - nNEAR_GRID_SIZE) * nGRID_SCALE)
            {
                if (CurGrid == val) //현재그리드가 변하지 않은 경우
                {
                    return;
                }
                CurGrid = val;
                
                foreach (var Pre in PreGrid.list_NearGrid)
                {
                    foreach (var Cur in CurGrid.list_NearGrid)
                    {
                        if (Cur.m_nX == Pre.m_nX && Cur.m_nZ == Pre.m_nZ) //중복된 그리드
                        {
                            Pre.ElementEnable();
                        }
                        else
                        {
                            Pre.ElementDisable();
                        }
                    }
                }
                CurGrid.AllEnable();
            }
        }

        sw.Stop();
    }


    public void Clear()
    {
        foreach(var grid in m_liGrid)
        {
            foreach (var element in grid)
            {
                element.Clear();
            }
        }
        m_liGrid.Clear();
    }
}
