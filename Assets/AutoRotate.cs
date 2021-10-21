using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotSpeed = 30;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(transform.up, rotSpeed * Time.deltaTime, Space.World);
        transform.position += Vector3.up * 0.1f * Time.deltaTime;
    }
}
