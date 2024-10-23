using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScene : MonoBehaviour
{
    public void Account()
    {
        SceneManager.LoadScene("AccountMenu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
