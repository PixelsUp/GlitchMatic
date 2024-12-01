using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI coinDisplay; // TextMeshPro for displaying coins
    public Button weapon1Button, weapon2Button, weapon3Button;
    public int weapon1Cost = 100, weapon2Cost = 300, weapon3Cost = 250;
    public Sprite boughtSprite;
    public GameObject noAdsButton;

    void Start()
    {
        UpdateCoinDisplay();

        // Check purchase status and update button states
        UpdateButtonStates();

        // Assign button click events
        weapon1Button.onClick.AddListener(() => BuyWeapon(1, weapon1Cost));
        weapon2Button.onClick.AddListener(() => BuyWeapon(2, weapon2Cost));
        weapon3Button.onClick.AddListener(() => BuyWeapon(3, weapon3Cost));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    private void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 100);
        coinDisplay.text = "Coins: " + totalCoins.ToString();
    }

    private void BuyWeapon(int weaponID, int cost)
    {
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 100);

        if (totalCoins >= cost)
        {
            totalCoins -= cost;
            PlayerPrefs.SetInt("TotalCoins", totalCoins); // Save the updated coin count
            PlayerPrefs.SetInt("Weapon" + weaponID + "Purchased", 1); // Mark this weapon as purchased

            UpdateCoinDisplay();
            UpdateButtonStates(); // Update button states after purchase
            Debug.Log("Weapon " + weaponID + " purchased!");
        }
        else
        {
            Debug.Log("Not enough coins to purchase Weapon " + weaponID);
        }
    }

    private void UpdateButtonStates()
    {
        // Disable buttons if weapons are already purchased
        if (PlayerPrefs.GetInt("Weapon1Purchased", 0) == 1)
        {
            weapon1Button.interactable = false;
            weapon1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
            weapon1Button.GetComponent<Image>().sprite = boughtSprite;
        }
        if (PlayerPrefs.GetInt("Weapon2Purchased", 0) == 1)
        {
            weapon2Button.interactable = false;
            weapon2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
            weapon2Button.GetComponent<Image>().sprite = boughtSprite;
        }
        if (PlayerPrefs.GetInt("Weapon3Purchased", 0) == 1)
        {
            weapon3Button.interactable = false;
            weapon3Button.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
            weapon3Button.GetComponent<Image>().sprite = boughtSprite;
        }
    }
    
    public void noAds()
    {
        noAdsButton.SetActive(false);
        if (LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.ads = false;
        }
    }
    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("MainMenu");
    }
}
