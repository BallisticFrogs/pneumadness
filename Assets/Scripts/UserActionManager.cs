using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UserActionManager : MonoBehaviour {

    public static UserActionManager INSTANCE;

    private Tilemap tilemapObj;

    public Grid grid;

    void Awake()
    {
        INSTANCE = this;
        tilemapObj = grid.GetComponentInChildren<Tilemap>();
    }

    // Use this for initialization
    void Start() {
         
    }

    // Update is called once per frame 
    void Update() {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cellPosition = grid.WorldToCell(mousepos);
            Vector3Int cellPositionInt = Vector3Int.CeilToInt(cellPosition);
                
            PipeTile pipe = QueueManager.INSTANCE.DequeuePipe();

            if (tilemapObj.GetTile(cellPositionInt) == null)
            {
                tilemapObj.SetTile(cellPositionInt, pipe);
                tilemapObj.RefreshTile(cellPositionInt);
                //TODO play success tube placement
            }
            else
            {
                //TODO play error tube placement
            }

            
        }
  
    }
    
}
