using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManagerScript : MonoBehaviour
{
    public GameObject gameOverUI; // Panel de Game Over
    public GameObject canvas;

    private bool dead = false;
    void Start()
    {
        if (gameOverUI == null)
        {
            gameOverUI = GameObject.Find("GameOverScreen");
            if (gameOverUI == null)
            {
                Debug.LogError("No se encontr� GameOverScreen en la escena.");
            }
        }
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        canvas.SetActive(false);
        Debug.Log("Activando pantalla de Game Over");
        if (!dead)
        {
            dead = true;
        }
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.EndGame();
            string user;
            if (PlayerPrefs.HasKey("username"))
            {
                user = PlayerPrefs.GetString("username");
            }
            else
            {
                user = "Player";
            }
            string room = RoomManager.Instance.currentRoom.ToString();
            if (LeaderboardManager.Instance != null)
            {
                LeaderboardManager.Instance.addScore(user, room);
            }
        }
    }

    void DestroySingletonsOnDeath()
    {
        if (_CharacterManager.Instance != null)
        {
            Destroy(_CharacterManager.Instance.gameObject);
        }


        /*// Destruir instancia del RoomManager si existe
        if (RoomManager.Instance != null)
        {
            Destroy(RoomManager.Instance.gameObject);
        }*/

        
        // Destruir instancia del CameraManager si existe
        if (_CameraManager.Instance != null)
        {
            Destroy(_CameraManager.Instance.gameObject);
        }

        /*// Destruir instancia del MusicScript si existe
        if (MusicScript.Instance != null)
        {
            Destroy(MusicScript.Instance.gameObject);
        }

        // Destruir instancia del SfxScript si existe
        if (SfxScript.Instance != null)
        {
            Destroy(SfxScript.Instance.gameObject);
        }*/
    }
    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        DestroySingletonsOnDeath();
        if (LeaderboardManager.Instance != null && LeaderboardManager.Instance.ads)
        {
            SceneManager.LoadScene("Pop_Up_Anuncios");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        //_CharacterManager.ResetLife();

        dead = false;
        Time.timeScale = 1f;
    }
}
