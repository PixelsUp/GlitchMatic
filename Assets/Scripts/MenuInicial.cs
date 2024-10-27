using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Play()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("Intro_Scene");
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
}