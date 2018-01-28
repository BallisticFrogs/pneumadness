using System.Collections.Generic;
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
        UIManager.INSTANCE.updateQueue(QueueManager.INSTANCE.GetNextPipes());
    }

    // Update is called once per frame 
    void Update()
    {
        var cellCoordsForMouse = GetCellCoordsForMouse();
        bool validPos = IsValidBuildingCell(cellCoordsForMouse);
        UIManager.INSTANCE.ShowMousePositionVald(validPos);

        if (Input.GetMouseButtonUp(0))
        {
            if (!validPos)
            {
                SoundManager.INSTANCE.PlayFx(pipeError);
                return;
            }

            // build pipe
            PipeTile pipe = QueueManager.INSTANCE.DequeuePipe();
            UIManager.INSTANCE.updateQueue(QueueManager.INSTANCE.GetNextPipes());
            tilemapObj.SetTile(cellCoordsForMouse, pipe);
            tilemapObj.RefreshTile(cellCoordsForMouse);

            // play sound
            SoundManager.INSTANCE.PlayFx(pipeBuilt);

            // update flow maps
            CursorManager.INSTANCE.UpdateFlowMaps();
        }
    }

    private Vector3Int GetCellCoordsForMouse()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cellPosition = grid.WorldToCell(mousepos);
        return Vector3Int.CeilToInt(cellPosition);
    }

    private bool IsValidBuildingCell(Vector3Int cellCoords)
    {
        // check conflict
        var tile = tilemapObj.GetTile(cellCoords);
        if (tile != null)
        {
            return false;
        }

        // check connectivity
        int connections = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                var point = new Vector3Int(cellCoords.x + i, cellCoords.y + j, cellCoords.z);
                var adjacentTile = tilemapObj.GetTile<PipeTile>(point);
                if (adjacentTile != null)
                {
                    var connected = CursorManager.AreConnected(QueueManager.INSTANCE.GetNextPipe().Type, i, j,
                        adjacentTile.Type);
                    if (connected) connections++;
                }
            }
        }

        if (connections == 0) return false;

        return true;
    }
}