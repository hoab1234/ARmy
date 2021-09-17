using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DoodleTouchEvent : MonoBehaviour
{
    public static DoodleTouchEvent instance;

    private void Awake()
    {
        instance = this;
    }
    private Touch touch;
    private Vector3 touchPos;
    public float rayLength = 100;
    Vector3 pos;
    public bool isPlaying = false;
    bool isTouched = false;
    bool alreadyFocus = false;
    GameObject doodle;
    public float lerpSpeed = 3f;
    bool tempbool2 = false;
    void Start()
    {
        //StartCoroutine("TouchDoodle");
    }
    VideoToogleBtn videotoogleBtn;
    // Update is called once per frame
    GameObject tempobj;
    bool tempbool = false;
    Vector3 tempVector3;
    void Update()
    {
#if UNITY_EDITOR
        if(goBack && !tempbool)
        {
            print("goback is true , tempbool is false");
            //videotoogleBtn.GetComponent<VideoPlayer>().Stop();
            tempobj.transform.position = Vector3.Lerp(tempobj.transform.position, tempVector3, Time.deltaTime * lerpSpeed * 5);
#endif

/*#if UNITY_ANDROID
            doodle.transform.position = Vector3.Lerp(doodle.transform.position, tempVector3, Time.deltaTime * lerpSpeed * 5);
#endif*/
        }

        pos = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

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
                        tempVector3 = hitinfo.transform.transform.position;
                        tempbool = true;
                        alreadyFocus = true;
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
        if (tempbool && !goBack)
        {
            tempobj.transform.position = Vector3.Lerp(tempobj.transform.position, pos, Time.deltaTime * lerpSpeed);
            if (!tempbool2)
            {
                StartCoroutine("XiconOn");
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
        DebugUI.instance.UpdateDebugForGm("touch ray");

        if (isTouched && ! goBack)
        {
            
            doodle.transform.position = Vector3.Lerp(doodle.transform.position, pos, Time.deltaTime * lerpSpeed);
            if (! tempbool2)
            {
                StartCoroutine("XiconOn");
            }
            
        }

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began && Physics.Raycast(touchRay, out hit))
        {
            if (hit.transform.CompareTag("DOODLE"))
            {
                if (!alreadyFocus || hit.transform.GetComponent<VideoToogleBtn>() != null)
                {

                    alreadyFocus = true;
                    isTouched = true;
                    
                    tempVector3 = hit.transform.position;
                    doodle = hit.transform.gameObject;

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
    IEnumerator XiconOn()
    {
        tempbool2 = true;
        yield return new WaitForSeconds(2f);
        UIManager.instance.UiIconXOn();
        isVideoFocus = true;
    }
    IEnumerator DoodleGoBackDone()
    {
        yield return new WaitForSeconds(3f);
        goBack = false;
    }
    bool goBack = false;
    public void ClickXicon()
    {
        //이만 자리로 돌아가줘
        //원래 차일드로 잇어야할 트랜스폼으로 되돌리기
        tempbool = false;
        alreadyFocus = false;
        isVideoFocus = false;
        isTouched=false;
        tempbool2 = false;
        UIManager.instance.UiIconXOff();
       
            goBack = true;
            StartCoroutine("DoodleGoBackDone");
        
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