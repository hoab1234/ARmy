using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


// 여러개의 이미지를 인식하고 싶다.
// ARTrackedImageManager에게 이미지를 추적하는 내용을 전달받고 싶다.

public class MultiTrackedImage : MonoBehaviour
{
    ARTrackedImageManager aRTrackedImageManager;

    void Awake()
    {
        aRTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OntrackedImagesChanged;
    }

    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OntrackedImagesChanged;
    }

    [System.Serializable]
    public class TrackedImageInfo
    {
        public string name;
        public GameObject obj;
    }

    public List<TrackedImageInfo> list;

    private void OntrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // 추적된 이미지 목록을 전체 검사하고 싶다.
        for (int i = 0; i < obj.updated.Count; i++)
        {
            // 각 항목하나
            ARTrackedImage marker = obj.updated[i];

            for (int j = 0; j < list.Count; j++)
            {
                // 만약 내가 등록한 목록에 있는 것이라면
                if (marker.referenceImage.name == list[j].name)
                {
                    // 추적중이라면
                    if (marker.trackingState == TrackingState.Tracking)
                    {
                        list[j].obj.SetActive(true);
                        list[j].obj.transform.position = marker.transform.position;
                        list[j].obj.transform.rotation = marker.transform.rotation;
                    }
                    else
                    {
                        list[j].obj.SetActive(false);
                    }
                }
            }
        }
    }

    void Update()
    {

    }
}
