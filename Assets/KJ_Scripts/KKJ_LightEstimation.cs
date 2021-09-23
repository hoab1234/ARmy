using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class KKJ_LightEstimation : MonoBehaviour
{
    public static KKJ_LightEstimation instance;

    public bool darkMode = false;

    [SerializeField]
    private ARCameraManager arCameraManager;

    [SerializeField]
    private Text brightnessValue;

    private Light currentLight;

    private void Awake()
    {
        currentLight = GetComponent<Light>();
    }
    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdated;
    }
    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if(args.lightEstimation.averageBrightness.HasValue)
        {
            brightnessValue.text = $"Brightness : {args.lightEstimation.averageBrightness.Value}";
            if(args.lightEstimation.averageBrightness < 0.3f)
            {
                darkMode = true;
            }
        }
    }

}
