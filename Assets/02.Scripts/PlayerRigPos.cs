using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigPos : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);        
    }
}
