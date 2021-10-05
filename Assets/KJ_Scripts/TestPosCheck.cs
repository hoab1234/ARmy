using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPosCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DebugUI.instance.LocalPosCheck(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugUI.instance.PosUpdate(this.gameObject);
    }
}
