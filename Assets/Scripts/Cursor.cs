using UnityEngine;

public class Cursor : MonoBehaviour
{
    public int targetIndex;

    public float speed = 1;

    private Vector3 waypoint1;
    private Vector3 waypoint2;
    private float progress;
    private bool done;

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
    }

    private void RecomputeWaypoints()
    {
        var grid = UserActionManager.INSTANCE.grid;
        var currentCell = grid.WorldToCell(transform.position);
        waypoint1 = grid.CellToWorld(currentCell) + 0.5f * grid.cellSize;
        Debug.Log("currentCell=" + currentCell);

        var nextCell = CursorManager.INSTANCE.FindNextCell(targetIndex, currentCell);
        if (nextCell.HasValue)
        {
            Debug.Log("nextCell=" + nextCell.Value);
            waypoint2 = grid.CellToWorld(nextCell.Value) + 0.5f * grid.cellSize;
        }
        else
        {
            waypoint2 = waypoint1;
        }


        if (nextCell == currentCell)
        {
            done = true;
        }
    }
}