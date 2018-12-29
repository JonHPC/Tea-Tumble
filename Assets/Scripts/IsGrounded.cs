using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class IsGrounded : MonoBehaviour {

    //public TextMeshProUGUI groundedStatus;//shows the current status, either "air" or "grounded"

    public LayerMask layermask;
    public float Maxdistance = 2;
    public Vector3 direction = -Vector3.up;
    public RaycastHit hit;


    void Update()
    {

        Debug.DrawRay(transform.position, direction * Maxdistance, Color.green);//draws the ray for debugging
        if (Physics.Raycast(transform.position, direction, out hit, Maxdistance, layermask))
        {
            //the ray collided with something, you can interact
            // with the hit object now by using hit.collider.gameObject
            transform.parent.gameObject.GetComponent<PlayerMovement>().isGrounded = true;//change sthis boolean
            transform.parent.gameObject.GetComponent<Animator>().SetBool("isGrounded", true);//sets the animation according to whether or not the bunny is grounded.
        }
        else
        {
            transform.parent.gameObject.GetComponent<PlayerMovement>().isGrounded = false;
            transform.parent.gameObject.GetComponent<Animator>().SetBool("isGrounded", false);//sets the animation according to whether or not the bunny is grounded.
        }
    }

    /*
    void OnTriggerEnter (Collider other)//checks to make sure the player is grounded before allowing a jump
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Platform"))// makes sure the isGrounded collider is touching a collider tagged platform
        {
            return;
        }

        transform.parent.gameObject.GetComponent<PlayerMovement>().isGrounded = true;//If so, sets the isGrounded boolean in the playermovement script to true.

        //groundedStatus.text = "Ground";

       


    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Platform"))
        {
            return;
        }
       
        transform.parent.gameObject.GetComponent<PlayerMovement>().isGrounded = false; //accesses the Accelerometer script in the parent and changes the variable to false
        //groundedStatus.text = "Air";
    }
*/

}
