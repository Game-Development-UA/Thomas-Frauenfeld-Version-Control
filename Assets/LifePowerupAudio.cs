using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerupAudio : MonoBehaviour {

    public AudioClip soundEffect;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        }
    }
}
