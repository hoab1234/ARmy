using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using UnityEngine.Video;
using System.Threading.Tasks;

// ar이미지 인식해서 모델 생기면
//파이어베이스에서 이미지 가져와서
// 쿼드 공장에서 쿼드 생성해서 texture거기에 입히고 싶다.
public class VideoLoader : MonoBehaviour
{
    public static VideoLoader instance;
    public int MaxVideoDoodle = 18;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    FirebaseStorage storage;
    StorageReference storageReference;


    public GameObject doodleFactory;

    int doodlehigh = 3; // 두들 임시 위치


    List<StorageReference> doodleVideos;
    // Start is called before the first frame update
    void Start()
    {
        isUseTrans = new bool[trans.Length];
        for (int i = 0; i < isUseTrans.Length; i++)
        {
            isUseTrans[i] = false;
        }

        storage = FirebaseStorage.DefaultInstance;
        
        //첫번째 스토리지
        //storageReference = storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/");

        //두번째 스토리지
        storageReference = storage.GetReferenceFromUrl("gs://army2-84bb3.appspot.com");

        doodleVideos = new List<StorageReference>();

        //StorageReference SampleVideo = storageReference.Child("/VideoDoodle/SampleVideo.mp4");

        //SampleVideo.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        //{
        //    if (!task.IsFaulted && !task.IsCanceled)
        //    {
        //        StartCoroutine(LoadVideo(Convert.ToString(task.Result))); //Fetch file from the link
        //    }
        //    else
        //    {
        //        Debug.Log(task.Exception);
        //    }
        //});
    }
    string VideoDoodleText;
    public void AddNewDoodle(int doodleNum)
    {

        // 새로운 doodle 참조
       
        StorageReference sr = storageReference.Child("/VideoDoodle/newFile"
               + (doodleNum - 1).ToString() + ".mp4");
        // 새로운 doodle 참조
        doodleVideos.Add(sr);

        sr.GetMetadataAsync().ContinueWith((Task<StorageMetadata> task) =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StorageMetadata meta = task.Result;
                VideoDoodleText = meta.GetCustomMetadata("VideoDoodleText");
                // do stuff with meta
            }//�̰� ������ �Ʒ��Լ� �����ϰ� �ؾ� �ɰŰ�����. �ڷ�ƾ?
        });
        //새로운 doodle 표시
        sr.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {

            if (!task.IsFaulted && !task.IsCanceled)
            {
                StartCoroutine(LoadVideo(Convert.ToString(task.Result),VideoDoodleText)); //Fetch file from the link
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });
        
    }


    public Transform[] trans;
    bool[] isUseTrans;
    int index = 0;
    string videoDoodleText;

    IEnumerator LoadVideo(string MediaUrl,string VideoDoodleTextDown)
    {
        UnityWebRequest request = UnityWebRequest.Get(MediaUrl);

        yield return request.SendWebRequest(); //Wait for the request to complete

       
        VideoPlayer vp;
        GameObject doodle = Instantiate(doodleFactory);
        vp = doodle.GetComponentInChildren<VideoPlayer>();
        vp.url = request.url;

        if (isUseTrans[index] == false)
        {
            doodle.transform.position = trans[index].transform.position;
            doodle.transform.parent = trans[index];
            doodle.GetComponent<Doodle>().setParent(trans[index].gameObject);
            isUseTrans[index] = true;
            index++;
        }

        DebugUI.instance.UpdateDebugForVideo("load video execute");

        TextMesh VideoText = doodle.GetComponentInChildren<TextMesh>();
        VideoText.text = VideoDoodleTextDown;
        DebugUI.instance.UpdateDebugForImg("Videoimage execute");
    }


    string videoDoodleTextDown;
    
    public void DownLoadVideo(int VideoNum)
    {
        if (VideoNum < MaxVideoDoodle)
        {
            for (int i = 0; i < VideoNum; i++)
            {
                doodleVideos.Add(storageReference.Child("/VideoDoodle/newFile"
                     + i.ToString() + ".mp4"));
            }
            for (int i = 0; i < VideoNum; i++)
            {
                doodleVideos[i].GetMetadataAsync().ContinueWith((Task<StorageMetadata> task) =>
                {
                    if (!task.IsFaulted && !task.IsCanceled)
                    {
                        StorageMetadata meta = task.Result;
                        videoDoodleTextDown = meta.GetCustomMetadata("VideoDoodleText");

                    }
                });
            }

            for (int i = 0; i < VideoNum; i++)
            {
                if (doodleVideos[i] == null) { break; }
                doodleVideos[i].GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
                {
                    if (!task.IsFaulted && !task.IsCanceled)
                    {
                        StartCoroutine(LoadVideo(Convert.ToString(task.Result),videoDoodleTextDown)); //Fetch file from the link
                        DebugUI.instance.UpdateDebugForVideo("video DOWNLOAD -> load Call");
                    }
                    else
                    {
                        Debug.Log(task.Exception);
                    }
                });
            }
        }
        else
        {
            for (int i = VideoNum - MaxVideoDoodle; i < VideoNum; i++)
            {
                doodleVideos.Add(storageReference.Child("/VideoDoodle/newFile"
                     + i.ToString() + ".mp4"));
            }

            for (int i = 0; i < VideoNum; i++)
            {
                if (doodleVideos[i] == null) break;
                doodleVideos[i].GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
                {
                    if (!task.IsFaulted && !task.IsCanceled)
                    {
                        StartCoroutine(LoadVideo(Convert.ToString(task.Result), videoDoodleTextDown)); //Fetch file from the link
                    }
                    else
                    {
                        Debug.Log(task.Exception);
                    }
                });
            }
        }
    }
}
