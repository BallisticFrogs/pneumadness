using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public static GameOverManager INSTANCE;

    public Sprite gameOverSprite;
    public SpriteRenderer gameOverSpriteRenderer;


    void Awake()
    {
        INSTANCE = this;
    }

    public void GameOver()
    {
        gameOverSpriteRenderer.sprite = gameOverSprite;
        Time.timeScale = 0;
    }

}
