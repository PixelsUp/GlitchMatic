using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject PauseUI; // Panel de Game Over

    public _CharacterManager characterManager;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool dead = false;

    public GameObject sureText;
    public bool completa;

    void Start()
    {
        if (PauseUI == null)
        {
            PauseUI = GameObject.Find("PauseScreen");
            if (PauseUI == null)
            {
                Debug.LogError("No se encontró PauseScreen en la escena.");
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Activando pantalla de Pause");
        characterManager.isPaused = false;
        characterManager.puedeDisparar = true;
        PauseUI.SetActive(false);
    }
    public void Pause()
    {
        Debug.Log("Activando pantalla de Pause");
        characterManager.isPaused = true;
        PauseUI.SetActive(true);
    }

    public void Exit()
    {
        SfxScript.TriggerSfx("SfxButton1");
        DestroySingletonsOnDeath();
        SceneManager.LoadScene("MainMenu");

        //_CharacterManager.ResetLife();

        dead = false;
        Time.timeScale = 1f;
    }

    public void Back()
    {
        Resume();
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("masterVolumen") || PlayerPrefs.HasKey("musicVolumen") || PlayerPrefs.HasKey("sfxVolumen"))
        {
            LoadVolume();
        }
        setMasterVolume();
        setMusicVolume();
        setSfxVolume();
    }

    public void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolumen");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolumen");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolumen");
    }

    public void setMasterVolume()
    {
        float mast = masterSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(mast) * 20);
        PlayerPrefs.SetFloat("masterVolumen", mast);
    }

    public void setMusicVolume()
    {
        float mus = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(mus) * 20);
        PlayerPrefs.SetFloat("musicVolumen", mus);
    }

    public void setSfxVolume()
    {
        float sfx = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(sfx) * 20);
        PlayerPrefs.SetFloat("sfxVolumen", sfx);
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
    public void FullScreen()
    {
        SfxScript.TriggerSfx("SfxButton1");
        if (!completa)
        {
            //sureText.SetActive(true);
        }
        Screen.fullScreen = !Screen.fullScreen;
        completa = true;
    }

    public void quitSureText()
    {
        sureText.SetActive(false);
        completa = false;
    }
}
