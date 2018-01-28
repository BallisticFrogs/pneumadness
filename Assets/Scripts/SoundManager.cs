using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager INSTANCE;

    [Range(0f, 1f)] public float volume = 1;

    public AudioSource bgmSource;
    public AudioSource fxSource;

    public AudioClip random_atmosphere_1;
    public AudioClip random_atmosphere_2;
    public AudioClip random_atmosphere_3;
    public AudioClip random_atmosphere_4;
    public AudioClip random_atmosphere_5;
    public AudioClip random_atmosphere_6;
    public AudioClip random_atmosphere_7;
    public AudioClip random_atmosphere_8;
    public AudioClip random_atmosphere_9;
    public AudioClip random_atmosphere_10;
    public AudioClip random_atmosphere_11;
    public AudioClip random_atmosphere_12;
    public AudioClip random_atmosphere_13;

    private int randomMusic  = 0;
    private float randomTime = 0f;
    private float timeCounter = 1f;
    private List<AudioClip> ambientClip;

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        ambientClip = new List<AudioClip>();
        ambientClip.Add(random_atmosphere_1);
        ambientClip.Add(random_atmosphere_2);
        ambientClip.Add(random_atmosphere_3);
        ambientClip.Add(random_atmosphere_4);
        ambientClip.Add(random_atmosphere_5);
        ambientClip.Add(random_atmosphere_6);
        ambientClip.Add(random_atmosphere_7);
        ambientClip.Add(random_atmosphere_8);
        ambientClip.Add(random_atmosphere_9);
        ambientClip.Add(random_atmosphere_10);
        ambientClip.Add(random_atmosphere_11);
        ambientClip.Add(random_atmosphere_12);
        ambientClip.Add(random_atmosphere_13);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter > randomTime && !fxSource.isPlaying)
        {
            fxSource.Stop();
            randomTime = Random.Range(2f, 15f);
            timeCounter = 0f;
            randomMusic = Random.Range(1, 13);
            fxSource.clip = ambientClip[randomMusic];
            fxSource.Play();
        }
        timeCounter += Time.deltaTime;
    }

    public void PlayFx(AudioClip clip)
    {
        fxSource.PlayOneShot(clip, volume);
    }

    public void PlayFx(List<AudioClip> clips)
    {
        var index = Random.Range(0, clips.Count - 1);
        PlayFx(clips[index]);
    }
}