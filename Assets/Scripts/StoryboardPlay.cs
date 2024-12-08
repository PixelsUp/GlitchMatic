using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class StoryboardPlay : MonoBehaviour
{
    [SerializeField] private string videoUrl = "https://pixelsup.github.io/StoryboardVideo/StoryGlitchmatic.mp4";
    private VideoPlayer videoPlayer;
    public GameObject sbObject;
    public GameObject sbCanvas;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            videoPlayer.url = videoUrl;
            videoPlayer.playOnAwake = false;
            videoPlayer.prepareCompleted += OnVideoPrepared;
            videoPlayer.loopPointReached += OnVideoFinished; // Detectar el final del video
        }
    }

    private void Update()
    {
        // Detecta la tecla ESC para volver
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backSb();
        }
    }

    void OnEnable()
    {
        if (videoPlayer)
        {
            videoPlayer.Stop(); // Detén cualquier reproducción previa
            videoPlayer.time = 0; // Reinicia el video desde el principio
            videoPlayer.Prepare(); // Prepara el video para reproducirlo
        }
    }

    private void OnVideoPrepared(VideoPlayer source)
    {
        videoPlayer.Play(); // Reproduce el video automáticamente
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        StartCoroutine(WaitAndBack(0.3f)); // Agregar una pequeña pausa antes de desactivar
    }

    private IEnumerator WaitAndBack(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        backSb(); // Llama a la función para desactivar los objetos
    }

    public void backSb()
    {
        sbObject.SetActive(false);
        sbCanvas.SetActive(false);
    }
}
