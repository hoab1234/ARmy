using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DebugUI : MonoBehaviour
{
    public static DebugUI instance;
    
    public Text Imagedebug;
    public Text VideoDebug;
    public Text GmDebug;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    
    public void UpdateDebugForImg(string text)
    {

        Imagedebug.text = text;
    
    }
    public void UpdateDebugForVideo(string text)
    {
        VideoDebug.text = text;
    }
    public void UpdateDebugForGm(string text)
    {
        GmDebug.text = text;
    }

}