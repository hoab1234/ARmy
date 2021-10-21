using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraDirectionManager : MonoBehaviour
{
    public ARCameraManager arcm;

    public void ChangeCameraDirection()
    {
        arcm.requestedFacingDirection = CameraFacingDirection.World;
    }
}
