using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupControll : MonoBehaviour
{

    public static CanvasGroupControll inst;

    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
    }

    private CanvasGroup m_CanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        m_CanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
    }

    public void SetCanvasForScreenShot(){
        SetCanvasAlphaZero();
        StartCoroutine(M_WaitSeconds());
        SetCanvasAlphaOne();
    }
    private void SetCanvasAlphaZero()
    {
        m_CanvasGroup.alpha=0;
        print("setcanvasZERO-------------"+m_CanvasGroup.alpha);
    }
    private void SetCanvasAlphaOne()
    {
        m_CanvasGroup.alpha = 1;
        print("setcanvasOne=============="+m_CanvasGroup.alpha);
    }
    IEnumerator M_WaitSeconds(){
        print("waitCoroutine");
        yield return new WaitForSeconds(0.2f);
    }
}
