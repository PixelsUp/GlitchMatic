using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardScript : MonoBehaviour
{
    [SerializeField] string username;
    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI leaderboardText; // Referencia al texto del leaderboard

    string url = "http://glitchmaticv2-env.eba-cpptua3t.eu-west-3.elasticbeanstalk.com/";

    private void Start()
    {
        getScores();
    }

    public void addScore()
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
    }
}
