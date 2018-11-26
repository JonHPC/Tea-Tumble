using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {



    public float platformMoverSpeed;



    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * platformMoverSpeed * Time.deltaTime, Space.World);//moves the platform upwards every frame
    }
}
