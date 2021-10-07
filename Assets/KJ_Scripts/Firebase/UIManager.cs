
using System;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject UiUpLoad;
    public GameObject UiIconX;
    public GameObject UIBackIcon;
    public GameObject UITempImageDoodle;

    

    public GameObject GuildCanvasGroup;
    public GameObject UIScreenshot;
    public GameObject UICameraIcon;

    public bool xIconOnOff=false;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        UiUpLoad.SetActive(false);
        UiIconX.SetActive(false);
        UIScreenshot.SetActive(false);
        UICameraIcon.SetActive(false) ;



        GuildCanvasGroup.SetActive(true);



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
        UiUpLoadOn();
        UITempImageDoodle.GetComponent<TemporaryDoodle>().SetContent(path);
    }
    public void UITempImageDoodleOff()
    {
        UITempImageDoodle.SetActive(false);
    }

    
}
