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


public class ImageLoaderBytes : MonoBehaviour
{




    

    // Start is called before the first frame update
    void Start()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef =
            storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/ImageDoodle/newFile1.jpg");


        const long maxAllowedSize = 10 * 1024 * 1024;
        storageRef.GetBytesAsync(maxAllowedSize).ContinueWith((Task<byte[]> task) => {
            print("getbtyesasync start");
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
                print("aasdfafaerrorerrorerror");
            }
            else
            {
                byte[] fileContents = task.Result;
                Debug.Log("Finished downloading!");
                print(fileContents[1]);
            }
        });



    }

    Texture2D texture;
    public GameObject doodleFactory;
    
    
    void Update()
    {
        
    }
}
