using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Video_Controller : MonoBehaviour
{
    // Lista de imágenes para mostrar
    public Sprite[] imageBank; // Banco de imágenes
    public Image displayImage; // Referencia al componente UI Image
    public string nextSceneName = "MainMenu"; // Nombre de la próxima escena
    public float delay = 10f; // Tiempo en segundos antes de cambiar de escena

    void Start()
    {
        // Verifica que se hayan asignado las imágenes y el componente UI Image
        if (imageBank == null || imageBank.Length == 0)
        {
            Debug.LogError("El banco de imágenes está vacío. Agrega sprites al array 'imageBank'.");
            return;
        }

        if (displayImage == null)
        {
            Debug.LogError("No se asignó un componente Image. Arrastra un objeto UI Image al campo 'displayImage'.");
            return;
        }

        // Asigna una imagen aleatoria del banco al componente Image
        displayImage.sprite = GetRandomImage();

        // Inicia el temporizador para cambiar de escena
        Invoke("ChangeScene", delay);
    }

    // Método para obtener una imagen aleatoria
    Sprite GetRandomImage()
    {
        int randomIndex = Random.Range(0, imageBank.Length);
        return imageBank[randomIndex];
    }

    // Cambiar de escena después del delay
    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
