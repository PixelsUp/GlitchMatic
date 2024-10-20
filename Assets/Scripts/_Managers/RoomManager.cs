using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public int currentRoom = 0;  // Tracks the current room number.
    public int roomsPerTheme = 3; // 2 normal rooms + 1 boss room per theme.
    public int roomsBeforeShop = 7; // After x rooms, shop.
    public string[] themes; // List of available themes (folder names).
    public int coinsPerRoom = 10; // Coins awarded per room cleared.
    public int currentThemeIndex = 0; // Current theme index.

    private int coins = 0; // Coins earned in the run.

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadNextRoom()
    {
        currentRoom++;

        // Check if it’s time to go to a shop.
        if (currentRoom % roomsBeforeShop == 0)
        {
            // Load shop scene
            LoadShop();
        }
        else if (currentRoom % roomsPerTheme == 0)
        {
            // Load boss room
            LoadBossRoom();
        }
        else
        {
            // Load normal room from the current theme
            LoadNormalRoom();
        }
    }

    private void LoadNormalRoom()
    {
        string theme = themes[currentThemeIndex];
        int roomIndex = Random.Range(1, 11); // Assuming 10 rooms per theme.
        string sceneName = $"Gameplay/Themes/{theme}/Normal_Rooms/Room_{roomIndex}";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void LoadBossRoom()
    {
        string theme = themes[currentThemeIndex];
        string sceneName = $"Gameplay/Themes/{theme}/Boss_Rooms/BossRoom";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        currentThemeIndex = (currentThemeIndex + 1) % themes.Length; // Rotate to the next theme.
    }

    private void LoadShop()
    {
        string sceneName = "Gameplay/Shop";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void EndGame()
    {
        // Logic to reward coins based on rooms cleared.
        int totalCoins = coins + (currentRoom * coinsPerRoom);
        // Store this for access in main menu or shop scene.
        PlayerPrefs.SetInt("CoinsEarned", totalCoins);

        // Reset for next run
        currentRoom = 0;
        coins = 0;
    }
}
