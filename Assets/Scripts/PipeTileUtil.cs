#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

//[CreateAssetMenu]
public class PipeTileUtil : MonoBehaviour
{
    [MenuItem("Assets/Create/PipeTile")]
    public static void create()
    {
        var pipeTile = ScriptableObject.CreateInstance<PipeTile>();
        AssetDatabase.CreateAsset(pipeTile, "Tiles/New Pipe Tile");
    }
}