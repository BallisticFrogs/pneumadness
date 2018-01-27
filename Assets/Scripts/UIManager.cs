using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static UIManager INSTANCE;

    private SpriteRenderer spriteRenderer0;
    private SpriteRenderer spriteRenderer1;
    private SpriteRenderer spriteRenderer2;

    // Use this for initialization
    void Awake ()
    {
        INSTANCE = this;
        spriteRenderer0 = transform.Find("nextPipe0").gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer1 = transform.Find("nextPipe1").gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer2 = transform.Find("nextPipe2").gameObject.GetComponent<SpriteRenderer>();
    }

    public void updateQueue(PipeTile[] tiles)
    {
        spriteRenderer0.sprite = tiles[0].sprite;
        spriteRenderer1.sprite = tiles[1].sprite;
        spriteRenderer2.sprite = tiles[2].sprite;
    }
}
