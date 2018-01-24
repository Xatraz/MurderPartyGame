using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text gameTimerText;
    public float gameDuration = 60f;
    public float killerBonusTime = 15f;
    public float gameTime;
    public Text startTimerText;
    public float startDuration = 3f;
    public float startTime;
    public bool playerMayMove = false;

    private void Start()
    {
        gameDuration += startDuration;
    }

    void Update ()
    {
        MatchStartTimer();

        gameTime = gameDuration - Time.timeSinceLevelLoad;

        string minutes = ((int)gameTime / 60).ToString();
        string seconds = (gameTime % 60).ToString("f2");

        gameTimerText.text = minutes + ":" + seconds;

        if (startTime > 0)
            MatchStartTimer();

        else
            Destroy(startTimerText);
    }

    void KillerBonusTime()
    {
        gameDuration += killerBonusTime;
    }

    void MatchStartTimer()
    {
        startTime = startDuration - Time.timeSinceLevelLoad;
        string seconds = ((int)startTime).ToString();
        if (startTime < 1)
        {
            seconds = "GO";
            playerMayMove = true;
        }
        startTimerText.text = seconds;
        
    }
}
