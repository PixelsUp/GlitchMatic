using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer; // Referencia al AudioMixer

    private void Start()
    {
        ApplySavedVolumeSettings(); // Aplicar los valores guardados al iniciar la escena
    }

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

    // Método para cargar y aplicar los valores de volumen guardados
    private void ApplySavedVolumeSettings()
    {
        // Verifica si hay valores guardados y los aplica
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
}