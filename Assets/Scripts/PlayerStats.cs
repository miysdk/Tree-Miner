using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int logs = 0;

    private void Start()
    {
        if (instance is not null && instance != this)
        {
            Destroy(this);
        }
        else instance = this;
    }

    public void IncreaseLogs(int value)
    {
        logs += value;
    }
}
