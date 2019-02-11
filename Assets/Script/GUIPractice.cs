using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPractice : MonoBehaviour {

    private void OnGUI()
    {
        GUI.Label(new Rect(100,50,100,30),"Hello World");
    }

}
