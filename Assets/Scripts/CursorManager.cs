using System.Collections.Generic;
using domain;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorManager : MonoBehaviour
{
    public static CursorManager INSTANCE;

    public GameObject tilemapObj;

    private Tilemap tilemap;
    private Dictionary<int, FlowMap> flowmapsById = new Dictionary<int, FlowMap>();

    void Awake()
    {
        INSTANCE = this;
        tilemap = GetComponent<Tilemap>();
    }

    public void RegisterEndpoint(int index)
    {
        BoundsInt bounds = tilemap.cellBounds;
        FlowMap flowMap = new FlowMap(index, bounds.size.x, bounds.size.y);
        flowmapsById.Add(flowMap.employee, flowMap);
    }

    private void UpdateFlowMaps()
    {
        foreach (var index in flowmapsById.Keys)
        {
            var employee = EmployeeManager.INSTANCE.GetEmployee(index);
            var map = flowmapsById[index];
            UpdateFlowMap(map, employee.pipeEndpoint);
        }
    }

    private void UpdateFlowMap(FlowMap map, Vector3Int pipeEnd)
    {
        // TODO if pipe tiles are destroyed, we should reset the all grid but ignore for now

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        HashSet<Vector3Int> closedList = new HashSet<Vector3Int>();
        Queue<Vector3Int> openList = new Queue<Vector3Int>();
        Queue<Vector3Int> nextOpenList = new Queue<Vector3Int>();
        nextOpenList.Enqueue(pipeEnd);

        int score = 0;
        while (nextOpenList.Count > 0)
        {
            // prepare open list for processing
            Queue<Vector3Int> tmp = openList;
            openList = nextOpenList;
            nextOpenList = tmp;

            // process all tiles at a given distance from the pipe end
            while (openList.Count > 0)
            {
                var coords = openList.Dequeue();
                if (closedList.Contains(coords)) continue;

                // update flow map score
                map.grid[coords.x][coords.y] = score;

                // find all surrounding pipes
                // filter the connected ones
                // add them to next open list
                PipeTile pipeTile = GetPipeTile(allTiles, coords.x, coords.y);
                for (int i = -1; i < 1; i++)
                {
                    for (int j = -1; j < 1; j++)
                    {
                        PipeTile otherPipeTile = GetPipeTile(allTiles, coords.x + i, coords.y + j);
                        if (otherPipeTile != null)
                        {
                            var connected = AreConnected(pipeTile.Type, i, j, otherPipeTile.Type);
                            if (connected)
                            {
                                nextOpenList.Enqueue(new Vector3Int(coords.x + i, coords.y + j, pipeEnd.z));
                            }
                        }
                    }
                }

                closedList.Add(coords);
            }

            // increase score for all pipes at distance d+1
            score++;
        }
    }

    private PipeTile GetPipeTile(TileBase[] allTiles, int x, int y)
    {
        TileBase tile = allTiles[x + y * tilemap.cellBounds.size.x];
        if (tile != null && tile is PipeTile) return (PipeTile) tile;
        return null;
    }

    private bool AreConnected(TileType refType, int otherRelX, int otherRelY, TileType otherType)
    {
        if (otherRelX != 0 && otherRelY != 0) return false;

        if (otherRelX == -1)
        {
            return refType.IsConnectedTo(Dir.LEFT) && otherType.IsConnectedTo(Dir.RIGHT);
        }

        if (otherRelX == 1)
        {
            return refType.IsConnectedTo(Dir.RIGHT) && otherType.IsConnectedTo(Dir.LEFT);
        }

        if (otherRelY == -1)
        {
            return refType.IsConnectedTo(Dir.DOWN) && otherType.IsConnectedTo(Dir.UP);
        }

        if (otherRelY == 1)
        {
            return refType.IsConnectedTo(Dir.UP) && otherType.IsConnectedTo(Dir.DOWN);
        }

        return false;
    }
}