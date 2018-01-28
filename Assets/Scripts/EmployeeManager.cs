using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager INSTANCE;

    public GameObject rootObj;

    public GameObject cursorsRootObj;

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
    }

    public void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            UpdateEmpoyee(i);
        }
    }

    public void UpdateEmpoyee(int index)
    {
        Employee employee = GetEmployee(index);

        // handle delayed arrivals
        if (employee.arrivalProgress < employee.arrivalDelay)
        {
            employee.arrivalProgress += Time.deltaTime;

            // TODO show warning to player an employee is going to arrive

            if (employee.arrivalProgress >= employee.arrivalDelay)
            {
                // TODO handle arrival
            }
        }

        if (employee.arrivalProgress < employee.arrivalDelay) return;

        employee.messageProgress += Time.deltaTime * employee.productivity;
        if (employee.waitingMessages.Count > 0) employee.messageSendProgress += Time.deltaTime * employee.productivity;
        if (employee.messageProgress > 1)
        {
            employee.messageProgress = 0;
            var messageTarget = ComputeRandomMessageTarget(index);
            employee.waitingMessages.Enqueue(messageTarget);
        }

        if (employee.messageSendProgress > 0.3f && employee.waitingMessages.Count > 0)
        {
            employee.messageSendProgress = 0;

            // check the message can reach its destination
            var targetIndex = employee.waitingMessages.Peek();
            var canBeSent = CursorManager.INSTANCE.CanReachDestination(employee.pipeEndpoint, targetIndex);
            if (canBeSent)
            {
                // remove from waiting queue
                employee.waitingMessages.Dequeue();

                // spawn a cursor
                var targetEmployee = GetEmployee(targetIndex);
                var cursorObj = Instantiate(targetEmployee.cursorPrefab, cursorsRootObj.transform);
                var grid = endpointsGridLayer.transform.parent.gameObject.GetComponent<Grid>();
                cursorObj.transform.position = grid.CellToWorld(employee.pipeEndpoint) + 0.5f * grid.cellSize;
            }
            else
            {
                // TODO show warning and/or sound
            }
        }

        if (employee.waitingMessages.Count > 5)
        {
            // TODO game over
        }
    }

    public int ComputeRandomMessageTarget(int senderIndex)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i != senderIndex) indexes.Add(i);
        }

        var randomIndex = Random.Range(0, indexes.Count - 1);
        return indexes[randomIndex];
    }

    private void InitializeEmployee(Grid grid, int childIndex)
    {
        var childTransform = transform.GetChild(childIndex);
        var employeeObj = childTransform.gameObject;
        var employee = employeeObj.GetComponent<Employee>();

        employee.cursorPrefab.GetComponent<Cursor>().targetIndex = childIndex;

        Vector3 employeeCellPos = grid.WorldToCell(childTransform.position);
        Vector3Int employeeCellCoords = Vector3Int.CeilToInt(employeeCellPos);
        //Debug.Log("Employee found at=" + employeeCellCoords);

        var tilemap = endpointsGridLayer.GetComponent<Tilemap>();
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