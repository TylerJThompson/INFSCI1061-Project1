using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    AudioSource audio;

    void OnEnable()
    {
        audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
    }
}
