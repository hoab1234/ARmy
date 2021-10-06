using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigPos : MonoBehaviour
{
    public float rigY = 1.4f;
    public Transform zoomCamera;

    void Start()
    {

    }

    void Update()
    {
        rigY = 1.4f * (10 - GetPercentage(-180, 3200, zoomCamera.transform.position.z, 1, 10));
        transform.position = new Vector3(transform.position.x, rigY, transform.position.z);
        transform.localScale = Vector3.one * 100 * (10 - GetPercentage(-180, 3200, zoomCamera.transform.localPosition.z, 1, 10));
    }

    private float GetPercentage(float Range1Min, float Range1Max, float Range1SelectedValue, float Range2Min, float Range2Max)
    {
        float range1Percent = (Range1SelectedValue - Range1Min) / (Range1Max - Range1Min) * 100;
        float range2NewValue = (Range2Max - Range2Min) * range1Percent / 100 + Range2Min;

        return range2NewValue;
    }
}
