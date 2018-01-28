﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UserActionManager : MonoBehaviour
{
    public static UserActionManager INSTANCE;

    public Grid grid;

    public Tilemap tilemapObj;

    public AudioClip pipeError;
    public List<AudioClip> pipeBuilt;

    void Awake()
    {
        INSTANCE = this;
    }

    // Use this for initialization
    void Start()
    {
        UIManager.INSTANCE.updateQueue(QueueManager.INSTANCE.GetNextPipe());
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cellPosition = grid.WorldToCell(mousepos);
            Vector3Int cellPositionInt = Vector3Int.CeilToInt(cellPosition);

            // TODO check conflict
            // TODO check connectivity

            // build pipe
            PipeTile pipe = QueueManager.INSTANCE.DequeuePipe();
            UIManager.INSTANCE.updateQueue(QueueManager.INSTANCE.GetNextPipe());
            tilemapObj.SetTile(cellPositionInt, pipe);
            tilemapObj.RefreshTile(cellPositionInt);

            // play sound
            SoundManager.INSTANCE.PlayFx(pipeBuilt);

            // update flow maps
            CursorManager.INSTANCE.UpdateFlowMaps();
        }
    }
}