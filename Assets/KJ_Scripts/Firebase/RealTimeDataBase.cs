using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System;


public class RealTimeDataBase : MonoBehaviour
{
    public static RealTimeDataBase instance;
    DatabaseReference reference;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }


    }


    public int ImgNum = 0; //  REAL TIME DB�� ����� �̹��� ���� ���� ����
    public int VideoNum = 0; // REALTIME DB�� ����� ���� ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        DebugUI.instance.UpdateDebugForGm("getcontentsNum");
        //google json�� db�ּ� �����Ű� RootReference�� �����Ǵ� ��
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        GetVideoNum();
        GetImageNum();
        DebugUI.instance.UpdateDebugForGm("num call done");
    }


    public void GetImageNum()
    {
        DebugUI.instance.UpdateDebugForImg("getimagenum start");
        FirebaseDatabase.DefaultInstance // realtime db���� �̹��� ���� ���� ������ ������
    .GetReference("ImageNum")
    .GetValueAsync().ContinueWith(task =>
    {
        if (task.IsFaulted)
        {
            // Handle the error...
            print("get imagenum error");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            ImgNum = Convert.ToInt32(snapshot.Value);
            print("imgnum =====" + ImgNum);
            ImageLoader.instance.DownLoadImages(ImgNum);
            DebugUI.instance.UpdateDebugForImg("getimagenum -> downloadCall" + ImgNum.ToString());
            // Do something with snapshot...
        }
    });
    }

    public void GetVideoNum() // realtime db���� ���� ���� ���� ������ ������
    {
        DebugUI.instance.UpdateDebugForVideo("getVideoNum start");
        FirebaseDatabase.DefaultInstance
    .GetReference("VideoNum")
    .GetValueAsync().ContinueWith(task =>
    {
        if (task.IsFaulted)
        {
            // Handle the error...
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            VideoNum = Convert.ToInt32(snapshot.Value);
            print("VideoNum =====" + VideoNum);
            VideoLoader.instance.DownLoadVideo(VideoNum);
            DebugUI.instance.UpdateDebugForVideo("getVideoNum -> downloadvideoCall");
            // Do something with snapshot...
        }
    });
    }
  

  
    
    public void UpdateImageNum()
    {
        ImgNum++;
        print("updateFunc");
        print(ImgNum);
        reference.Child("ImageNum").SetValueAsync(ImgNum);
        UIManager.instance.UiUpLoadOff();
        ImageLoader.instance.AddNewDoodle(ImgNum);
        
    }
    
    internal void UpdateVideoNum()
    {
        VideoNum++;
        print("updateFunc");
        print("videonum ===" + VideoNum);
        reference.Child("VideoNum").SetValueAsync(VideoNum);
        VideoLoader.instance.AddNewDoodle(VideoNum);
    }
}