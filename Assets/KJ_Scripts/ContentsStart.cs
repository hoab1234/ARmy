using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsStart : MonoBehaviour
{
    public static ContentsStart inst;
    void Awake(){
        if(inst == null)
        {
            inst = this;
        }
    }

    Renderer[] ContentsRenderer;


    // Start is called before the first frame update
    void Start()
    {
        ContentsRenderer =  GetComponentsInChildren<Renderer>();
        ARCamFacingDir.inst.SetRenderers(ContentsRenderer);
        UIManager.instance.GuildCanvasGroupOff();
        UIManager.instance.UISelectIconOnOff(true);

        StartCoroutine(CheckPos());

    }

    IEnumerator CheckPos(){
        while(true){

        yield return new WaitForSeconds(0.5f);
        print("transform position ========" + transform.position);
        print("Local position ========" + transform.localPosition);
        }
    }
}
