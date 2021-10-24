using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RecordStopBtnBlink : MonoBehaviour
{
    //Canvas guideCanvas;
    private CanvasGroup canvasGroup01;
    // Start is called before the first frame update

    private bool show;

    void Start()
    {
        canvasGroup01 = gameObject.GetComponent<CanvasGroup>();
        show = true;
        StartCoroutine(Blink());
    }
    bool fadeIn = true;
    IEnumerator Blink()
    {
        while (show)
        {
            if (fadeIn)
            {
                canvasGroup01.DOFade(0.3f, 1f);
                fadeIn = false;
            }
            else
            {
                canvasGroup01.DOFade(1f, 1f);
                fadeIn = true;
            }

            yield return new WaitForSeconds(1f);
        }

    }
    private void OnDisable()
    {
        show = false;
    }

}
