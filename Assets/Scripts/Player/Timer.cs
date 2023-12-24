using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f;
    public Text timerText;

    private float currentTime;

    private bool puzzle = false;
    private  bool isClear;

    public bool Puzzle
    {
        set { puzzle = value; }
    }
    private void Start()
    {
        isClear = false; ;
        currentTime = totalTime;
        UpdateTimer();
        puzzle = false;
    }

    private void Update()
    {
        if (currentTime > 0 && !isClear)
        {
            if (puzzle == false) return;
            currentTime -= Time.deltaTime;
            UpdateTimer();
        }
        else
        {
            currentTime = 0;
        }
    }

    /// <summary>
    /// ƒQ[ƒ€ƒNƒŠƒA‚ÉŒÄ‚ÔŠÖ”
    /// </summary>
    public void GameClear() 
    {
        isClear = true;
        ScoreManager.score += (int)currentTime * 100;
    }


    private void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
