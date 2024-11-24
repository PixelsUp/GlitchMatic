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
        // Dividir el rawData por líneas
        string[] entries = rawData.Split(new[] { '\n', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<KeyValuePair<string, int>> leaderboardEntries = new List<KeyValuePair<string, int>>();

        foreach (string entry in entries)
        {
            if (!string.IsNullOrWhiteSpace(entry) && entry.Contains(":"))
            {
                // Limpiar espacios extra alrededor de cada entrada
                string[] parts = entry.Split(':');
                if (parts.Length == 2)
                {
                    string playerName = parts[0].Trim(); // Eliminar espacios del nombre
                    string scoreString = parts[1].Trim(); // Eliminar espacios del puntaje

                    if (int.TryParse(scoreString, out int playerScore))
                    {
                        leaderboardEntries.Add(new KeyValuePair<string, int>(playerName, playerScore));
                    }
                    else
                    {
                        Debug.LogWarning($"Error al procesar puntaje: {entry}");
                    }
                }
            }
        }

        // Ordenar las entradas por puntajes de mayor a menor
        leaderboardEntries.Sort((a, b) => b.Value.CompareTo(a.Value));

        // Actualizar el texto del leaderboard
        leaderboardText.text = ""; // Limpia el texto existente
        for (int i = 0; i < leaderboardEntries.Count; i++)
        {
            var entry = leaderboardEntries[i];
            leaderboardText.text += $"{i + 1}. {entry.Key}: {entry.Value}\n";
        }

        // Debug adicional para ver el resultado procesado
        Debug.Log($"Leaderboard actualizado: \n{leaderboardText.text}");
    }

    public void Back()
    {
        SfxScript.TriggerSfx("SfxButton1");
        SceneManager.LoadScene("MainMenu");
        GetComponentInChildren<Canvas>().enabled = false;
    }

}
