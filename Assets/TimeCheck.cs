using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimeCheck : MonoBehaviour
{
    public static TimeCheck inst;

    private void Awake()
    {
        inst = this;
    }


    string time_string;

    public Text timeText;

    public string GetCurrentTime()
    {
        return DateTime.Now.ToString(("yy-MM-dd-HH-mm-ss"));
    }

    
    public bool istrue = false;
    private void Update()
    {
        if (istrue)
        {

            timeText.text = GetCurrentTime();
            istrue = false;
        }
    }


}
