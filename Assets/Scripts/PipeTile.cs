using domain;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class PipeTile : Tile
{
    public TileType Type;

    [MenuItem("Assets/Create/PipeTile")]
    public static void create()
    {
        var pipeTile = CreateInstance<PipeTile>();
        AssetDatabase.CreateAsset(pipeTile, "Tiles/New Pipe Tile");
    }
}