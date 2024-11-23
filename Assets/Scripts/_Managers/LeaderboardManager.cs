using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    [SerializeField] string username;
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI leaderboardText; // Referencia al texto del leaderboard

    string url = "http://glitchmaticv2-env.eba-cpptua3t.eu-west-3.elasticbeanstalk.com/";

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

    // Método que se llama cuando una nueva escena es cargada
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // Llama al método para cambiar la música cuando la escena cambia
        if (scene.name.Contains("Leaderboard_Scene"))
        {
            GetComponentInChildren<Canvas>().enabled = true;
            getScores();
        }
    }

    public void addScore(string username, string score)
    {
        StartCoroutine(httpCor("addScore/" + username + "/" + score + "/"));
    }
    public void getScores()
    {
        StartCoroutine(httpCor("getScores/"));
    }

    IEnumerator httpCor(string header)
    {
        UnityWebRequest www = new UnityWebRequest(url + header, "GET", new DownloadHandlerBuffer(), new UploadHandlerRaw(new byte[0]));

        yield return www.SendWebRequest();
        if (www.error == null)
        {
            Debug.Log(www.downloadHandler.text);

            // Procesa y actualiza el texto del leaderboard
            UpdateLeaderboard(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("HTTP ERROR:");
            Debug.Log(www.error);
        }
        www.Dispose();
    }

    // Método para actualizar el texto del leaderboard
    void UpdateLeaderboard(string rawData)
    {
        // Supongamos que `rawData` tiene el formato "Jugador1:100,Jugador2:90,Jugador3:80..."
        string[] entries = rawData.Split(',');
        leaderboardText.text = ""; // Limpia el texto existente

        for (int i = 0; i < entries.Length; i++)
        {
            leaderboardText.text += (i + 1) + ". " + entries[i] + "\n"; // Añade cada entrada
        }
    }

    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("MainMenu");
        GetComponentInChildren<Canvas>().enabled = false;
    }

}
