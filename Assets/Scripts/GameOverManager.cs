using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager INSTANCE;

    public SpriteRenderer gameOverSpriteRenderer;

    public AudioClip defeatFx;
    public AudioClip victoryFx;

    void Awake()
    {
        INSTANCE = this;
    }

    public void GameOver()
    {
        SoundManager.INSTANCE.PlayFx(defeatFx);
        gameOverSpriteRenderer.gameObject.active = true;
        Time.timeScale = 0;
    }

    public void Victory()
    {
        // TODO
        SoundManager.INSTANCE.PlayFx(victoryFx);
        Debug.Log("Victory");
    }
}