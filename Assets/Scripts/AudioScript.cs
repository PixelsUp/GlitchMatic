using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource Source;
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if (!Source.isPlaying) {
        //    Source.Play();
        //}
    }
}

