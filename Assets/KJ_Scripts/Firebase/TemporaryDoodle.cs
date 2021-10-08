using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryDoodle : MonoBehaviour
{
    public int maxImgDoodleSize = 256;
    public string path;
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1f;
        transform.forward = Camera.main.transform.forward;
    }
    
    [HideInInspector]
    public Texture texture;

    internal void SetContent(string path)
    {
        this.path = path;
        texture = NativeGallery.LoadImageAtPath(path, maxImgDoodleSize);
        if(texture != null)
        {
            Material material = this.gameObject.GetComponent<MeshRenderer>().materials[0];
            material.mainTexture = texture;
        }
            
    }
}
