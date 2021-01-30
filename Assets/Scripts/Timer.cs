using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    
    void Start()
    {
        timerText.text = "00:00";
    }

    public void SetUIEnabled(bool show)
    {
        timerText.enabled = show;
    }

    public void UpdateTimerUI(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}