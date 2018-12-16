using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGlow : MonoBehaviour {


    public Gradient myGradient;//accessses the gradient
    public float strobeDuration = 2f;//duration of gradient
    //public float ltSpeed;//the speed at which the lts move upwards when spawned
    Light lt;//accesses the light component

    void Start()
    {
        lt = GetComponent<Light>();//initializes the light component
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);//sets t to a duration set
        lt.color = myGradient.Evaluate(t);//runs the gradient based on t
        //Destroy(gameObject,strobeDuration);//destroys the gradient after the duration is done

    }

    /*void FixedUpdate()
    {

        transform.Translate(Vector3.up * ltSpeed * Time.deltaTime, Space.World);//moves the landing color sprite upwards every frame
    }*/
}
