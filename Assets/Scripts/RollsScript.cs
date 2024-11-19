using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollsScript : MonoBehaviour
{
    public TextMeshProUGUI rollsDisplay;
    public _CharacterManager _characterManager;
    private int rolls;
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
        rollsDisplay.text = "Rolls: " + rolls.ToString();
    }
}
