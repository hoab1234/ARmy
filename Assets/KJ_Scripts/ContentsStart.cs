using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsStart : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.GuildCanvasGroupOff();
        UIManager.instance.UICameraIconOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
