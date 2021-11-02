using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionTest : MonoBehaviour
{
    public Text positionTest;

    void Start()
    {
        positionTest = GameObject.Find("positionTestText").GetComponent<Text>();
    }

    void Update()
    {
        positionTest.text = $"{transform.position}";        
    }
}
