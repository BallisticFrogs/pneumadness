using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager INSTANCE;

    public SpriteRenderer gameOverSpriteRenderer;

    public AudioClip defeatFx;
    public AudioClip victoryFx;
    public SpriteRenderer victorySpriteRenderer;

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
            if ((gameOverSpriteRenderer.gameObject.active == true || victorySpriteRenderer.gameObject.active == true) && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                int currentScene = SceneManager.GetActiveScene().buildIndex;
                if (gameOverSpriteRenderer.gameObject.active == true)
                {
                    SceneManager.LoadScene(currentScene);
                } else if (victorySpriteRenderer.gameObject.active == true)
                {
                    if (currentScene >= 2)
                    {
                        Application.Quit();
                    }
                    SceneManager.LoadScene(currentScene+1);
                }
            }
        }
    }

    public void Victory()
    {
        victorySpriteRenderer.gameObject.active = true;
        Time.timeScale = 0;
        // TODO
        SoundManager.INSTANCE.PlayFx(victoryFx);
        Debug.Log("Victory");
    }
}