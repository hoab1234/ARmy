using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadMove : MonoBehaviour
{
    bool floatUp;
    Vector3 startPos;

    void Start()
    {
        int randomNum = Random.Range(0, 2);
        if (randomNum == 0)
        {
            floatUp = false;
        }
        else
        {
            floatUp = true;
        }
        startPos = transform.position;
    }

    void Update()
    {
        if (transform.position.x > 3) transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        if (floatUp) StartCoroutine("Floatingup");
        else if (!floatUp) StartCoroutine("Floatingdown");
    }

    IEnumerator Floatingup()
    {
        if (Mathf.Abs(transform.position.y - startPos.y) <= 1)
        {
            transform.position += new Vector3(0.2f, 0.1f, 0) * Time.deltaTime;
        }
        yield return new WaitForSeconds(1);
        floatUp = false;
    }

    IEnumerator Floatingdown()
    {
        if (Mathf.Abs(transform.position.y - startPos.y) <= 1)
        {
            transform.position -= new Vector3(-0.2f, 0.1f, 0) * Time.deltaTime;
        }
        yield return new WaitForSeconds(1);
        floatUp = true;
    }
}
