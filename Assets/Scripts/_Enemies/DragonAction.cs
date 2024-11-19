using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAction : MonoBehaviour
{
    public string ActionName;
    public float UtilityValue;

    public System.Action PerformAction;

    public DragonAction(string name, float utility, System.Action action)
    {
        ActionName = name;
        UtilityValue = utility;
        PerformAction = action;
    }
}
