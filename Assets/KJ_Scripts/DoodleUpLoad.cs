using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using System.Threading.Tasks;

public class DoodleUpLoad : MonoBehaviour
{

    public GameObject TempDoodleImg;


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
            DebugUI.instance.UpdateDebugForGm("PATH IS NULL");
        }
        else
        {
            DebugUI.instance.UpdateDebugForGm("path =" + local_file);
        }
        StorageReference uploadRef = storageReference.Child("ImageDoodle/newFile" + (RealTimeDataBase.instance.ImgNum).ToString() + ".jpg");

        uploadRef.PutFileAsync("file://" + local_file, new_meatadata) 
              .ContinueWith((Task<StorageMetadata> task) =>
              {
                  DebugUI.instance.UpdateDebugForGm("putfileAsync");
                  if (task.IsFaulted || task.IsCanceled)
                  {
                      DebugUI.instance.UpdateDebugForGm((local_file).ToString());
                  }
                  else
                  {
                      
                      StorageMetadata metadata = task.Result;
                     
                      RealTimeDataBase.instance.UpdateImageNum();
                  }
              });
        StartCoroutine(TempUIoff());
    }

    IEnumerator TempUIoff()
    {
        yield return new WaitForSeconds(0.1f);
        TempDoodleImg.SetActive(false);
    }
   
}
