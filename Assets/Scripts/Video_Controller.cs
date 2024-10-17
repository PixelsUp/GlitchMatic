using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video_Controller : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Reproduce el video al empezar la escena
        videoPlayer.Play();

        // Opción para realizar una acción cuando termine el video
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        //SceneManager.LoadScene("NextScene"); //Cargas otra escena cuando el video termina
    }
}
