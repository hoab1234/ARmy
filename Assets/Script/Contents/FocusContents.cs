using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusContents : MonoBehaviour
{
    VideoToogleBtn videoToggleBtn;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        videoToggleBtn = GetComponent<VideoToogleBtn>();
        pos = Camera.main.transform.position + Camera.main.transform.forward * 3f;
       
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            print("frie2-------");
            transform.position = Vector3.Lerp(transform.position, pos, 5);
            if(videoToggleBtn != null)
            {
                videoToggleBtn.UpdateFocusState(true);
            }
        }
    }
}
