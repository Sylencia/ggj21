using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text countdownText;

    void Start()
    {
        countdownText.text = "3";
    }

    public void SetUIEnabled(bool show)
    {
        countdownText.enabled = show;
    }

    public void UpdateCountdownUI(float time)
    {
        var seconds = Mathf.CeilToInt(time % 60);

        countdownText.text = seconds.ToString();
    }
}