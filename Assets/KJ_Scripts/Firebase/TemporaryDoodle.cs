using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
public class TemporaryDoodle : MonoBehaviour
{
    public int maxImgDoodleSize = 256;
    public string path;
    public float distanceFromCamera = 2f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera;
        transform.forward = Camera.main.transform.forward;
    }

    [HideInInspector]
    public Texture texture;

    internal void SetImage(string path)
    {
        this.path = path;
        texture = NativeGallery.LoadImageAtPath(path, maxImgDoodleSize);

        if (texture != null)
        {
            Material material = this.gameObject.GetComponent<MeshRenderer>().materials[0];
            material.mainTexture = texture;
        }

    }
        //NativeGallery.GetVideoProperties(currentVideoPath);
        //NativeGallery.GetVideoFromGallery
      /*  Internal_Storage_FileName = Path.Combine(Application.persistentDataPath, Video_Name + ".mp4");

        VP.url = Internal_Storage_FileName;*/
    VideoPlayer vp;
    internal void SetVideo()
    {
        
        string Path = "storage/emulated/0/Movies/Tee";
        //string Path = Application.persistentDataPath + "/Tee/";
        string[] files = Directory.GetFiles(Path);
        string currentVideoPath = files[files.Length - 1];

        //VideoLoader.instance.NewDoodleUpLoad(RealTimeDataBase.instance.VideoNum,currentVideoPath);
        

        
        vp = GetComponent<VideoPlayer>();
        vp.url = currentVideoPath;

        StartCoroutine(SetUpVideo());
    }


    IEnumerator SetUpVideo()
    {
        vp.Play();
        yield return new WaitForSeconds(3f);
        vp.Pause();
    }
}


/*
public void SelectVideo()
{
    //���� ��θ� �Է�
    //string Path = Application.persistentDataPath + "/Tee";
    string Path = "storage/emulated/0/Movies/Tee";

    //testText[3].text = Path;
    //�ش� ������ �����ϴ��� Ȯ��
    //testText[1].text = "selectFile()";


    string[] files = Directory.GetFiles(Path);

    string currentVideoPath = files[files.Length - 1];
    UIManager.instance.UITempVideoDoodleOn(currentVideoPath);
    // foreach (string item in files)
    // {
    //     testText[0].text = item;
    // }


}*/