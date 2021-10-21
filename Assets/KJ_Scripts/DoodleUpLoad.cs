using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using System.Threading.Tasks;
using DG.Tweening;
public class DoodleUpLoad : MonoBehaviour
{

    public GameObject TempDoodleImg;
    public GameObject imageDoodle;

    FirebaseStorage storage;
    StorageReference storageReference;

    // Start is called before the first frame update
    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/");
    }

    public void UpLoadDoodle()
    {
        var inputField = TempDoodleImg.GetComponentInChildren<InputField>();
        var inputText = inputField.text;
        var new_meatadata = new MetadataChange
        {
            CustomMetadata = new Dictionary<string, string>
                {
                    { "ImageDoodleText", inputText },
                }
        };

        string local_file;
        local_file = TempDoodleImg.GetComponent<TemporaryDoodle>().path;
        if(local_file == null)
        {
        }
        else
        {
        }
        StorageReference uploadRef = storageReference.Child("ImageDoodle/newFile" + (RealTimeDataBase.instance.ImgNum).ToString() + ".jpg");

        uploadRef.PutFileAsync("file://" + local_file, new_meatadata) 
              .ContinueWith((Task<StorageMetadata> task) =>
              {
                  if (task.IsFaulted || task.IsCanceled)
                  {
                  }
                  else
                  {
                      
                      StorageMetadata metadata = task.Result;
                     
                      RealTimeDataBase.instance.UpdateImageNum();
                  }
              });
        StartCoroutine(TempUIoff());
        Texture texture = TempDoodleImg.GetComponent<TemporaryDoodle>().texture;
        SetPosUpLoadDoodle(texture, inputText);
    }

    IEnumerator TempUIoff()
    {
        yield return new WaitForSeconds(0.01f);
        TempDoodleImg.SetActive(false);
        UIManager.instance.UiUpLoadOff();
    }

    public void SetPosUpLoadDoodle(Texture texture , string str)
    {
        GameObject Doodle = Instantiate(imageDoodle);
        Doodle.GetComponentInChildren<MeshRenderer>().material.mainTexture = texture;
        TextMesh imageText = Doodle.GetComponentInChildren<TextMesh>();
        imageText.text = str;
        Doodle.transform.position = TempDoodleImg.transform.position;
        Doodle.transform.rotation = TempDoodleImg.transform.rotation;

        int index = Doodle.GetComponent<Doodle>().getPosIndex();
        Doodle.transform.parent = ImageLoader.instance.trans[index];
        Doodle.GetComponent<Doodle>().setParent(ImageLoader.instance.trans[index].gameObject);
        Doodle.GetComponent<Doodle>().GoBack();

        UIManager.instance.UICameraIconOn();
        //Doodle.GetComponent<Doodle>().setDir();
    } 
   
}
