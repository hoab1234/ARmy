using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
public class KKJ_TransCreator : MonoBehaviour
{

    public GameObject[] trans;
    public float SetTime = 2f;
    private bool make = true;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SetActiveTran");   
        
        for(int i = 0; i < trans.Length; i++)
        {
            //trans[i].GetComponent<PathFollower>().enabled = false;
        }
    }
    IEnumerator SetActiveTran()
    {
        int i = 0;
        while(make)
        {
            trans[i].SetActive(true);
            //trans[i].GetComponent<PathFollower>().enabled = true;
            i++;
            yield return new WaitForSeconds(SetTime);
            if (i >= trans.Length)
                make = false;
        }
    }
}
