using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;

public class MyARRayCast : MonoBehaviour
{
    public Transform indicator;
    public GameObject testGround;
    ARRaycastManager aRRaycastManager;

    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        // 만약 유니티에서 실행하지 않았다면 testGround를 끄고 싶다.
#if UNITY_EDITOR 
        testGround.SetActive(true);
#else
        testGround.SetActive(false);
#endif
    }

    void Update()
    {

#if UNITY_EDITOR
        UpdateIndicatorForUnityEditor();
        UpdateTVForUnityEditor();
#else
        UpdateIndicator();
        UpdateTV();
#endif
    }

    public GameObject tv;

    private void UpdateTVForUnityEditor()
    {
        // 마우스로 버튼을 클릭하면
        if (Input.GetButtonDown("Fire1"))
        {
            // 2D좌표인 마우스 위치에서 3D월드로 향하는 Ray를 만들고
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            // 부딪힌 것이 Indicator라면
            int layer = 1 << LayerMask.NameToLayer("Indicator");
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            {
                // 그 위치에 TV를 이동 및 회전 시키고 싶다.
                tv.transform.position = hitInfo.point;
                tv.transform.rotation = hitInfo.transform.rotation;
                // TV를 보이게 하고 싶다.
                tv.SetActive(true);
                // tv를 처음부터 다시 재상하고 싶다.
                VideoPlayer vp = tv.GetComponentInChildren<VideoPlayer>();
                vp.Stop();
                vp.Play();
            }
        }
    }

    private void UpdateIndicatorForUnityEditor()
    {
        // 1. 카메라 위치에서 카메라 앞방향으로 Ray를 만들고
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        // 2. 바라보고 부딪힌 곳이 있다면
        int layer = 1 << LayerMask.NameToLayer("Indicator");
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layer))
        {
            // 3. 그 곳에 indicator를 위치시키고 회전도 하고 싶다.
            indicator.position = hitInfo.point + hitInfo.normal * 0.1f;
            indicator.up = hitInfo.normal;
        }
    }

    private void UpdateTV()
    {
        if(Input.touchCount == 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);
        // 화면을 터치하면
        if (touch.phase == TouchPhase.Began)
        {
            // 터치한 위치에서 3D월드로 향하는 Ray를 만들고
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;
            // 부딪힌 것이 Indicator라면
            int layer = 1 << LayerMask.NameToLayer("Indicator");
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            {
                // 그 위치에 TV를 이동 및 회전 시키고 싶다.
                tv.transform.position = hitInfo.point;
                tv.transform.rotation = hitInfo.transform.rotation;
                // TV를 보이게 하고 싶다.
                tv.SetActive(true);
                // tv를 처음부터 다시 재생하고 싶다.
                VideoPlayer vp = tv.GetComponentInChildren<VideoPlayer>();
                vp.Stop();
                vp.Play();
            }
        }
    }

    private void UpdateIndicator()
    {
        // 1. ARRaycastManager를 이용해서 바라보고 싶다.
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        if (aRRaycastManager.Raycast(center, hitResults))
        {
            indicator.rotation = hitResults[0].pose.rotation;
            indicator.position = hitResults[0].pose.position + indicator.up * 0.1f;
        }
    }
}
