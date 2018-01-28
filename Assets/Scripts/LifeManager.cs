using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager INSTANCE;

    public int initialLifeCount = 3;
    private int currentLifeCount;

    public Sprite lifeOK;
    public Sprite lifeKO;

    public SpriteRenderer life1;
    public SpriteRenderer life2;
    public SpriteRenderer life3;


    void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
        currentLifeCount = initialLifeCount;
        life1.sprite = lifeOK;
        life2.sprite = lifeOK;
        life3.sprite = lifeOK;
    }

    public void RemoveLife()
    {
        currentLifeCount--;
        if (currentLifeCount == 3)
        {
            life1.sprite = lifeOK;
            life2.sprite = lifeOK;
            life3.sprite = lifeOK;
        }

        if (currentLifeCount == 2)
        {
            life1.sprite = lifeOK;
            life2.sprite = lifeOK;
            life3.sprite = lifeKO;
        }

        if (currentLifeCount == 1)
        {
            life1.sprite = lifeOK;
            life2.sprite = lifeKO;
            life3.sprite = lifeKO;
        }

        if (currentLifeCount == 0)
        {
            life1.sprite = lifeKO;
            life2.sprite = lifeKO;
            life3.sprite = lifeKO;
            GameOverManager.INSTANCE.GameOver();
        }
    }

    public int GetCurrentLifeCount()
    {
        return currentLifeCount;
    }

    public void RestoreLifeCount()
    {
        currentLifeCount = initialLifeCount;
        life1.sprite = lifeOK;
        life2.sprite = lifeOK;
        life3.sprite = lifeOK;
    }
}