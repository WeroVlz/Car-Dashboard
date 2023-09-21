using System;
using TMPro;
using UnityEngine;

/// <summary>
/// This class calculates real time and displays it.
/// </summary>
public class TimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI time;

    void Start(){
        
    }

    void Update()
    {
        DateTime currentTime = DateTime.Now;
        string formattedTime = currentTime.ToString("HH:mm");
        time.text = formattedTime;
    }
}
