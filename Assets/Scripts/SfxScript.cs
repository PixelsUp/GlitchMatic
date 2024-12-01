using System;
using UnityEngine;

public class SfxScript : MonoBehaviour
{
    private static SfxScript instance = null;
    private AudioSource sfxSource;
    private float vol_aux = 0.5f;

    // Clips de efectos de sonido
    public AudioClip sfxButton1;
    public AudioClip sfxAttack;
    public AudioClip sfxHurt;
    public AudioClip sfxDead;
    public AudioClip sfxBowShot;
    public AudioClip sfxSword1;
    public AudioClip sfxSword2;
    public AudioClip sfxSword3;
    public AudioClip sfxImpactCaC;
    public AudioClip sfxImpactGun;
    public AudioClip sfxGrunt1;
    public AudioClip sfxGrunt2;
    public AudioClip sfxDash;

    // Evento para reproducir efectos de sonido
    public static event Action<string> OnPlaySfx;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        sfxSource = GetComponent<AudioSource>();
        if (sfxSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en el GameObject para SFX.");
        }

        vol_aux = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        sfxSource.volume = vol_aux;
    }

    void OnEnable()
    {
        // Suscribirse al evento
        OnPlaySfx += PlaySfx;
    }

    void OnDisable()
    {
        // Desuscribirse del evento
        OnPlaySfx -= PlaySfx;
    }

    // Método para invocar el evento desde otros scripts
    public static void TriggerSfx(string sfxName)
    {
        if (OnPlaySfx != null)
        {
            OnPlaySfx.Invoke(sfxName);
        }
    }

    // Método para reproducir efectos de sonido
    private void PlaySfx(string sfxName)
    {
        AudioClip clipToPlay = null;

        switch (sfxName)
        {
            case "SfxButton1":
                clipToPlay = sfxButton1;
                break;
            case "SfxAttack":
                clipToPlay = sfxAttack;
                break;
            case "SfxHurt":
                clipToPlay = sfxHurt;
                break;
            case "SfxDead":
                clipToPlay = sfxDead;
                break;
            case "SfxBowShot":
                clipToPlay = sfxBowShot;
                break;
            case "SfxSword1":
                clipToPlay = sfxSword1;
                break;
            case "SfxSword2":
                clipToPlay = sfxSword2;
                break;
            case "SfxSword3":
                clipToPlay = sfxSword3;
                break;
            case "SfxImpactCaC":
                clipToPlay = sfxImpactCaC;
                break;
            case "SfxImpactGun":
                clipToPlay = sfxImpactGun;
                break;
            case "SfxGrunt1":
                clipToPlay = sfxGrunt1;
                break;
            case "SfxGrunt2":
                clipToPlay = sfxGrunt2;
                break;
            case "SfxDash":
                clipToPlay = sfxDash;
                break;
            default:
                Debug.LogWarning("Efecto de sonido no encontrado");
                return;
        }

        if (clipToPlay != null)
        {
            sfxSource.PlayOneShot(clipToPlay); // Usamos PlayOneShot para que no se interrumpan otros sonidos
        }
    }
}
