using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    private static MusicScript instance = null; // Variable para mantener una única instancia
    private AudioSource Source;
    public float fadeInDuration = 2f; // Duración del fade in en segundos
    private float vol_aux = 0.5f;

    // Clips de audio para cada escena
    public AudioClip startMusic;
    public AudioClip menuMusic;
    public AudioClip gameMusic1;
    public AudioClip gameMusic2;
    public AudioClip gameMusic3;
    public AudioClip gameMusic4;
    public AudioClip gameMusic5;
    public AudioClip gameMusic6;
    public AudioClip deadMusic;
    public AudioClip bossUp50;
    public AudioClip bossUnder50;

    void Awake()
    {
        // Verifica si ya existe una instancia de este objeto, si es así destruye la nueva
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruyas este objeto al cambiar de escena
        }

        Source = GetComponent<AudioSource>();
        if (Source == null)
        {
            Debug.LogError("No se encontró un AudioSource en el GameObject.");
        }
    }

    void Start()
    {
        // Asegúrate de que el volumen sea 0 para hacer el fade in después
        Source.volume = 0f;
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

    // Método para ambiar la música en función de la escena
    void PlayMusicForScene(string sceneName)
    {
        AudioClip clipToPlay = null;

        // Usamos un switch para asignar la música dependiendo de la escena
        switch (sceneName)
        {
            case "StartMenu":
                clipToPlay = startMusic;
                break;
            case "MainMenu":
                clipToPlay = menuMusic;
                break;
            case "Intro_Scene":
                // Crear un arreglo con las pistas de música para la escena Intro_Scene
                AudioClip[] gameMusic = { gameMusic1, gameMusic2, gameMusic3, gameMusic4, gameMusic5, gameMusic6 };

                // Seleccionar un índice aleatorio del arreglo
                int randNum = Random.Range(0, gameMusic.Length); // Esto generará un número entre 0 y 5
                clipToPlay = gameMusic[randNum]; // Asignar la pista aleatoria
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
                return;
        }

        // Verifica si el clip a reproducir es el mismo que ya está en el AudioSource
        if (Source.clip == clipToPlay)
        {
            return; // No hacer nada si la música ya está sonando y es la misma
        }

        // Si tenemos una nueva pista, la asignamos y reproducimos
        Source.clip = clipToPlay;
        Source.Play();

        // Siempre hacemos el fade in cuando se cambia la música
        StartCoroutine(FadeIn(Source, fadeInDuration));
    }

    public void PlaySpecificMusic(AudioClip clip)
    {
        if (Source.clip == clip) return;

        Source.Stop();
        Source.clip = clip;
        Source.Play();
    }

    // Método estático para acceder al método PlaySpecificMusic desde otros scripts
    public static void TriggerMusic(AudioClip clip)
    {
        if (instance != null)
        {
            instance.PlaySpecificMusic(clip);
        }
        else
        {
            Debug.LogWarning("No se encontró la instancia de MusicScript.");
        }
    }

    // Corutina para hacer el fade in de volumen
    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0f; // Empezamos con el volumen en 0
        float targetVolume = vol_aux; // El volumen al que queremos llegar

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
