﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    bool isPaused;
    public GameObject pauseMenu;
    public Text time;
    public Text timeBig;
    public Text timeCheck1;
    public float timer = 0.0f;
    public float pTimer = 0.0f;
    public float checkTimer = 0.0f;
    public bool timerCheck = true;
    public bool checkPointCheck = false;
    CarMovement car;

    // Use this for initialization
    void Start()
    {
        car = GameObject.FindGameObjectWithTag("Player").GetComponent<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCheck != false)
        {
            timer += Time.deltaTime;
            pTimer += Time.deltaTime;
            time.text = "Time: " + timer.ToString();
            PlayerPrefs.SetFloat("timer1", checkTimer);
        }

        if (checkPointCheck == true)
        {
            timeCheck1.text = "Check Point 1 = " + PlayerPrefs.GetFloat("timer1");
        }

        //the pause menu button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused == true)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else if (isPaused == false)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
        timeBig.text = "Best Time = " + PlayerPrefs.GetFloat("timer");
    }
    void OnApplicationQuit()
    {
        if (timer < pTimer)
            // save the time when you quit the game
            PlayerPrefs.SetFloat("timer", pTimer);
    }
}
