using UnityEngine;
using TMPro;

public class CoinDisplayUI : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay;

    void Start()
    {
        UpdateCoinDisplay();
    }

    void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coinDisplay.text = "Coins: " + totalCoins.ToString();
    }
}