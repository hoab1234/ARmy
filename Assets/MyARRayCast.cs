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
        // ���� ����Ƽ���� �������� �ʾҴٸ� testGround�� ���� �ʹ�.
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
        // ���콺�� ��ư�� Ŭ���ϸ�
        if (Input.GetButtonDown("Fire1"))
        {
            // 2D��ǥ�� ���콺 ��ġ���� 3D����� ���ϴ� Ray�� �����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            // �ε��� ���� Indicator���
            int layer = 1 << LayerMask.NameToLayer("Indicator");
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            {
                // �� ��ġ�� TV�� �̵� �� ȸ�� ��Ű�� �ʹ�.
                tv.transform.position = hitInfo.point;
                tv.transform.rotation = hitInfo.transform.rotation;
                // TV�� ���̰� �ϰ� �ʹ�.
                tv.SetActive(true);
                // tv�� ó������ �ٽ� ����ϰ� �ʹ�.
                VideoPlayer vp = tv.GetComponentInChildren<VideoPlayer>();
                vp.Stop();
                vp.Play();
            }
        }
    }

    private void UpdateIndicatorForUnityEditor()
    {
        // 1. ī�޶� ��ġ���� ī�޶� �չ������� Ray�� �����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;
        // 2. �ٶ󺸰� �ε��� ���� �ִٸ�
        int layer = 1 << LayerMask.NameToLayer("Indicator");
        if (Physics.Raycast(ray, out hitInfo, float.MaxValue, ~layer))
        {
            // 3. �� ���� indicator�� ��ġ��Ű�� ȸ���� �ϰ� �ʹ�.
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
        // ȭ���� ��ġ�ϸ�
        if (touch.phase == TouchPhase.Began)
        {
            // ��ġ�� ��ġ���� 3D����� ���ϴ� Ray�� �����
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;
            // �ε��� ���� Indicator���
            int layer = 1 << LayerMask.NameToLayer("Indicator");
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            {
                // �� ��ġ�� TV�� �̵� �� ȸ�� ��Ű�� �ʹ�.
                tv.transform.position = hitInfo.point;
                tv.transform.rotation = hitInfo.transform.rotation;
                // TV�� ���̰� �ϰ� �ʹ�.
                tv.SetActive(true);
                // tv�� ó������ �ٽ� ����ϰ� �ʹ�.
                VideoPlayer vp = tv.GetComponentInChildren<VideoPlayer>();
                vp.Stop();
                vp.Play();
            }
        }
    }

    private void UpdateIndicator()
    {
        // 1. ARRaycastManager�� �̿��ؼ� �ٶ󺸰� �ʹ�.
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        if (aRRaycastManager.Raycast(center, hitResults))
        {
            indicator.rotation = hitResults[0].pose.rotation;
            indicator.position = hitResults[0].pose.position + indicator.up * 0.1f;
        }
    }
}
