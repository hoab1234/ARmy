using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Threading.Tasks;


// ar�̹��� �ν��ؼ� �� �����
//���̾�̽����� �̹��� �����ͼ�
// ���� ���忡�� ���� �����ؼ� texture�ű⿡ ������ �ʹ�.
public class ImageLoader : MonoBehaviour
{
    public static ImageLoader instance;
    public int MaxImgaeDoodle = 50;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    FirebaseStorage storage;
    StorageReference storageReference;

    Texture2D texture;
    public GameObject doodleFactory;

    int doodlehigh = 5;

    List<string> ImageDoodleTextList;
    List<StorageReference> doodleImgs;
    // Start is called before the first frame update
    void Start()
    {
        isUseTrans = new bool[trans.Length];
        for(int i = 0; i < isUseTrans.Length; i++)
        {
            isUseTrans[i] = false;
        }
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/");
        doodleImgs = new List<StorageReference>();

    }

    string ImageDoodleText; //��Ÿ������ ���� ����Ʈ
    public void AddNewDoodle(int doodleNum)
    {
        print("doodlenum===" + doodleNum);
        print("doodleImgs.Count===" + doodleImgs.Count);
        DebugUI.instance.UpdateDebugForGm("addnewdoodle");
        StorageReference sr = storageReference.Child("/ImageDoodle/newFile"
               + (doodleNum - 1).ToString() + ".jpg");
        // ���ο� doodle ����
        doodleImgs.Add(sr);

        sr.GetMetadataAsync().ContinueWith((Task<StorageMetadata> task) =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
                StorageMetadata meta = task.Result;
                ImageDoodleText=meta.GetCustomMetadata("ImageDoodleText");
                // do stuff with meta
            }//�̰� ������ �Ʒ��Լ� �����ϰ� �ؾ� �ɰŰ�����. �ڷ�ƾ?
        });
        //���ο� doodle ǥ��
        sr.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
          {
              if (!task.IsFaulted && !task.IsCanceled)
              {
                  StartCoroutine(LoadImage(Convert.ToString(task.Result),ImageDoodleText)); //Fetch file from the link
              }
              else
              {
                  Debug.Log(task.Exception);
              }
          });
    }
    public Transform[] trans;
    bool[] isUseTrans;
    int index=0;
    IEnumerator LoadImage(string MediaUrl,string ImageDoodleText)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); //Create a request
        yield return request.SendWebRequest(); //Wait for the request to complete

        texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        print("loadimage func start");
        GameObject doodle = Instantiate(doodleFactory);
        doodle.GetComponentInChildren<MeshRenderer>().material.mainTexture = texture;
       
        if(isUseTrans[index] == false)
        {

            doodle.transform.position = trans[index].transform.position;
            doodle.transform.parent = trans[index];
            isUseTrans[index] = true;
            index++;
        }
       
        
        TextMesh imageText = doodle.GetComponentInChildren<TextMesh>();
        imageText.text = ImageDoodleText;
        DebugUI.instance.UpdateDebugForImg("loadimage execute");
    }
    string ImageDoodleTextDown;
    public void DownLoadImages(int imgNum)
    {
        if (imgNum < MaxImgaeDoodle)
        {
            for (int i = 0; i < imgNum; i++)
            {
                doodleImgs.Add(storageReference.Child("/ImageDoodle/newFile"
                     + i.ToString() + ".jpg"));
            }
            for(int i = 0 ; i < imgNum ; i++)
            {
                 doodleImgs[i].GetMetadataAsync().ContinueWith((Task<StorageMetadata> task) =>
                    {
                       if (!task.IsFaulted && !task.IsCanceled)
                       {
                           StorageMetadata meta = task.Result;
                            ImageDoodleTextDown = meta.GetCustomMetadata("ImageDoodleText");
                           
                       }
                    });
            }
            for (int i = 0; i < imgNum; i++)
            {
                if (doodleImgs[i] == null) break;

                doodleImgs[i].GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
                {


                    if (!task.IsFaulted && !task.IsCanceled)
                    {
                        
                        DebugUI.instance.UpdateDebugForImg(ImageDoodleText);
                        StartCoroutine(LoadImage(Convert.ToString(task.Result),ImageDoodleTextDown)); //Fetch file from the link
                        DebugUI.instance.UpdateDebugForImg("downloadimage loadimage func call");
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
            for (int i = imgNum - MaxImgaeDoodle; i < imgNum; i++)
            {
                doodleImgs.Add(storageReference.Child("/ImageDoodle/newFile"
                     + i.ToString() + ".jpg"));
            }

            for (int i = 0; i < imgNum; i++)
            {
                if (doodleImgs[i] == null) break;
                doodleImgs[i].GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
                {
                    if (!task.IsFaulted && !task.IsCanceled)
                    {
                        StartCoroutine(LoadImage(Convert.ToString(task.Result),ImageDoodleTextDown)); //Fetch file from the link
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

                    //doodleImgs[i].GetMetadataAsync().ContinueWith((Task<StorageMetadata> task) =>
                    //{
                    //    if (!task.IsFaulted && !task.IsCanceled)
                    //    {
                    //        StorageMetadata meta = task.Result;
                    //        ImageDoodleText = meta.GetCustomMetadata("ImageDoodleText");
                    //        // do stuff with meta
                    //    }
                    //});