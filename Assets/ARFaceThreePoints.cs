using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using Unity.Collections;
using UnityEngine.UI;

public class ARFaceThreePoints : MonoBehaviour
{
    public static ARFaceThreePoints inst;


    public Transform[] right;
    public Transform[] left;
    ARFaceManager aRFaceManager;

    int[,] targetPos = { { 205, 425 }, { 69, 333 }, { 159, 386 }, { 205, 425 }, { 9, 10 }, { 205, 425 }, { 205, 425 }, { 205, 425 }, { 205, 425 }, { 205, 425 }, { 205, 425 }, { 205, 425 }, { 205, 425 }, { 205, 425 } };
    public int[,] testArr;

    int firstIndex;

    void Awake()
    {
        firstIndex = 0;

        if(inst == null)
        {
            inst = this;
        }
        aRFaceManager = GetComponent<ARFaceManager>();

        for (int i = 0; i < right.Length; i++)
        {
            right[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < left.Length; i++)
        {
            left[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        aRFaceManager.facesChanged += ChoiceSticker;
    }

    private void OnDisable()
    {
        aRFaceManager.facesChanged -= ChoiceSticker;
    }

    void ChoiceSticker(ARFacesChangedEventArgs obj)
    {
        ARFace face = obj.updated[0];

        Vector3 pos1 = face.vertices[targetPos[firstIndex, 0]];
        Quaternion rot1 = face.transform.rotation;
        pos1 = face.transform.TransformPoint(pos1);
        right[firstIndex].position = pos1;
        right[firstIndex].rotation = rot1;

        Vector3 pos2 = face.vertices[targetPos[firstIndex, 1]];
        Quaternion rot2 = face.transform.rotation;
        pos2 = face.transform.TransformPoint(pos2);
        left[firstIndex].position = pos2;
        left[firstIndex].rotation = rot2;
    }

    public void ChangeTargetPos(int setIndex)
    {
        firstIndex = setIndex;

        for (int i = 0; i < right.Length; i++)
        {
            if (i == firstIndex) right[i].gameObject.SetActive(true);
            else right[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (i == firstIndex) left[i].gameObject.SetActive(true);
            else left[i].gameObject.SetActive(false);
        }
    }

    // KKJ
    public void SetNoneFaceFilter()
    {
        firstIndex = 0;

        for (int i = 0; i < right.Length; i++)
        {
            if (i == firstIndex) right[i].gameObject.SetActive(true);
            else right[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (i == firstIndex) left[i].gameObject.SetActive(true);
            else left[i].gameObject.SetActive(false);
        }
    }
}
