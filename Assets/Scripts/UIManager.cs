using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager INSTANCE;

    public SpriteRenderer spriteRenderer0;
    public SpriteRenderer spriteRenderer1;
    public SpriteRenderer spriteRenderer2;

    void Awake()
    {
        INSTANCE = this;
    }

    public void updateQueue(PipeTile[] tiles)
    {
        spriteRenderer0.sprite = tiles[0].sprite;
        spriteRenderer1.sprite = tiles[1].sprite;
        spriteRenderer2.sprite = tiles[2].sprite;
    }
}