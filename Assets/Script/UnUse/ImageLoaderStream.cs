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
using System.Threading;

public class ImageLoaderStream : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        // Create a storage reference from our storage service
        StorageReference storageRef =
            storage.GetReferenceFromUrl("gs://airy-907b8.appspot.com/ImageDoodle/newFile1.jpg");

        storageRef.GetStreamAsync(stream => {
            // Do something with the stream here.
            //
            // This code runs on a background thread which reduces the impact
            // to your framerate.
            //
            // If you want to do something on the main thread, you can do that in the
            // progress eventhandler (second argument) or ContinueWith to execute it
            // at task completion.
            
        }, null, CancellationToken.None);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
