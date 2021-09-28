using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KKJ_TransCreator : MonoBehaviour
{

    public GameObject[] trans;
    public float SetTime = 2f;
    private bool make = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetActiveTran");    
    }
    int i = 0;
    IEnumerator SetActiveTran()
    {
        while(make)
        {
            trans[i].SetActive(true);
            i++;
            yield return new WaitForSeconds(SetTime);
            if (i >= 5)
                make = false;
        }
    }
}
