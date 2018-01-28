using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (gameOverSpriteRenderer.gameObject.active == true && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}    public void Victory()
    {
        // TODO
        SoundManager.INSTANCE.PlayFx(victoryFx);
        Debug.Log("Victory");
    }
}