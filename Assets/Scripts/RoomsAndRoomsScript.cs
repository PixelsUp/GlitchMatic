using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoomsAndRoomsScript : MonoBehaviour
{
    public TextMeshProUGUI rollsDisplay;
    public TextMeshProUGUI roomsDisplay;
    public TextMeshProUGUI finalRoomsDisplay;
    public _CharacterManager _characterManager;
    private int rolls;
    private int rooms;
    void Start()
    {
        rolls = 2;
        UpdateRollsDisplay();
    }

    private void Update()
    {
        UpdateRollsDisplay();
    }
    void UpdateRollsDisplay()
    {
        rolls = _characterManager.currentRollCharges;
        rollsDisplay.text = "Rolls: " + rolls;
    }
    void UpdateRoomDisplay()
    {
        rooms = RoomManager.Instance.currentRoom;
        roomsDisplay.text = "Rooms: " + rooms;
        finalRoomsDisplay.text = "Rooms Cleared: " + rooms;
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
        UpdateRoomDisplay();
    }
}
