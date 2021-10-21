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
    public Transform[] right;
    public Transform[] left;
    ARFaceManager aRFaceManager;

    int index;
    // public Text textIndex;
    int[,] targetPos = { { 101, 330 }, { 69, 333 }, { 101, 330 }, { 159, 386 } , { 9, 10 } , { 101, 330 } , { 101, 330 } , { 101, 330 }, { 101, 330 }, { 101, 330 }, { 101, 330 }, { 101, 330 }, { 101, 330 }, { 101, 330 } };
    //int[] targetPos = new int[] { 101, 330 };
    public int[,] testArr;

    int firstIndex;

    // public int INDEX
    // {
    //     get { return index; }
    //     set
    //     {
    //         index = value;
    //         textIndex.text = index.ToString();
    //     }
    // }

    // int maxIndex = 468;
    // public void OnClickAddIndex()
    // {
    //     INDEX = (INDEX + 1) % maxIndex;
    // }

    // public void OnClickSubtractIndex()
    // {
    //     INDEX = (INDEX + maxIndex - 1) % maxIndex;
    // }

    void Awake()
    {
        //INDEX = 0;
        aRFaceManager = GetComponent<ARFaceManager>();

        for(int i = 0; i < right.Length; i++)
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

    // private void OnFaceChangedAll(ARFacesChangedEventArgs obj)
    // {
    //     for (int i = 0; i < obj.updated.Count; i++)
    //     {
    //         ARFace face = obj.updated[i];
    //         Vector3 pos = face.vertices[INDEX];
    //         pos = face.transform.TransformPoint(pos);
    //         cubes[0].position = pos;
    //     }
    // }

    // private void OnFaceChangedThree(ARFacesChangedEventArgs obj)
    // {
    //     NativeArray<ARCoreFaceRegionData> array = new NativeArray<ARCoreFaceRegionData>();
    //     for (int i = 0; i < obj.updated.Count; i++)
    //     {
    //         ARCoreFaceSubsystem core = aRFaceManager.subsystem as ARCoreFaceSubsystem;
    //         core.GetRegionPoses(obj.updated[i].trackableId, Allocator.Persistent, ref array);

    //         for (int j = 0; j < array.Length; j++)
    //         {
    //             switch (array[j].region)
    //             {
    //                 case ARCoreFaceRegion.NoseTip:
    //                     cubes[2].position = array[j].pose.position;
    //                     break;
    //                 case ARCoreFaceRegion.ForeheadLeft:
    //                     cubes[1].position = array[j].pose.position;
    //                     break;
    //                 case ARCoreFaceRegion.ForeheadRight:
    //                     cubes[0].position = array[j].pose.position;
    //                     break;
    //             }
    //         }
    //     }
    // }

    void ChoiceSticker(ARFacesChangedEventArgs obj)
    {
        //if (firstIndex == null ) return;

        ARFace face = obj.updated[0];
        // textIndex.text = $"{face.transform.rotation.eulerAngles}";
        /*
        for (int j = 0; j < targetPos.GetLength(firstIndex); j++)
        {
            Vector3 pos = face.vertices[targetPos[firstIndex, j]];
            Quaternion rot = face.transform.rotation;
            pos = face.transform.TransformPoint(pos);
            right[firstIndex].position = pos;
            left[firstIndex].position = pos;
            right[firstIndex].rotation = rot;
            left[firstIndex].rotation = rot;
        }
        */

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
            if(i == firstIndex) right[i].gameObject.SetActive(true);
            else right[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < left.Length; i++)
        {
            if (i == firstIndex) left[i].gameObject.SetActive(true);
            else left[i].gameObject.SetActive(false);
        }
    }
}
