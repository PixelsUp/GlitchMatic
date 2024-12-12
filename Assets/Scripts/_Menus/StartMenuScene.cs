using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartMenuScene : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer; // Referencia al AudioMixer
    [SerializeField] private Transform background; // Referencia al fondo
    [SerializeField] private float movementMultiplier = 0.1f; // Controla el movimiento del fondo
    [SerializeField] private Camera mainCamera; // Cámara principal
    public GameObject quitButton;

    public GameObject sbObject;
    public GameObject sbCanvas;
    public GameObject controlsObject;
    private void Awake()
    {
        // Configura la posición inicial del fondo
        if (background != null)
        {
            //initialBackgroundPosition = background.position;
        }

        // Configura la cámara principal si no está asignada
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        // Aplica la configuración de volumen guardada
        ApplySavedVolumeSettings();

        // Restablece la posición inicial del fondo
        ResetBackgroundPosition();

        if (Application.platform == RuntimePlatform.WebGLPlayer && quitButton != null)
        {
            quitButton.SetActive(false);
        }

    }

    private void Update()
    {
        // Maneja el movimiento del fondo con el cursor
        HandleCursorBackgroundMovement();
    }

    private void HandleCursorBackgroundMovement()
    {
        if (background == null || mainCamera == null) return;

        // Obtén la posición del cursor en pantalla
        Vector3 cursorViewportPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // Si el cursor no está en los límites visibles, vuelve a la posición inicial
        if (cursorViewportPosition.x < 0f || cursorViewportPosition.x > 1f ||
            cursorViewportPosition.y < 0f || cursorViewportPosition.y > 1f)
        {
            background.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            return;
        }

        // Calcula el desplazamiento del fondo basado en la posición del cursor
        Vector3 offset = new Vector3(
            (cursorViewportPosition.x - 0.5f) * movementMultiplier,
            (cursorViewportPosition.y - 0.5f) * movementMultiplier,
            0
        );

        // Actualiza la posición del fondo
        background.position = new Vector3(Screen.width / 2, Screen.height / 2, 0) + offset;
    }

    public void ResetBackgroundPosition()
    {
        if (background != null)
        {
            background.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
    }

    public void Account()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("AccountMenu");
    }

    public void MainMenu()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("MainMenu");
    }

    private void ApplySavedVolumeSettings()
    {
        // Aplica el volumen guardado para cada canal
        if (PlayerPrefs.HasKey("masterVolumen"))
        {
            float mast = PlayerPrefs.GetFloat("masterVolumen");
            myMixer.SetFloat("master", Mathf.Log10(mast) * 20);
        }

        if (PlayerPrefs.HasKey("musicVolumen"))
        {
            float mus = PlayerPrefs.GetFloat("musicVolumen");
            myMixer.SetFloat("music", Mathf.Log10(mus) * 20);
        }

        if (PlayerPrefs.HasKey("sfxVolumen"))
        {
            float sfx = PlayerPrefs.GetFloat("sfxVolumen");
            myMixer.SetFloat("sfx", Mathf.Log10(sfx) * 20);
        }
    }

    public void Quit()
    {
        SfxScript.TriggerSfx("SfxButton1");
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void playSb()
    {
        SfxScript.TriggerSfx("SfxButton1");
        sbObject.SetActive(true);
        sbCanvas.SetActive(true);
    }

    public void backSb()
    {
        SfxScript.TriggerSfx("SfxButton1");
        sbObject.SetActive(false);
        sbCanvas.SetActive(false);
    }
    public void playControls()
    {
        SfxScript.TriggerSfx("SfxButton1");
        controlsObject.SetActive(true);
    }

    public void backControls()
    {
        SfxScript.TriggerSfx("SfxButton1");
        controlsObject.SetActive(false);
    }
}
