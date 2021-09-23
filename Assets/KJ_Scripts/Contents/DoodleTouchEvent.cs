using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class DoodleTouchEvent : MonoBehaviour
{
    public static DoodleTouchEvent instance;

    private void Awake()
    {
        instance = this;
    }
    private Touch touch;
    public float rayLength = 100;
    public Transform pos;
    public bool isPlaying = false;
    bool alreadyFocus = false;
    GameObject doodle;
    public float lerpSpeed = 3f;
    void Start()
    {
        
        //StartCoroutine("TouchDoodle");
    }
    VideoToogleBtn videotoogleBtn;
    // Update is called once per frame
    GameObject tempobj;
    Vector3 tempVector3;
    void Update()
    {
        if(cameraFocus)
        {
            //tempobj.transform.position = pos.transform.position;
            doodle.transform.position = pos.transform.position;
        }
        

        #if UNITY_EDITOR
        if (  Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {
                
                    if (hitinfo.transform.CompareTag("DOODLE"))
                    {
                    if (!alreadyFocus || hitinfo.transform.GetComponent<VideoToogleBtn>()!=null)
                    {
                        tempobj = hitinfo.transform.gameObject;
                        if (! alreadyFocus)
                        { 
                            tempVector3 = hitinfo.transform.transform.position;
                            StartCoroutine("XiconOn");
                        }
                        alreadyFocus = true;
                        tempobj.transform.DOMove(pos.transform.position, 1f, false);
                        videotoogleBtn = hitinfo.transform.GetComponent<VideoToogleBtn>();
                        if (isVideoFocus && videotoogleBtn != null) // video doodle
                        {
                            if (!isPlaying)
                            {
                                videotoogleBtn.Play();
                                isPlaying = true;
                            }
                            else if (isPlaying)
                            {
                                videotoogleBtn.Pause();
                                isPlaying = false;
                            }
                        }
                    }
                }
            }
        }
      
        #endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }

        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        //DebugUI.instance.UpdateDebugForGm("touch ray");

       

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began && Physics.Raycast(touchRay, out hit))
        {
            if (hit.transform.CompareTag("DOODLE"))
            {
                if (!alreadyFocus || hit.transform.GetComponent<VideoToogleBtn>() != null)
                {

                    doodle = hit.transform.gameObject;
                    if (!alreadyFocus)
                    {
                        tempVector3 = hit.transform.transform.position;
                        StartCoroutine("XiconOn");
                    }
                    alreadyFocus = true;
                    doodle.transform.DOMove(pos.transform.position, 1f, false);

                    videotoogleBtn = hit.transform.GetComponent<VideoToogleBtn>();
                    
                    if ( isVideoFocus && videotoogleBtn != null) // video doodle
                    {
                        if (!isPlaying)
                        {
                            videotoogleBtn.Play();
                            isPlaying = true;
                        }
                        else if (isPlaying)
                        {
                            videotoogleBtn.Pause();
                            isPlaying = false;
                        }
                    }
                    else
                    { // image doodle

                    }
                }

            }
        }
        #endif
    }
    
    bool isVideoFocus = false;
    bool cameraFocus = false;
    IEnumerator XiconOn()
    {
        print("icobn on -------");
        yield return new WaitForSeconds(1f);
        UIManager.instance.UiIconXOn();
        isVideoFocus = true;
        cameraFocus = true;
        
    }
    
    
    public void ClickXicon()
    {
        //�̸� �ڸ��� ���ư���
        //���� ���ϵ�� �վ���� Ʈ���������� �ǵ�����
        print("icobn offfff -------");
        alreadyFocus = false;
        isVideoFocus = false;
        UIManager.instance.UiIconXOff();
        cameraFocus = false;
        if (videotoogleBtn != null)
        {
            videotoogleBtn.GetComponent<VideoPlayer>().Stop();
            videotoogleBtn.xIconClicked = true;
        }
        //tempobj.transform.DOMove(tempVector3, 1f, false);
        doodle.transform.DOMove(tempVector3, 1f, false);
    }
}



/*IEnumerator TouchDoodle()
{
    while (true)
    {
        touch = Input.GetTouch(0);
        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        DebugUI.instance.UpdateDebugForGm("touch ray");
        if (Physics.Raycast(touchRay, out hit))
        {
            if (hit.transform.CompareTag("DOODLE"))
            {
                DebugUI.instance.UpdateDebugForGm("doodle touch");
                //hit.transform.position = Vector3.Lerp(hit.transform.position, pos, 5);
                videotoogleBtn = hit.transform.GetComponent<VideoToogleBtn>();
                if (videotoogleBtn != null) // video doodle
                {
                    if (!isPlaying)
                    {

                        videotoogleBtn.Play();
                        yield return new WaitForSeconds(0.2f);
                        isPlaying = true;
                    }
                    else if (isPlaying)
                    {
                        videotoogleBtn.Pause();
                        yield return new WaitForSeconds(0.2f);
                        isPlaying = false;
                    }
                }
                else
                { // image doodle

                }

            }
        }
        yield return new WaitForSeconds(0.2f);
    }
}*/




/* void Update()
 {
     touch = Input.GetTouch(0);
     Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
     RaycastHit hit;
     DebugUI.instance.UpdateDebugForGm("touch ray");
     if (Physics.Raycast(touchRay, out hit))
     {
         if (hit.transform.CompareTag("DOODLE"))
         {
             DebugUI.instance.UpdateDebugForGm("doodle touch");
             //hit.transform.position = Vector3.Lerp(hit.transform.position, pos, 5);
             videotoogleBtn = hit.transform.GetComponent<VideoToogleBtn>();
             if (videotoogleBtn != null) // video doodle
             {
                 if (!isPlaying)
                 {

                     videotoogleBtn.Play();
                     isPlaying = true;
                 }
                 else if (isPlaying)
                 {
                     videotoogleBtn.Pause();
                     isPlaying = false;
                 }
             }
             else
             { // image doodle

             }
         }
     }
 }*/