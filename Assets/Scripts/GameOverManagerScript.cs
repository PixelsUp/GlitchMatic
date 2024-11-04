using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManagerScript : MonoBehaviour
{
    public GameObject gameOverUI; // Panel de Game Over

    private bool dead = false;

    void Start()
    {
        if (gameOverUI == null)
        {
            gameOverUI = GameObject.Find("GameOverScreen");
            if (gameOverUI == null)
            {
                Debug.LogError("No se encontró GameOverScreen en la escena.");
            }
        }
    }

    public void gameOver()
    {
        Debug.Log("Activando pantalla de Game Over");
        if (!dead)
        {
            SfxScript.TriggerSfx("SfxDead");
            dead = true;
        }
        gameOverUI.SetActive(true);
    }

    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("MainMenu");
        //_CharacterManager.ResetLife();

        dead = false;
        Time.timeScale = 1f;
    }
}
