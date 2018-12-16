using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleIsGrounded : MonoBehaviour
{



    void OnTriggerEnter(Collider other)//checks to make sure the player is grounded before allowing a jump
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Platform"))// makes sure the isGrounded collider is touching a collider tagged platform
        {
            return;
        }

        transform.parent.gameObject.GetComponent<TitleBunny>().isGrounded = true;//If so, sets the isGrounded boolean in the playermovement script to true.



    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Platform"))
        {
            return;
        }

        transform.parent.gameObject.GetComponent<TitleBunny>().isGrounded = false; //accesses the Accelerometer script in the parent and changes the variable to false

    }
}