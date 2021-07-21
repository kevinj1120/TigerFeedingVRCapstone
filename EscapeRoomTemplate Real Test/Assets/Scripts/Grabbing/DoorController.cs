using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool _doorHasBeenOpened = false;
    public AudioSource doorUnlockAudioSource;
    public AudioSource escapeSoundAudioSource;
    public AudioSource ambientAudioSource;
    public AudioClip songAudioClip;

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
       
    }
}
