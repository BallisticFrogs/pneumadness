using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager INSTANCE;

    [Range(0f, 1f)]
    public float volume = 1;

    private AudioSource fxSource;

    private void Awake()
    {
        INSTANCE = this;
    }

    // Use this for initialization
    void Start () {
        fxSource = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayFx(AudioClip clip)
    {
        fxSource.PlayOneShot(clip, volume);
    }
}
