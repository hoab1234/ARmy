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


    public int ImgNum = 0; //  REAL TIME DB에 저장된 이미지 갯수 담을 변수
    public int VideoNum = 0; // REALTIME DB에 저장된 비디오 갯수 담을 변수

    // Start is called before the first frame update
    void Start()
    {
        DebugUI.instance.UpdateDebugForGm("getcontentsNum");
        //google json에 db주소 넣은거가 RootReference로 참조되는 듯
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        GetVideoNum();
        GetImageNum();
        DebugUI.instance.UpdateDebugForGm("num call done");
    }


    public void GetImageNum()
    {
        DebugUI.instance.UpdateDebugForImg("getimagenum start");
        FirebaseDatabase.DefaultInstance // realtime db에서 이미지 저장 갯수 데이터 가져옴
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

    public void GetVideoNum() // realtime db에서 비디오 저장 갯수 데이터 가져옴
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