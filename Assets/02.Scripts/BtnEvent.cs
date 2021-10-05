using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    public bool isFocusing = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(markerInfoPanel != null)
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
        image.DOFade(1, 0.5f);
    }

    public void CloseMarkerInfoPanel()
    {
        rt.DOAnchorPos(new Vector2(0, -1600), 0.5f);
        image.DOFade(0, 0.5f);
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
        if (changeToGlobalBtn != null) changeToGlobalBtn.interactable = !QuitPanel.activeSelf;
        if (changeToLocalBtn != null) changeToLocalBtn.interactable = !QuitPanel.activeSelf;
        if (focusBtn != null) focusBtn.interactable = !QuitPanel.activeSelf;

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
            if (Camera.main.transform.localPosition.z < 3200)
            {
                Camera.main.transform.localPosition = new Vector3(0, 0, Mathf.Lerp(Camera.main.transform.localPosition.z, 3200, 10 * Time.deltaTime));
            }
            else
            {
                isFocusing = false;
            }
        }
    }
}