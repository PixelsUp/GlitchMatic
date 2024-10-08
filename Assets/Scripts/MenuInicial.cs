using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Intro_Scene");
    }

    public void Character()
    {
        SceneManager.LoadScene("Character_Scene");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop_Scene");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits_Scene");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options_Scene");
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard_Scene");
    }

    public void Quit()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

}