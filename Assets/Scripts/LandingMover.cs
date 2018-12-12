using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingMover : MonoBehaviour {

    public float landingColorSpeed;

   
   

    void FixedUpdate()
    {

        transform.Translate(Vector3.up * landingColorSpeed * Time.deltaTime, Space.World);//moves the landing color sprite upwards every frame
    }
}
