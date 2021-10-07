using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosCheck : MonoBehaviour
{
    

    // Start is called before the first frame update
    public void Start()
    {
       

        StartCoroutine(ARPOSCHECK());    
    }

    IEnumerator ARPOSCHECK()
    {
        while (true)
        {
            Vector3 temp;
            yield return new WaitForSeconds(1f);
            temp = transform.position;
            if (0.5f <= temp.x || temp.x <= -0.5f ||
                0.5f <= temp.y || temp.y <= -0.5f ||
                0.5f <= temp.z || temp.z <= -0.5f)
            {
                transform.position = Vector3.zero;
            }
            
        }
    }
}
