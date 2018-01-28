using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager INSTANCE;

    public SpriteRenderer gameOverSpriteRenderer;


    void Awake()
    {
        INSTANCE = this;
    }

    public void GameOver()
    {
        gameOverSpriteRenderer.gameObject.active = true;
        Time.timeScale = 0;
    }

    public void Victory()
    {
        // TODO
        Debug.Log("Victory");
    }
}