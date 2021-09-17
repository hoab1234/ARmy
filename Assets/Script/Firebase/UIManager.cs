using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject UiSelect;
    public GameObject UiUpLoad;
    public GameObject UiIconX;
    public bool xIconOnOff=false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UiSelect.SetActive(true);
        UiUpLoad.SetActive(false);
        UiIconX.SetActive(false);
    }

    public void UiUpLoadOn()
    {
        UiSelect.SetActive(false);
        UiUpLoad.SetActive(true);
    }
    public void UiSelectOn()
    {
        UiSelect.SetActive(true);
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
}
