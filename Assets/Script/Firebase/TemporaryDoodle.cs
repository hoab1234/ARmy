using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryDoodle : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3f;
        transform.forward = Camera.main.transform.forward;
        //transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

   
}
