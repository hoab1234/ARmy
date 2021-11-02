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
    public float rayLength = 100;
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

    Transform tempParentsT;
    GameObject tempParent;
    

    void Update()
    {
        if (cameraFocus)
        {
            
            //tempobj.transform.DOMove(pos.transform.position, 0.01f, false);
            //tempobj.transform.position = pos.transform.position;//lerp로 바꿀것 or vector3.toward
            //doodle.transform.position = pos.transform.position;
        }


#if UNITY_EDITOR
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo))
            {

                if (hitinfo.transform.CompareTag("DOODLE"))
                {
                    if (!alreadyFocus || hitinfo.transform.GetComponent<VideoToogleBtn>() != null)
                    {
                        tempobj = hitinfo.transform.gameObject;
                        if (!alreadyFocus)
                        {
                           
                            hitinfo.transform.gameObject.GetComponent<Doodle>().EnableToggleBillborad(true);
                            hitinfo.transform.gameObject.GetComponent<Doodle>().GoToCamera();
                            StartCoroutine("XiconOn");
                        }
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
                        hit.transform.gameObject.GetComponent<Doodle>().EnableToggleBillborad(true);
                        StartCoroutine("XiconOn");
                        hit.transform.gameObject.GetComponent<Doodle>().GoToCamera();
                    }
                    alreadyFocus = true;

                    videotoogleBtn = hit.transform.GetComponent<VideoToogleBtn>();

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
#endif
       
      

    }
    bool isVideoFocus = false;
    bool cameraFocus = false;
    IEnumerator XiconOn()
    {
        yield return new WaitForSeconds(1.2f);
        UIManager.instance.UiIconXOn();
        isVideoFocus = true;
    }


    public void ClickXicon()
    {
        //�̸� �ڸ��� ���ư���
        //���� ���ϵ�� �վ���� Ʈ���������� �ǵ�����
        print("icobn offfff -------");
        alreadyFocus = false;
        isVideoFocus = false;
        UIManager.instance.UiIconXOff();
        if (videotoogleBtn != null)
        {
            videotoogleBtn.GetComponent<VideoPlayer>().Stop();
            videotoogleBtn.xIconClicked = true;
        }

      // tempobj.GetComponent<Doodle>().GoBack();
      //  tempobj.GetComponent<Doodle>().EnableToggleBillborad(false);

#if UNITY_ANDROID
         doodle.GetComponent<Doodle>().GoBack();
         doodle.GetComponent<Doodle>().EnableToggleBillborad(false);
#endif
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