using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimeStamp;
    [SerializeField] bool playing;
    private float timer;
    int minutes;
    int seconds;
    int milliseconds;

    private void Awake()
    {
        TimeStamp = GameObject.Find("TimeStamp").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        playing = true;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (playing == true)
        {
            timer += Time.deltaTime;
            minutes = Mathf.FloorToInt(timer / 60f);
            seconds = Mathf.FloorToInt(timer % 60f);
            milliseconds = Mathf.FloorToInt((timer * 100f) % 100f);
            TimeStamp.text = "Time " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            timer = 0;
        }
    }

    public void ResetTimer()
    {
        timer = 0;
    }
}
