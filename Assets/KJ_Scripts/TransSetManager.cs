using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransSetManager : MonoBehaviour
{
    public static TransSetManager inst;

    [HideInInspector]
    public bool[] ImgtransCheck;
    [HideInInspector]
    public bool[] VideotransCheck;

    public int maxImgNum = 30;
    public int maxVideoNum= 20;

    //int maxImgNum = ImageLoader.instance.MaxImgaeDoodle;
    //int maxVideoNum = VideoLoader.instance.MaxVideoDoodle;

    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
        }

        ImgtransCheck = new bool[30];
        VideotransCheck = new bool[20];


        for (int i = 0; i < maxImgNum; i++)
        {
            ImgtransCheck[i] = false;
        }
        for (int i = 0; i < maxVideoNum; i++)
        {
            VideotransCheck[i] = false;
        }
    }

   



}
