using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccountScene : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
