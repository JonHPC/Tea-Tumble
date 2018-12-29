using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour {


    public void OpenURL()
    {
        Application.OpenURL("https://teabunnystudios.com");//opens this URL in a browser
        Debug.Log("Logo button works");
    }
}
