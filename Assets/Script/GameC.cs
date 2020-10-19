using System;
using System.Collections;
using System.Windows;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class GameC : MonoBehaviour
{
    private bool _checkWin;
    
    private WaitScreen _waitScreen;
    private String _WSObj = "WaitScreen";
        
    private WaitScreen _waitScreenMain;
    private String _WSMainObj = "WaitScreen_Main";

    private Move _move;
    private String _MoveObj = "puzzle";
    
    private CountDown _countDown;
    private String _countDownObj = "Timer";

    public bool gaming;

    public bool allowRedo = true;
    public bool allowGiveUp = false;

    public Image helpBg;

    internal Button winButton;
    internal String winBTname = "winb";
    internal Text winText;
    internal String winTextName = "WIN";
    internal Text helpText;
    internal String helpTextName = "helpText";

    private bool bottomLock;

    private bool showHelp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        helpBg.enabled = false;
        FindObjectOfType<AudioManager>().Play("dawn");
        _waitScreen = GameObject.Find(_WSObj).GetComponent<WaitScreen>();
        _waitScreenMain = GameObject.Find(_WSMainObj).GetComponent<WaitScreen>();
        _move = GameObject.Find(_MoveObj).GetComponent<Move>();
        _countDown = GameObject.Find(_countDownObj).GetComponent<CountDown>();
        winButton = GameObject.Find(winBTname).GetComponent<Button>();
        winText = GameObject.Find(winTextName).GetComponent<Text>();
        winButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        helpText = GameObject.Find(helpTextName).GetComponent<Text>();
        helpText.gameObject.SetActive(false);
    }
    
    private void FadeIn()
    {
        _waitScreen.FadeIn();
        _waitScreenMain.FadeIn();
    }

    private void FadeOut()
    {
        _waitScreen.FadeOut();
        _waitScreenMain.FadeOut();
    }

    public void PressButtom()
    {
        FindObjectOfType<AudioManager>().Play("ding");
    }

    public void ResetCube()
    {
        if (!bottomLock)
        {
            if (!gaming)
            {
                _move.ReStart();
            }
        }
    }

    public void Redo()
    {
        if (!bottomLock)
        {
            if (allowRedo)
            {
                _move.Redo();
            }
        }
    }

    public void Random()
    {
        if (!bottomLock)
        {
            if (!gaming)
            {
                _move.RandomTheCube();
            }
        }
    }

    IEnumerator GameStart()
    {
        FindObjectOfType<AudioManager>().Stop();
        gaming = true;
        allowRedo = false;
        _move.MoveLock = true;
        FadeIn();
        FindObjectOfType<AudioManager>().Play("game");
        yield return new WaitForSeconds(1);
        _move.RandomTheCube();
        _countDown.InfoPreparing();
        yield return new WaitForSeconds(7.5f);
        FadeOut();
        yield return new WaitForSeconds(1f);
        _countDown.StartCountDown(10f);
        _countDown.InfoReady();
        yield return new WaitForSeconds(10);
        _move.MoveLock = false;
        allowRedo = true;
        _move.ClearSteps();
        _countDown.InfoGo();
        _checkWin = true;
        allowGiveUp = true;
        _countDown.StartTimer();
    }

    public void GiveUp()
    {
        if (!bottomLock)
        {
            if (gaming && allowGiveUp)
            {
                gaming = false;
                _checkWin = false;
                _countDown.InfoGaveUp();
                _countDown.StopTimer();
                allowGiveUp = false;
                FindObjectOfType<AudioManager>().Stop();
                FindObjectOfType<AudioManager>().Play("dawn");
            }
        }
    }

    public void StartGame()
    {
        if (!bottomLock)
        {
            StartCoroutine(GameStart());
        }
    }

    public void Iknow()
    {
        winButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
        FindObjectOfType<AudioManager>().Stop();
        FindObjectOfType<AudioManager>().Play("dawn");
        bottomLock = false;
    }

    public void Help()
    {
        if (showHelp)
        {
            PressButtom();
            helpText.gameObject.SetActive(false);
            helpBg.enabled = false;
            showHelp = false;
        }
        else
        {
            PressButtom();
            showHelp = true;
            helpBg.enabled = true;
            helpText.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_checkWin)
        {
            if (_move._logic.CheckWin())
            {
                _countDown.InfoFinish();
                _countDown.StopTimer();
                _checkWin = false;
                gaming = false;
                FindObjectOfType<AudioManager>().Stop();
                FindObjectOfType<AudioManager>().Play("dawn");
                winButton.gameObject.SetActive(true);
                winText.gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Stop();
                FindObjectOfType<AudioManager>().Play("win");
                bottomLock = true;
            }
        }
    }
}
