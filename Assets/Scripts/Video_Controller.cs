using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Video_Controller : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Obtiene el componente VideoPlayer adjunto al mismo objeto
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer == null)
        {
            Debug.LogError("No se encontró un componente VideoPlayer en el GameObject.");
            return;
        }

        // Reproduce el video al empezar la escena
        videoPlayer.Play();

        // Agrega el método EndReached al evento loopPointReached
        videoPlayer.loopPointReached += EndReached;
    }

    // Método que se llama cuando el video termina
    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene("MenuInicial"); // Reemplaza "NombreDeLaEscena" con el nombre de tu escena objetivo
    }
}
