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
    public float localLeftXpos = 3f;
    public float localRightXpos = 1.3f;
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
        StartCoroutine(SetOriginPos());
    }

    IEnumerator SetOriginPos()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            if(!isRightdir)
            {

                if( transform.localPosition.x >= 2.2 || transform.localPosition.z != 0) 
                {
                    transform.localPosition = new Vector3(localLeftXpos, transform.localPosition.y, 0f);
                }
            }
            else if(isRightdir)
            {
                if (transform.localPosition.x >= 2.2 || transform.localPosition.z != 0)
                {
                    transform.localPosition = new Vector3(localRightXpos, transform.localPosition.y, 0f);
                }
            }
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
