using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay;
    private int totalCoins;
    void Start()
    {
        totalCoins = 0;
        UpdateCoinDisplay();
    }

    void UpdateCoinDisplay()
    {
        totalCoins = totalCoins + 10;
        coinDisplay.text = "Coins: " + totalCoins.ToString();
    }

    void OnEnable()
    {
        // Subscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Desubscribirse del evento de cambio de escena
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // M�todo que se llama cuando una nueva escena es cargada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Llama al m�todo para cambiar la m�sica cuando la escena cambia
        UpdateCoinDisplay();
    }
}