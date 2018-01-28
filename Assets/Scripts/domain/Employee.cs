using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    public GameObject cursorPrefab;

    public float arrivalDelay;

    public float productivity;

    public GameObject roomActiveSprite;

    public int messageQuota;

    [HideInInspector] public float arrivalProgress;
    [HideInInspector] public float messageProgress;
    [HideInInspector] public float messageSendProgress;
    [HideInInspector] public Vector3Int pipeEndpoint;
    [HideInInspector] public Queue<int> waitingMessages = new Queue<int>();

    public void SetActive()
    {
        roomActiveSprite.SetActive(false);
    }
    
}