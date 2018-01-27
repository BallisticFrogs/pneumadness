using System.Collections.Generic;
using domain;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager INSTANCE;

    public GameObject rootObj;

    private Dictionary<int, Employee> employeesById = new Dictionary<int, Employee>();

    void Awake()
    {
        INSTANCE = this;
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