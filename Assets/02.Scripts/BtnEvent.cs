using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Mapbox.Examples;

public class BtnEvent : MonoBehaviour
{
    public static BtnEvent instance;

    public GameObject markerInfoPanel;
    RectTransform rt;
    Image image;

    public GameObject QuitPanel;

    public Button changeToGlobalBtn;
    public Button changeToLocalBtn;
    public Button focusBtn;

    public Transform cameraPositionOrigin;

    public bool isFocusing = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (markerInfoPanel != null)
        {
            image = markerInfoPanel.GetComponent<Image>();
            rt = markerInfoPanel.GetComponent<RectTransform>();
        }
    }
    public void SceneToMain()
    {
        SceneManager.LoadScene(2);
    }
    public void OpenMarkerInfoPanel()
    {
        rt.DOAnchorPos(Vector2.zero, 0.5f);
        //image.DOFade(1, 0.5f);
    }

    public void CloseMarkerInfoPanel()
    {
        rt.DOAnchorPos(new Vector2(0, -1600), 0.5f);
        //image.DOFade(0, 0.5f);
    }

    public void ChangeToGlobal()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeToLocal()
    {
        SceneManager.LoadScene(0);
    }

    public void Focus()
    {
        isFocusing = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CancelQuitPanel()
    {
        QuitPanel.SetActive(false);
    }

    void Update()
    {
        //Debug.Log($"isFocusing: {isFocusing}");

        if (changeToGlobalBtn != null)
        {
            if (QuitPanel.activeSelf == true || rt.anchoredPosition.y == 0)
            {
                changeToGlobalBtn.interactable = false;
            }
            else
            {
                changeToGlobalBtn.interactable = true;
            }
        }

        if (changeToLocalBtn != null)
        {
            if (QuitPanel.activeSelf == true || rt.anchoredPosition.y == 0)
            {
                changeToLocalBtn.interactable = false;
            }
            else
            {
                changeToLocalBtn.interactable = true;
            }
        }

        if (focusBtn != null)
        {
            if (QuitPanel.activeSelf == true || rt.anchoredPosition.y == 0)
            {
                focusBtn.interactable = false;
            }
            else
            {
                focusBtn.interactable = true;
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (QuitPanel.activeSelf)
                {
                    QuitPanel.SetActive(false);
                }
                else if (markerInfoPanel != null && rt.anchoredPosition.y == 0)
                {
                    CloseMarkerInfoPanel();
                }
                else
                {
                    QuitPanel.SetActive(true);
                }
            }
        }

        if (isFocusing)
        {
            if (Camera.main.transform.localPosition.z < 3200 || cameraPositionOrigin.localPosition.z < 3200)
            {
                PlayerRigPos.instance.isChangeScale = true;
                SpawnOnMap.instance.isChangeScale = true;
                Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, Mathf.Lerp(Camera.main.transform.localPosition.z, 3200, 10 * Time.deltaTime));
                //cameraPositionOrigin.localPosition = Camera.main.transform.localPosition;
                cameraPositionOrigin.localPosition = new Vector3(cameraPositionOrigin.localPosition.x, cameraPositionOrigin.localPosition.y, Mathf.Lerp(cameraPositionOrigin.localPosition.z, 3200, 10 * Time.deltaTime));
                //isFocusing = false;
            }
            else
            {
                isFocusing = false;
            }
        }
    }
}