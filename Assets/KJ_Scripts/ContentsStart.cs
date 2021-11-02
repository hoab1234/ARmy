using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentsStart : MonoBehaviour
{
    public static ContentsStart inst;
    void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
    }

    Renderer[] ContentsRenderer;

    void Start()
    {
        ContentsRenderer = GetComponentsInChildren<Renderer>();
        ARCamFacingDir.inst.SetRenderers(ContentsRenderer);
        UIManager.instance.GuildCanvasGroupOff();
        UIManager.instance.UISelectIconOnOff(true);
    }
}
