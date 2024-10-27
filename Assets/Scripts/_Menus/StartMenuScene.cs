using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScene : MonoBehaviour
{
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
}
