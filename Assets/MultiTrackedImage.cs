using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


// �������� �̹����� �ν��ϰ� �ʹ�.
// ARTrackedImageManager���� �̹����� �����ϴ� ������ ���޹ް� �ʹ�.

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
        // ������ �̹��� ����� ��ü �˻��ϰ� �ʹ�.
        for (int i = 0; i < obj.updated.Count; i++)
        {
            // �� �׸��ϳ�
            ARTrackedImage marker = obj.updated[i];

            for (int j = 0; j < list.Count; j++)
            {
                // ���� ���� ����� ��Ͽ� �ִ� ���̶��
                if (marker.referenceImage.name == list[j].name)
                {
                    // �������̶��
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
