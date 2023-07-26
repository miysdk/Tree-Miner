using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public TextMeshProUGUI logsText;

    public int logs = 0;

    private void Start()
    {
        if (instance is not null && instance != this)
        {
            Destroy(this);
        }
        else instance = this;

        logsText.text = logs.ToString();
    }

    public void IncreaseLogs(int value)
    {
        logs += value;
        logsText.text = logs.ToString();
    }
}
