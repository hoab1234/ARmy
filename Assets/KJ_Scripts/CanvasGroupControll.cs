using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupControll : MonoBehaviour
{

    public static CanvasGroupControll inst;

    private CanvasGroup m_CanvasGroup;
    // Start is called before the first frame update
    void Awake()
    {
        if(inst ==null)
        {
            inst = this;
        }
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCanvasForScreenShot(){
        print("set canvasforscreenshot-----------------------------------------");
        SetCanvasAlphaZero();
        StartCoroutine(M_WaitSeconds());
        
    }
    private void SetCanvasAlphaZero()
    {
        m_CanvasGroup.alpha=0;
        print("setcanvasZERO-------------"+m_CanvasGroup.alpha);
    }
    IEnumerator M_WaitSeconds(){
        print("waitCoroutine----------------------------------------------");
        yield return new WaitForSeconds(1f);
        SetCanvasAlphaOne();
    }
    private void SetCanvasAlphaOne()
    {
        m_CanvasGroup.alpha = 1;
        print("setcanvasOne=============="+m_CanvasGroup.alpha);
    }
    
}
