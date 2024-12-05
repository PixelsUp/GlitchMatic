using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Video_Controller : MonoBehaviour
{
    // Lista de imágenes para mostrar
    public Sprite[] imageBank; // Banco de imágenes
    public Image displayImage; // Referencia al componente UI Image
    public Slider timerSlider; // Referencia al componente Slider
    public string nextSceneName = "MainMenu"; // Nombre de la próxima escena
    public float delay = 10f; // Tiempo en segundos antes de cambiar de escena

    private float elapsedTime = 0f; // Tiempo transcurrido

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

        if (timerSlider == null)
        {
            Debug.LogError("No se asignó un Slider. Arrastra un objeto Slider al campo 'timerSlider'.");
            return;
        }

        // Asigna una imagen aleatoria del banco al componente Image
        displayImage.sprite = GetRandomImage();

        // Configura el Slider
        timerSlider.maxValue = delay; // Configura el rango del Slider de 0 a delay
        timerSlider.value = 0f;       // Inicia el Slider en 0
    }

    void Update()
    {
        // Incrementa el tiempo transcurrido
        elapsedTime += Time.deltaTime;

        // Actualiza el Slider con el tiempo transcurrido
        timerSlider.value = elapsedTime;

        // Cambia de escena cuando el tiempo transcurrido supera el delay
        if (elapsedTime >= delay)
        {
            ChangeScene();
        }
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

