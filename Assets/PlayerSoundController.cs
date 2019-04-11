using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioClip footsteps;

    public void FootHitsGround()
    {
        SoundManager.instance.PlaySingle(footsteps);
    }
}
