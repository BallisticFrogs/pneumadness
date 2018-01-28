using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager INSTANCE;

    [Range(0f, 1f)] public float volume = 1;

    public AudioSource bgmSource;
    public AudioSource fxSource;

    private void Awake()
    {
        INSTANCE = this;
    }

    public void PlayFx(AudioClip clip)
    {
        fxSource.PlayOneShot(clip, volume);
    }

    public void PlayFx(List<AudioClip> clips)
    {
        var index = Random.Range(0, clips.Count);
        PlayFx(clips[index]);
    }
}