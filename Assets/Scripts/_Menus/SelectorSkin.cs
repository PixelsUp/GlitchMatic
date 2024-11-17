using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectorSkin : MonoBehaviour
{

    public static Sprite SelectedSkin { get; private set; }
    public static RuntimeAnimatorController SelectedAnim { get; private set; }

    // Array de sprites para las diferentes skins del personaje
    [SerializeField] private Sprite[] skins;
    [SerializeField] private List<string> skinNames; // Lista de nombres de las skins
    [SerializeField] private RuntimeAnimatorController[] animControllers; // Array de controladores de animación



    // Referencia a la imagen central que mostrará las skins
    [SerializeField] private Image skinImage;
    [SerializeField] private TextMeshProUGUI nameText; // El texto que muestra el nombre de la skin

    // Índice actual en el array de skins
    private int currentSkinIndex = 0;


    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("MainMenu");
    }

    // Método para cambiar a la skin anterior
    public void CambiarASkinAnterior()
    {
        currentSkinIndex--;
        if (currentSkinIndex < 0)
        {
            currentSkinIndex = skins.Length - 1; // Volver al último skin
        }
        ActualizarSkin();
    }

    // Método para cambiar a la skin siguiente
    public void CambiarASkinSiguiente()
    {
        currentSkinIndex++;
        if (currentSkinIndex >= skins.Length)
        {
            currentSkinIndex = 0; // Volver al primer skin
        }
        ActualizarSkin();
    }

    // Método para actualizar la imagen del skin actual
    private void ActualizarSkin()
    {
        SfxScript.TriggerSfx("SfxButton1");
        skinImage.sprite = skins[currentSkinIndex];
        nameText.text = skinNames[currentSkinIndex];
        SelectedSkin = skins[currentSkinIndex];
        SelectedAnim = animControllers[currentSkinIndex];

    }

}