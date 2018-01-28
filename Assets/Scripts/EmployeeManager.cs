using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager INSTANCE;

    public GameObject rootObj;

    public GameObject endpointsGridLayer;

    private Dictionary<int, Employee> employeesById = new Dictionary<int, Employee>();

    void Awake()
    {
        INSTANCE = this;
    }

    public void Start()
    {
        var grid = endpointsGridLayer.transform.parent.gameObject.GetComponent<Grid>();
        for (int i = 0; i < transform.childCount; i++)
        {
            InitializeEmployee(grid, i);
        }

        // spawn a cursor for test 
        var employee1 = GetEmployee(0);
        var employee2 = GetEmployee(1);
        var cursorObj = Instantiate(employee2.cursorPrefab, rootObj.transform);
        cursorObj.transform.position = grid.CellToWorld(employee1.pipeEndpoint) + 0.5f * grid.cellSize;
//        var cursor = cursorObj.GetComponent<Cursor>();
//        cursor.targetIndex = 1;
    }

    private void InitializeEmployee(Grid grid, int childIndex)
    {
        var childTransform = transform.GetChild(childIndex);
        var employeeObj = childTransform.gameObject;
        var employee = employeeObj.GetComponent<Employee>();

        Vector3 employeeCellPos = grid.WorldToCell(childTransform.position);
        Vector3Int employeeCellCoords = Vector3Int.CeilToInt(employeeCellPos);
        //Debug.Log("Employee found at=" + employeeCellCoords);

        var tilemap = endpointsGridLayer.GetComponent<Tilemap>();
        TileBase[] allTiles = tilemap.GetTilesBlock(tilemap.cellBounds);

        for (int j = -1; j <= 1; j++)
        {
            for (int k = -1; k <= 1; k++)
            {
                if (k == 0 && j == 0) continue;
                var x = employeeCellCoords.x + j;
                var y = employeeCellCoords.y + k;
                var point = new Vector3Int(x, y, 0);

                var endpoint = tilemap.GetTile<PipeTile>(point);
                if (endpoint != null)
                {
                    employee.pipeEndpoint = point;
                    //Debug.Log("Employee endpoint found at=" + point);

                    CursorManager.INSTANCE.RegisterEndpoint(childIndex, point);

                    return;
                }
            }
        }
    }

    public Employee GetEmployee(int index)
    {
        var childTransform = rootObj.transform.GetChild(index);
        return childTransform.gameObject.GetComponent<Employee>();
    }

    public void ActivateEmployee(int index)
    {
        var childTransform = rootObj.transform.GetChild(index);
//        childTransform.gameObject.GetComponent<>();
    }
}