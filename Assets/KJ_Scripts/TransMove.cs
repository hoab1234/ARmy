using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    public Transform targetTrans;
    public bool UP = false;
    int dir;

    public bool isRightdir = false;

    public bool getDir()
    {
        return isRightdir;
    }

    // Start is called before the first frame updat
    void Start()
    {
        if(UP)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((dir*transform.up) * moveSpeed * Time.deltaTime);
        if(UP)
        {
            if (transform.position.y >= targetTrans.position.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y * -1f, transform.position.z);
            }
        }
        else
        {
            if (transform.position.y <= targetTrans.position.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y * -1f, transform.position.z);
            }
        }
    }
}
