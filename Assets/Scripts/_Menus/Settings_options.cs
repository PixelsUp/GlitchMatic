using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings_options : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("masterVolumen") || PlayerPrefs.HasKey("musicVolumen") || PlayerPrefs.HasKey("sfxVolumen")){
            LoadVolume();
        }
        setMasterVolume();
        setMusicVolume();
        setSfxVolume();
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolumen");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolumen");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolumen");
    }

    public void setMasterVolume()
    {
        float mast = masterSlider.value;
        myMixer.SetFloat("master", Mathf.Log10(mast)*20);
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

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
