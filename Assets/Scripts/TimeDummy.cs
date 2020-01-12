using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDummy : MonoBehaviour {
    [SerializeField] Text TimeStamp;
    [SerializeField] bool playing;
    private float Timer;

    private void Start() {
        playing = true;
    }

    private void Update() {
        if (playing == true) {
            Timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(Timer / 60f);
            int seconds = Mathf.FloorToInt(Timer % 60f);
            int milliseconds = Mathf.FloorToInt((Timer * 100f) % 100f);
            TimeStamp.text = "Time " + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("000");
        }
    }
}
