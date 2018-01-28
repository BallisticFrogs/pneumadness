using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    public float speed = 1;

    [HideInInspector] public int targetIndex;

    private Vector3 waypoint1;
    private Vector3 waypoint2;
    private float progress;
    private bool canReachDestination;
    private Vector3Int? previousCell;

    public void Start()
    {
        progress = 0f;
        RecomputeWaypoints();
    }

    public void Update()
    {
        // update progress
        progress += Time.deltaTime * speed * 0.1f;
        // Debug.Log("progress=" + progress);

        // recompute waypoints if necessary
        if (progress >= 1)
        {
            progress -= 1;
            RecomputeWaypoints();
        }

        // move
        transform.position = waypoint1 + progress * (waypoint2 - waypoint1);

        // rotate
        if (waypoint1 != waypoint2)
        {
            var angle = Vector3.Angle(waypoint2 - waypoint1, Vector3.up);
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    private void RecomputeWaypoints()
    {
        var grid = UserActionManager.INSTANCE.grid;
        var currentCell = grid.WorldToCell(transform.position);

        // if the message is in empty space, it is lost
        // and player looses one life
        var pipeTile = CursorManager.INSTANCE.tilemapPipesObj.GetComponent<Tilemap>().GetTile<PipeTile>(currentCell);
        if (pipeTile == null || pipeTile.Type == TileType.OFFICE)
        {
            pipeTile = CursorManager.INSTANCE.tilemapEndpointsObj.GetComponent<Tilemap>()
                .GetTile<PipeTile>(currentCell);
        }

        if (pipeTile == null || pipeTile.Type == TileType.OFFICE)
        {
            // loose a life
            SoundManager.INSTANCE.PlayFx(SoundManager.INSTANCE.cursorLostFx);
            LifeManager.INSTANCE.RemoveLife();
            Destroy(gameObject);
            return;
        }

        if (!canReachDestination)
        {
            // re-check if we can reach the destination
            canReachDestination = CursorManager.INSTANCE.CanReachDestination(currentCell, targetIndex);
        }

        // compute point 1
        waypoint1 = grid.CellToWorld(currentCell) + 0.5f * grid.cellSize;
        // Debug.Log("currentCell=" + currentCell);

        // compute point 2
        Vector3Int? nextCell;
        if (canReachDestination)
        {
            // find next cell towards destination
            nextCell = CursorManager.INSTANCE.FindNextCell(targetIndex, currentCell);
            if (nextCell.HasValue)
            {
                // Debug.Log("nextCell=" + nextCell.Value);
                waypoint2 = grid.CellToWorld(nextCell.Value) + 0.5f * grid.cellSize;
            }
            else
            {
                waypoint2 = waypoint1;
            }
        }
        else
        {
            // choose random path with no backtracking
            var prevCell = previousCell.HasValue ? previousCell.Value : new Vector3Int(-100, -100, 0);
            nextCell = CursorManager.INSTANCE.FindNextCellRandom(currentCell, prevCell);
            waypoint2 = grid.CellToWorld(nextCell.Value) + 0.5f * grid.cellSize;
        }

        previousCell = currentCell;
        if (nextCell == currentCell)
        {
            SoundManager.INSTANCE.PlayFx(SoundManager.INSTANCE.cursorArrivedFx);
            Destroy(gameObject);
        }
    }
}