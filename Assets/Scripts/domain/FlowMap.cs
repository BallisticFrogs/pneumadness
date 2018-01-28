using UnityEngine;

public class FlowMap
{
    public readonly int employee;
    public readonly int[][] grid;

    public FlowMap(int employee, int width, int height)
    {
        this.employee = employee;
        grid = new int[width][];
        for (int i = 0; i < width; i++)
        {
            grid[i] = new int[height];
        }
    }

    public int scoreFromGridCoords(Vector3Int cellGridCoords)
    {
        int x = cellGridCoords.x + grid.Length / 2;
        int y = cellGridCoords.y + grid[0].Length / 2;
        if (x > grid.Length || y > grid[0].Length) return 0;
        return grid[x][y];
    }

    private Vector3Int gridToMap(FlowMap map, Vector3Int cellGridCoords)
    {
        return new Vector3Int(cellGridCoords.x + map.grid.Length / 2,
            cellGridCoords.y + map.grid[0].Length / 2,
            cellGridCoords.z);
    }
}