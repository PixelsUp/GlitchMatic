using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    // Variables para el movimiento del fondo
    [SerializeField] private Transform background; // Referencia al fondo
    [SerializeField] private float movementMultiplier = 0.1f; // Controla cuánto se mueve el fondo
    [SerializeField] private Camera mainCamera; // Cámara principal

    public GameObject controlsObject;

    private void Awake()
    {
        // Guarda la posición inicial del fondo
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
        // Establece la posición inicial del fondo
        ResetBackgroundPosition();
        //Debug.Log("INITIAL: "+ initialBackgroundPosition);
    }

    private void Update()
    {
        // Controla el movimiento del fondo con el cursor
        HandleCursorBackgroundMovement();

        // Detecta la tecla ESC para volver
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
        //Debug.Log("BG POS: " + background.position);
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
            background.position = new Vector3 (Screen.width/2, Screen.height/2, 0);
            return;
        }

        // Calcula el desplazamiento del fondo basándose en la posición del cursor en la pantalla
        Vector3 offset = new Vector3(
            (cursorViewportPosition.x - 0.5f) * movementMultiplier,
            (cursorViewportPosition.y - 0.5f) * movementMultiplier,
            0
        );

        // Ajusta la posición del fondo
        background.position = new Vector3(Screen.width / 2, Screen.height / 2, 0) + offset;
        //background.position = new Vector2((initialBackgroundPosition.x + offset.x) * );
    }

    public void ResetBackgroundPosition()
    {
        if (background != null)
        {
            background.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
    }

    public void Play()
    {
        SfxScript.TriggerSfx("SfxButton1");
        //SceneManager.LoadScene("Intro_Scene");

        // Llamar a la referencia de la función del RoomManager
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.ResetRoom();
            RoomManager.Instance.LoadNextRoom();
            RoomManager.Instance.earnedCoins = 0;
        }
    }

    public void Character()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("Character_Scene");
    }

    public void Shop()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("Shop_Scene");
    }

    public void Credits()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("Credits_Scene");
    }

    public void Options()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("Options_Scene");
    }

    public void Leaderboard()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("Leaderboard_Scene");
    }

    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("StartMenu");
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
