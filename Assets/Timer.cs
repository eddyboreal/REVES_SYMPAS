using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Text TimerText = default;
    public bool CanStart = false;

    private float timer = 0f;
    private float startTime = 1.6f;
    private LevelManager levelManager = default;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        if (!CanStart)
        {
            startTime -= Time.deltaTime;
            if(startTime <= 0f)
            {
                CanStart = true;
            }
        }

        if(!levelManager.IsLevelOver() && CanStart)
        {
            timer += Time.deltaTime * (1f / Time.timeScale);

            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer % 60F);
            int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
            TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
    }
}
