using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }

    public int currentRoom = 0;  // Tracks the current room number.
    public int roomsPerTheme = 3; // 2 normal rooms + 1 boss room per theme.
    public int roomsBeforeShop = 7; // After x rooms, shop.
    public string[] Theme; // List of available themes (folder names).
    public int coinsPerRoom = 10; // Coins awarded per room cleared.
    public int currentThemeIndex = 1; // Current theme index.

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

    // metodo para iniciar el punto de cambio de escenas
    // pilla el componente con el script metido, solo es un collider con isTrigger
    public void EnableTransitionPoint()
    {
        GameObject transitionPoint = GameObject.Find("TransitionPoint");
        if (transitionPoint != null)
        {
            transitionPoint.GetComponent<TransitionPoint>().ActivateTransition();
        }
    }

    public void LoadNextRoom()
    {
        Debug.Log(currentRoom);
        currentRoom++;

        // Check if it�s time to go to a shop.
        //if (currentRoom % roomsBeforeShop == 0)
        //{
            // Load shop scene
        //    LoadShop();
        //}
        if (currentRoom % roomsPerTheme == 0)
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
        int earnedCoins = currentRoom * coinsPerRoom;
        string theme = Theme[currentThemeIndex];
        int roomIndex = Random.Range(1, 4); // Assuming 10 rooms per theme.
        string sceneName = $"Scenes/Gameplay/Themes/{theme}/Normal_Rooms/Room_{roomIndex}";
        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += (scene, mode) => PlacePlayerAtSpawn();
        Debug.Log("Nuemro de escena: " + currentRoom);
    }

    private void LoadBossRoom()
    {
        string theme = Theme[currentThemeIndex];
        string sceneName = $"Scenes/Gameplay/Themes/{theme}/Boss_Rooms/BossRoom";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        currentThemeIndex = (currentThemeIndex + 1) % Theme.Length; // Rotate to the next theme.
    }

    private void LoadShop()
    {
        string sceneName = "Gameplay/Shop";
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // Llama a esta funci�n al cargar una nueva escena, esta para poner el personaje en el spawnpoint
    private void PlacePlayerAtSpawn()
    {
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null && _CharacterManager.Instance != null)
        {
            _CharacterManager.Instance.transform.position = spawnPoint.transform.position;
        }
    }
    public void EndGame()
    {
        // Calcula las monedas totales ganadas en la partida actual.
        int earnedCoins = currentRoom * coinsPerRoom;
        Debug.Log("Total de monedas ganadas: " + earnedCoins);

        // Acumula el total de monedas almacenado en PlayerPrefs.
        int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Obtiene el valor actual o 0 si no existe.
        totalCoins += earnedCoins; // Suma las monedas ganadas en esta partida.
        PlayerPrefs.SetInt("TotalCoins", totalCoins); // Guarda el nuevo total en PlayerPrefs.

        Debug.Log("Total de monedas guardadas: " + totalCoins);

        // Reinicia las variables para la pr�xima partida.
        currentRoom = 0;
    }
    // M�todo para resetear el n�mero de sala, llamable desde otros scripts.
    public void ResetRoom()
    {
        currentRoom = 0;
        Debug.Log("N�mero de sala reseteado a 0.");
    }
}
