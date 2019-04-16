﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip[] footsteps;
    public AudioClip[] takeDamage;
    public AudioClip[] firePistol;

   // public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             

    public AudioSource playerFS;
    public AudioSource playerGun;
    public AudioSource playerVoice;

    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    //void Awake()
    //{
    //    //Check if there is already an instance of SoundManager
    //    if (instance == null)
    //        //if not, set it to this.
    //        instance = this;
    //    //If instance already exists:
    //    else if (instance != this)
    //        //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
    //        Destroy(gameObject);

    //    //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
    //    DontDestroyOnLoad(gameObject);
    //}

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(AudioClip[] clips, AudioSource efxSource)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

    protected void PlaySingle(AudioClip clip, AudioSource efxSource)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void FootHitsGround()
    {
        RandomizeSfx(footsteps, playerFS);
    }

    public void TakeDamage()
    {
        RandomizeSfx(takeDamage, playerVoice);
    }

    public void FirePistol()
    {
        RandomizeSfx(firePistol, playerFS);
    }
    public void FireShotgun()
    {
//        RandomizeSfx(fireShotgun, playerFS);
    }
}
