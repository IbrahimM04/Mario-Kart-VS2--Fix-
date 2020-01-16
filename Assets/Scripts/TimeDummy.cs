using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDummy : MonoBehaviour {
    [SerializeField] Text TimeStamp;
    [SerializeField] bool playing;
    private float Timer;
    int minutes;
    int seconds;
    int milliseconds;

    private void Start() {
        playing = false;
    }

    private void Update() {
        if (playing == true) {
            Timer += Time.deltaTime;
            minutes = Mathf.FloorToInt(Timer / 60f);
            seconds = Mathf.FloorToInt(Timer % 60f);
            milliseconds = Mathf.FloorToInt((Timer * 100f) % 100f);
            TimeStamp.text = "Time " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Timer = 0;
        }
   
    }
}
