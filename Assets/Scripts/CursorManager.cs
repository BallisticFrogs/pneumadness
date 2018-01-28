using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorManager : MonoBehaviour
{
    public static CursorManager INSTANCE;

    public GameObject tilemapPipesObj;
    public GameObject tilemapEndpointsObj;

    private Tilemap tilemapPipes;
    private Tilemap tilemapEndpoints;
    private Dictionary<int, FlowMap> flowmapsById = new Dictionary<int, FlowMap>();

    void Awake()
    {
        INSTANCE = this;
        tilemapPipes = tilemapPipesObj.GetComponent<Tilemap>();
        tilemapEndpoints = tilemapEndpointsObj.GetComponent<Tilemap>();
    }

    public void RegisterEndpoint(int index, Vector3Int endpoint)
    {
        FlowMap flowMap = new FlowMap(index, 50, 50);
        UpdateFlowMap(flowMap, endpoint);
        flowmapsById.Add(flowMap.employee, flowMap);
    }

    public Vector3Int? FindNextCell(int targetIndex, Vector3Int currentCell)
    {
        FlowMap map;
        flowmapsById.TryGetValue(targetIndex, out map);

        int currentScore = map.scoreFromGridCoords(currentCell);
        ;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                var point = new Vector3Int(currentCell.x + i, currentCell.y + j, currentCell.z);
                var cellScore = map.scoreFromGridCoords(point);
                if (cellScore > 0 && cellScore == currentScore - 1)
                {
                    return point;
                }
            }
        }

        return null;
    }

    public void UpdateFlowMaps()
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
        // TODO if pipe tiles are destroyed, we should reset the whole grid but ignore for now

        HashSet<Vector3Int> closedList = new HashSet<Vector3Int>();
        Queue<Vector3Int> openList = new Queue<Vector3Int>();
        Queue<Vector3Int> nextOpenList = new Queue<Vector3Int>();
        nextOpenList.Enqueue(pipeEnd);

        int score = 0;
        while (nextOpenList.Count > 0)
        {
            score++;

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
                map.grid[coords.x + map.grid.Length / 2][coords.y + map.grid[0].Length / 2] = score;
                // Debug.Log(coords + "=" + score);

                // find all surrounding pipes
                // filter the connected ones
                // add them to next open list
                PipeTile pipeTile = tilemapPipes.GetTile<PipeTile>(coords);
                if (pipeTile == null || pipeTile.Type == TileType.OFFICE)
                {
                    pipeTile = tilemapEndpoints.GetTile<PipeTile>(coords);
                }

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        var point = new Vector3Int(coords.x + i, coords.y + j, coords.z);

                        PipeTile otherPipeTile = tilemapPipes.GetTile<PipeTile>(point);
                        if (otherPipeTile == null || otherPipeTile.Type == TileType.OFFICE)
                        {
                            otherPipeTile = tilemapEndpoints.GetTile<PipeTile>(point);
                        }

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
        }
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