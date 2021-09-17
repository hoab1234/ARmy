using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class VideoToogleBtn : MonoBehaviour
{
    VideoPlayer vp;
    public GameObject PlayBtn;
    public GameObject PauseBtn;
    bool videoFocus = false;
    bool isFocus = false;
    public enum State
    {
        None,
        Play,
        Pause
    }
    public State vpState;
    public void UpdateFocusState(bool state)
    {
        isFocus = state;
    }
    void Start()
    {
        vp = GetComponentInChildren<VideoPlayer>();
        vp.Pause();
        vpState = State.None;
        PlayBtn.SetActive(true);
        PauseBtn.SetActive(false);
        //isFocus = false;
        //StartCoroutine("BtnToggle");
    }
    void Update()
    {
       
    }
    public void SetState(bool state)
    {
        if (state)
        {
            vpState = State.Play;
        }
        else
        {
            vpState = State.Pause;
        }
    }
    private void checkOver()
    {
        long playerCurrentFrame = vp.frame;
        long playerFrameCount = Convert.ToInt64(vp.frameCount);

        if (playerCurrentFrame < playerFrameCount-1)
        {
            //print("VIDEO IS PLAYING");
            //print("curreent freme----" + playerCurrentFrame);
            //print("count freme----" + playerFrameCount);
        }
        else
        {
            print("VIDEO IS OVER");
            //Do w.e you want to do for when the video is done playing.
            PlayBtn.SetActive(true);
            DoodleTouchEvent.instance.isPlaying = false;
            //Cancel Invoke since video is no longer playing
            CancelInvoke("checkOver");
        }
    }
    public void Play()
    {
        
            vp.Play();
            vpState = State.Play;
            PlayBtn.SetActive(false);
            PauseBtn.SetActive(false);
            InvokeRepeating("checkOver", .1f, .1f);

    }
    public void Pause()
    {
        
            vp.Pause();
            vpState = State.Pause;
            PlayBtn.SetActive(false);
            PauseBtn.SetActive(true);
        
    }

}

/*
    IEnumerator BtnToggle()
    {

        while (true)
        {

            yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
            if (isFocus)
            {
                if (vpState == State.None || vpState == State.Pause)
                {
                    vp.Play();
                    vpState = State.Play;
                    PlayBtn.SetActive(false);
                    PauseBtn.SetActive(false);
                }
                else if (vpState == State.Play)
                {
                    vp.Pause();
                    vpState = State.Pause;
                    PlayBtn.SetActive(false);
                    PauseBtn.SetActive(true);
                }
            }
            //else if(vpState == Pause)
            yield return new WaitForSeconds(0.2f);

        }

    }*/