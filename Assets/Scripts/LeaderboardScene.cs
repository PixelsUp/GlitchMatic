using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LeaderboardManager.Instance != null)
        {
            LeaderboardManager.Instance.Back();
        }
    }

}
