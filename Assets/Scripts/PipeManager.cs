using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PipeManager : MonoBehaviour
{
    public static PipeManager INSTANCE;

    private Tilemap tilemapObj;

    public PipeTile tubeTile;

    public Grid grid;

    void Awake()
    {
        INSTANCE = this;
        tilemapObj = grid.GetComponentInChildren<Tilemap>();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("mousepos = " + mousepos);
            Vector3 cellPosition = grid.WorldToCell(mousepos);
            Vector3Int cellPositionInt = Vector3Int.CeilToInt(cellPosition);
            Debug.Log("cellPosition = " + cellPositionInt); /*
            tilemapObj.SetColor(cellPositionInt, Color.green);
            tilemapObj.SetTile(cellPositionInt, tubeTile);
            tilemapObj.RefreshTile(cellPositionInt);*/
        }
    }
}