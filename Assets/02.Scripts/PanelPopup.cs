using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PanelPopup : MonoBehaviour
{
    public GameObject Panel;
    Image image;

    RectTransform rt;

    void Start()
    {
        image = Panel.GetComponent<Image>();
        rt = transform.GetComponent<RectTransform>();
    }

    public void OpenPanel()
    {
        rt.DOAnchorPos(Vector2.zero, 0.5f);
        image.DOFade(1, 0.5f);
    }

    public void ClosePanel()
    {
        rt.DOAnchorPos(new Vector2(0, -1600), 0.5f);
        image.DOFade(0, 0.5f);
    }
}
