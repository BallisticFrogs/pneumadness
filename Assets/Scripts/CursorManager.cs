using System.Collections.Generic;
using domain;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorManager : MonoBehaviour
{
    public static CursorManager INSTANCE;

    public GameObject tilemapObj;

    private Dictionary<int, FlowMap> flowmapsById = new Dictionary<int, FlowMap>();
    private int gridWidth;
    private int gridheight;

    void Awake()
    {
        INSTANCE = this;
    }

//    void Start()
//    {
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//    }

    private void ComputeGridSize()
    {
        // TODO
    }

    public void AddEmployee(Employee employee)
    {
        FlowMap flowMap = new FlowMap(employee.index, gridWidth, gridheight);
        flowmapsById.Add(flowMap.employee, flowMap);
    }

    private void UpdateFlowMaps()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) continue;
            }
        }
    }

    private void UpdateFlowMap(FlowMap map)
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile == null) continue;
            }
        }
    }
}