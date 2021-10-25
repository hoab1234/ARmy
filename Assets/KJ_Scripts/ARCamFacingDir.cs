using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ARCamFacingDir : MonoBehaviour
{

    public static ARCamFacingDir inst;

    void Awake(){
        if(inst == null)
        {
            inst = this;
        }
    }

    [SerializeField]
    public ARCameraManager m_CameraManager;

    [SerializeField]
    GameObject m_ARCoreExtensions;

    public ARCameraManager cameraManager
    {
        get => m_CameraManager;
        set => m_CameraManager = value;
    }

    [SerializeField]
    ARSession m_Session;

    public ARSession session
    {
        get => m_Session;
        set => m_Session = value;
    }
    ARPlaneManager m_planeManager;
    ARPointCloudManager m_ARPointCloudManager;
    ARAnchorManager m_ARAnchorManager;
    ARRaycastManager m_ARRaycastManager;

    ARFaceManager m_ARFaceManager;

    ARFaceThreePoints m_ARFaceThreePoints;

    Renderer[] renderers;

    public void SetRenderers(Renderer[] _renderers){
        renderers = _renderers;
    }
    void Start()
    {

        m_CameraManager.requestedFacingDirection = CameraFacingDirection.World;


        m_planeManager = GetComponent<ARPlaneManager>();

        m_ARPointCloudManager = GetComponent<ARPointCloudManager>();

        m_ARAnchorManager = GetComponent<ARAnchorManager>();

        m_ARRaycastManager = GetComponent<ARRaycastManager>();

        m_ARFaceManager = GetComponent<ARFaceManager>();

        m_ARFaceThreePoints = GetComponent<ARFaceThreePoints>();

        
    }

    bool isFacingDirWorld = true;
    public void FacingDirChange()
    {
        if (isFacingDirWorld)
        {
            ToUser();
            isFacingDirWorld = false;
            ARFaceThreePoints.inst.SetNoneFaceFilter();
        }
        else
        {
            ToWorld();
            isFacingDirWorld = true;
            ARFaceThreePoints.inst.SetNoneFaceFilter();
        }
    }
    private void ToWorld()
    {
        ShowPrefab();
        print("facing direction ==== world");
        m_CameraManager.requestedFacingDirection = CameraFacingDirection.World;
        ARAnchorOnOff(true);
        ARFaceOnOff(false);

    }
    private void ToUser()
    {
        HidePrefab();
        print("facing direction ====  user");
        m_CameraManager.requestedFacingDirection = CameraFacingDirection.User;
        ARAnchorOnOff(false);
        ARFaceOnOff(true);
    }

    GameObject prefab;
    public void HidePrefab()
    {
        prefab = GameObject.FindGameObjectWithTag("ARMYROAD");
        if (prefab == null) return;
        print(prefab.name);
        
        foreach(var _renderer in renderers)
        {
            _renderer.enabled = false;
        }
        //prefab.SetActive(false);
    }
    public void ShowPrefab()
    {
        if (prefab == null) return;
         //prefab.SetActive(true);
        foreach(var _renderer in renderers)
        {
            _renderer.enabled = true;
        }

        prefab = GameObject.FindGameObjectWithTag("ARMYROAD");
        print(prefab.name);
    }
    public void ARAnchorOnOff(bool BOOL)
    {
        if (BOOL)
        {
            m_planeManager.enabled = true;
            m_ARPointCloudManager.enabled = true;
            m_ARAnchorManager.enabled = true;
            m_ARRaycastManager.enabled = true;
            m_ARCoreExtensions.SetActive(true);
        }
        else
        {
            m_planeManager.enabled = false;
            m_ARPointCloudManager.enabled = false;
            m_ARAnchorManager.enabled = false;
            m_ARRaycastManager.enabled = false;
            m_ARCoreExtensions.SetActive(false);
        }
    }

    public void ARFaceOnOff(bool BOOL)
    {
        if (BOOL)
        {
            m_ARFaceManager.enabled = true;
            m_ARFaceThreePoints.enabled = true;
        }
        else
        {
            m_ARFaceManager.enabled = false;
            m_ARFaceThreePoints.enabled = false;

        }
    }

    public void CheckFacingDir()
    {
        if (m_CameraManager.requestedFacingDirection == CameraFacingDirection.User)
        {
            ToWorld();
            isFacingDirWorld = true;
        }
    }
    public void UploadWhenFacingUser()
    {
        if (m_CameraManager.requestedFacingDirection == CameraFacingDirection.User)
        {
            
            StartCoroutine(FacingDirChangeDelay());
        }
    }
    IEnumerator FacingDirChangeDelay(){
        yield return new WaitForSeconds(0.5f);
        FacingDirChange();
    }
}

