using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeCounter;
    TimeSpan timePlaying;
    bool timerGoing;
    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.enabled = true;
        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingValue = "Time:" + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingValue;

            yield return null;
        }
    }
}
