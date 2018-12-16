using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPSMover : MonoBehaviour {

    private Vector3 pos1 = new Vector3(-10, 19, 5);
    private Vector3 pos2 = new Vector3(10, 19, 5);
    public float speed = 1.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f)); //moves the superPs2 back and forth between the defined X values over one second
    }
}
