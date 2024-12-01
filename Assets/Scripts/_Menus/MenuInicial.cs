using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    // Variables para el movimiento del fondo
    [SerializeField] private Transform background; // Referencia al fondo
    [SerializeField] private float movementMultiplier = 0.1f; // Controla cu�nto se mueve el fondo
    [SerializeField] private Camera mainCamera; // C�mara principal

    private Vector3 initialBackgroundPosition; // Guarda la posici�n inicial del fondo

    private void Awake()
    {
        // Guarda la posici�n inicial del fondo
        if (background != null)
        {
            initialBackgroundPosition = background.position;
        }

        // Configura la c�mara principal si no est� asignada
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Start()
    {
        // Establece la posici�n inicial del fondo
        ResetBackgroundPosition();
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
    }

    private void HandleCursorBackgroundMovement()
    {
        if (background == null || mainCamera == null) return;

        // Obt�n la posici�n del cursor en pantalla
        Vector3 cursorViewportPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);

        // Si el cursor no est� en los l�mites visibles, vuelve a la posici�n inicial
        if (cursorViewportPosition.x < 0f || cursorViewportPosition.x > 1f ||
            cursorViewportPosition.y < 0f || cursorViewportPosition.y > 1f)
        {
            background.position = initialBackgroundPosition;
            return;
        }

        // Calcula el desplazamiento del fondo bas�ndose en la posici�n del cursor en la pantalla
        Vector3 offset = new Vector3(
            (cursorViewportPosition.x - 0.5f) * movementMultiplier,
            (cursorViewportPosition.y - 0.5f) * movementMultiplier,
            0
        );

        // Ajusta la posici�n del fondo
        background.position = initialBackgroundPosition + offset;
    }

    public void ResetBackgroundPosition()
    {
        if (background != null)
        {
            background.position = initialBackgroundPosition;
        }
    }

    public void Play()
    {
        SfxScript.TriggerSfx("SfxButton1");
        //SceneManager.LoadScene("Intro_Scene");

        // Llamar a la referencia de la funci�n del RoomManager
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

    public void Quit()
    {
        SfxScript.TriggerSfx("SfxButton1");
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("StartMenu");
    }
}
