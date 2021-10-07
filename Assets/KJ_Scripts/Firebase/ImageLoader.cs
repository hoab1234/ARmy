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
using Random = UnityEngine.Random;


// ar�̹��� �ν��ؼ� �� �����
//���̾�̽����� �̹��� �����ͼ�
// ���� ���忡�� ���� �����ؼ� texture�ű⿡ ������ �ʹ�.
public class ImageLoader : MonoBehaviour
{
    public static ImageLoader instance;
    public int MaxImgaeDoodle = 30;
   
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


    List<string> ImageDoodleTextList;
    List<StorageReference> doodleImgs;
    // Start is called before the first frame update
    void Start()
    {
       
        storage = FirebaseStorage.DefaultInstance;



        //첫번째 스토리지
        storageReference = storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/");

        //두번째 스토리지
        //storageReference = storage.GetReferenceFromUrl("gs://army2-84bb3.appspot.com");

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
    
    int index=0;


   

    IEnumerator LoadImage(string MediaUrl,string ImageDoodleText)
    {

        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl); //Create a request
        yield return request.SendWebRequest(); //Wait for the request to complete
        //yield return new WaitUntil(() => request.isDone);

        texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        print("loadimage func start");
        GameObject doodle = Instantiate(doodleFactory);
        doodle.GetComponentInChildren<MeshRenderer>().material.mainTexture = texture;


        TextMesh imageText = doodle.GetComponentInChildren<TextMesh>();
        imageText.text = ImageDoodleText;
        DebugUI.instance.UpdateDebugForImg("loadimage execute");
        
        index = doodle.GetComponent<Doodle>().getPosIndex();
       
        
        
            print("Check1");
            doodle.transform.parent = trans[index];
            doodle.transform.localPosition = Vector3.zero;
            doodle.GetComponent<Doodle>().setParent(trans[index].gameObject);
            doodle.GetComponent<Doodle>().setDir();
           
           
           
            

       
       
        
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
                        //DebugUI.instance.UpdateDebugForImg("downloadimage loadimage func call");
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