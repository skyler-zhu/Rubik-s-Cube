using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private float _currenttime;

    private float _starttime = 10f;

    private float _timerTime = 0f;

    private bool _begin;

    private bool _timer;

    public Text countdown;

    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        _currenttime = _starttime;
        _begin = false;
        info.text = "Welcome!";
    }

    internal void StartTimer()
    {
        _timerTime = 0f;
        _timer = true;
    }

    internal void StartCountDown(float sec)
    {
        _starttime = sec;
        _currenttime = _starttime;
        _begin = true;
    }

    internal void InfoReady()
    {
        info.text = "Ready?";
    }
    
    internal void InfoGo()
    {
        info.text = "Go!";
    }
    
    internal void InfoFinish()
    {
        info.text = "Finished!";
    }
    
    internal void InfoPreparing()
    {
        info.text = "Preparing Cube~";
    }
    
    internal void InfoGaveUp()
    {
        info.text = "Good Luck next time~";
    }
    

    internal void StopTimer()
    {
        _timer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_begin)
        {
            _currenttime -= 1 * Time.deltaTime;
            countdown.text = _currenttime.ToString();
            if (_currenttime <= 0)
            {
                _begin = false;
            }
        }

        if (_timer)
        {
            _timerTime += 1 * Time.deltaTime;
            countdown.text = _timerTime.ToString();
        }
    }
}
