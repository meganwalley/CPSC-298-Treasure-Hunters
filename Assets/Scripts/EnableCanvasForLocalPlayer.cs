using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

public class EnableCanvasForLocalPlayer : NetworkBehaviour
{

    void Start()
    {
        if (isLocalPlayer)
        {
            Component[] allCanvas = GetComponentsInChildren<Canvas>();
            foreach(Canvas canvas in allCanvas)
            {
                canvas.enabled = true;
            }
        }
    }
}
