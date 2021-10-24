using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecordLimit : MonoBehaviour
{
    public Slider RecordLimitBar;

    public float maxRecordTime = 10f;
    public float minusTime = 0f;

    private void Awake()
    {
        RecordLimitBar = GetComponentInChildren<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        RecordLimitBar.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(RecordLimitBar.value >= 0.0f)
        {
            RecordLimitBar.value += Time.deltaTime;
        }
        if (RecordLimitBar.value >= 10)
        {
            UIController.inst.OnClickStopRecord();
            GameObject.Find("record").SetActive(false);
        }
    }
    private void OnDisable()
    {
        RecordLimitBar.value = 0f;
    }
}
