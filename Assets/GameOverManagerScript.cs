using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManagerScript : MonoBehaviour
{
    public GameObject gameOverUI; // Panel de Game Over

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
        gameOverUI.SetActive(true);
    }
}
