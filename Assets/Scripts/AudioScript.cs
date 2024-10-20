using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    private AudioSource Source;
    public float fadeInDuration = 2f; // Duración del fade in en segundos

    // Clips de audio para cada escena
    public AudioClip menuMusic;
    public AudioClip gameMusic1;
    public AudioClip gameMusic2;
    public AudioClip gameMusic3;
    public AudioClip gameMusic4;
    public AudioClip gameMusic5;
    public AudioClip gameMusic6;

    private bool isFirstTime = true; // Para controlar el fade in solo la primera vez que se reproduce una canción

    void Start()
    {
        DontDestroyOnLoad(gameObject); // No destruyas este objeto al cambiar de escena
        Source = GetComponent<AudioSource>();

        // Reproducir la música adecuada según la escena inicial
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }

    void OnEnable()
    {
        // Subscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desubscribirse del evento de cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Método que se llama cuando una nueva escena es cargada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Llama al método para cambiar la música cuando la escena cambia
        PlayMusicForScene(scene.name);
    }

    // Método para cambiar la música en función de la escena
    void PlayMusicForScene(string sceneName)
    {
        AudioClip clipToPlay = null;

        // Usamos un switch para asignar la música dependiendo de la escena
        switch (sceneName)
        {
            case "MenuScene":
                clipToPlay = menuMusic;
                break;
            case "Intro_Scene":
                clipToPlay = gameMusic1;
                break;
            case "GameScene2":
                clipToPlay = gameMusic2;
                break;
            case "GameScene3":
                clipToPlay = gameMusic3;
                break;
            case "GameScene4":
                clipToPlay = gameMusic4;
                break;
            case "GameScene5":
                clipToPlay = gameMusic5;
                break;
            case "GameScene6":
                clipToPlay = gameMusic6;
                break;
            default:
                Debug.LogWarning("Escena desconocida, sin música asignada");
                break;
        }

        // Si tenemos una nueva pista, la asignamos y reproducimos con fade in solo la primera vez
        if (clipToPlay != null && Source.clip != clipToPlay)
        {
            Source.clip = clipToPlay;
            Source.Play();

            if (isFirstTime)
            {
                StartCoroutine(FadeIn(Source, fadeInDuration)); // Solo aplicamos el fade in la primera vez
                isFirstTime = false; // Para que no se repita el fade in
            }
            else
            {
                Source.volume = 1f; // Asegurarse de que el volumen esté al máximo después del primer fade in
            }
        }
    }

    // Corutina para hacer el fade in de volumen
    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0f; // Empezamos con el volumen en 0
        float targetVolume = 1f; // El volumen al que queremos llegar

        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, currentTime / duration); // Lerp para un fade suave
            yield return null;
        }

        audioSource.volume = targetVolume; // Asegurarse de que el volumen sea exactamente 1 al final
    }
}
