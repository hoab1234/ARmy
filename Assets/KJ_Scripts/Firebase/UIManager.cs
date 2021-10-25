
using System;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject FaceFilter;

    public GameObject UiUpLoad;
    public GameObject UiIconX;
    public GameObject UIBackIcon;
    public GameObject UITempImageDoodle;
    public GameObject UITempVideoDoodle;
    public GameObject UIScreenShot;
    public GameObject GuildCanvasGroup;
    public GameObject UIScreenshot;
    public GameObject UISelectIcon;
    public GameObject UIVideoIcon;
    public GameObject UICameraIcon;
    public GameObject XiconForContents;
    public GameObject RecordBtn;
    public GameObject RecordStopBtn;
    public GameObject UIChangeCamFacingDir;

    public GameObject UIFaceFilterScroll;

    public Transform VideoCameraPos;
    public Transform VideoCameraTargetPos;
    public Transform VideoCameraStartPos;

    public Transform UIFacingDirStartPos;

    public Transform UIFacingDirTargetPos;

    public bool xIconOnOff=false;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        UiUpLoad.SetActive(false);
        UiIconX.SetActive(false);
        UIScreenshot.SetActive(false);
        UICameraIcon.SetActive(true);
        GuildCanvasGroup.SetActive(true);
        RecordBtn.SetActive(false);
        RecordStopBtn.SetActive(false);
        UIScreenShot.SetActive(false);
        UISelectIcon.SetActive(false);
        UIFaceFilterScroll.SetActive(false);
    }

    // Start is called before the first frame update
    public void Start()
    {
      

    }
    internal void GuildCanvasGroupOn()
    {
        GuildCanvasGroup.SetActive(true);
    }
    internal void GuildCanvasGroupOff()
    {
        GuildCanvasGroup.SetActive(false);
    }
    public void UiUpLoadOn()
    {
        UiUpLoad.SetActive(true);
    }
    public void UiUpLoadOff()
    {
        UiUpLoad.SetActive(false);
    }
   
    public void UiIconXOn()
    {
        UiIconX.SetActive(true);
        xIconOnOff = true;
    }
    public void UiIconXOff()
    {
        UiIconX.SetActive(false);
        xIconOnOff = false;
        
    }

    public void UIScreenShotBtnOn()
    {
        UIScreenshot.SetActive(true);
    }
    public void UIScreenShotBtnOff()
    {
        UIScreenshot.SetActive(false);
    }
    public void UICameraIconOn()
    {
        UICameraIcon.SetActive(true);
    }
    public void UICameraIconOff()
    {
        UICameraIcon.SetActive(false);
    }

    public void UIBackIconBtnOff()
    {
        UIBackIcon.SetActive(false);
    }
    public void UIBackIconBtnOn()
    {
        UIBackIcon.SetActive(true);
    }


    public void UITempImageDoodleOn(string path)
    {
        UITempImageDoodle.SetActive(true);
        UITempImageDoodle.GetComponent<TemporaryDoodle>().SetImage(path);
        UiUpLoadOn();
    }
    public void UITempVideoDoodleOn()
    {
        UITempVideoDoodle.SetActive(true);
        UiUpLoadOn();
        UITempVideoDoodle.GetComponent<TemporaryDoodle>().SetVideo();
    }
    public void UITempImageDoodleOff()
    {
        UITempImageDoodle.SetActive(false);
    }
    public void UITempVideoDoodleOff()
    {
        UITempVideoDoodle.SetActive(false);
    }
    public void UIScreenShotOnOff(bool BOOL)
    {
        UIScreenshot.SetActive(BOOL);
    }
   
    public void UIVideoCameraIconOnOff(bool BOOL)
    {
        if(BOOL)
        {
            VideoCameraPos.DOMove(VideoCameraTargetPos.position, 1f);
        }
        else
        {
            VideoCameraPos.DOMove(VideoCameraStartPos.position, 1f);
        }
    }
    public void XiconForContentsOnOff(bool BOOL)
    {
        if (BOOL)
        {
            XiconForContents.SetActive(BOOL);
        }
        else
        {
            XiconForContents.SetActive(BOOL);
        }
    }
    public void UISelectIconOnOff(bool BOOL)
    {
        if (BOOL)
        {
            UISelectIcon.SetActive(BOOL);
        }
        else
        {
            UISelectIcon.SetActive(BOOL);
        }
    }
    public void UI_Record_OnOff(bool BOOL)
    {
        if (BOOL)
        {
            RecordBtn.SetActive(BOOL);
        }
        else
        {
            RecordBtn.SetActive(BOOL);
        }
    }
    public void UI_Record_Stop_OnOff(bool BOOL)
    {
        if (BOOL)
        {
            RecordStopBtn.SetActive(BOOL);
        }
        else
        {
            RecordStopBtn.SetActive(BOOL);
        }
    }
      public void UIChangeCamFacingDirIconOnOff(bool BOOL)
    {
        if(BOOL)
        {
            UIChangeCamFacingDir.transform.DOMove(UIFacingDirTargetPos.position, 1f);
        }
        else
        {
            UIChangeCamFacingDir.transform.DOMove(UIFacingDirStartPos.position, 1f);
        }
    }

    private bool useFilter = true;
    public void UIFaceFilterScrollToggle()
    {
         if (useFilter)
        {
            UIFaceFilterScroll.SetActive(useFilter);
            useFilter = false;
        }
        else
        {
            UIFaceFilterScroll.SetActive(useFilter);
            useFilter= true;
        }
    }
    public void UIFaceFilterScrollOnOff(bool BOOL)
    {
          if (BOOL)
        {
            UIFaceFilterScroll.SetActive(BOOL);
            useFilter = false;
        }
        else
        {
            UIFaceFilterScroll.SetActive(BOOL);
            useFilter= true;
        }
    }

    public void FaceFilterStickerOnOff(bool BOOL){
         if (BOOL)
        {
            FaceFilter.SetActive(BOOL);
        }
        else
        {
            FaceFilter.SetActive(BOOL);
        }
    }
}
