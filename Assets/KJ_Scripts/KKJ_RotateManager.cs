using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KKJ_RotateManager : MonoBehaviour
{
    public float rotSpeed;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);  
    }
}
