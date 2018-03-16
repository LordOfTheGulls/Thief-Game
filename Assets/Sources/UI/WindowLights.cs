using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


public class WindowLights : MonoBehaviour
{

    public Image[] ImageList = new Image[10];
    void Start()
    {

        InvokeRepeating("LightWindows", 0, 5);
        InvokeRepeating("DelayWindows", 0, 8);

    }

    void LightWindows()
    {
        int n = Random.Range(0, 10);
        ImageList[n].enabled = !ImageList[n].enabled;


    }

    void DelayWindows()
    {
        int n = Random.Range(0, 10);
        ImageList[n].enabled = false;

    }
}


