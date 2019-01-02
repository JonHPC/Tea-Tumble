using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBunny : MonoBehaviour {


    public bool instructionsOn;//used for the instructions tutorial to set animations
    public bool isGrounded;//used to set animation when grounded
    public bool isMoving; //used to set running animation
    public float tiltSpeed;

    private SpriteRenderer flipIt;//references the sprite renderer to flip the sprite when going left or right

    public AudioSource jumpSound;//refernces the jump sound component

    //private Vector3 pos;//will store the player position
    //public GameObject light;//the groundglow game object
    //public GameObject light1;//ditto above
    //public GameObject light2;//ditto above
    //public GameObject light3;//ditto above

    Vector3 originalPos;

                              
    void Start () {
        originalPos = gameObject.transform.position;//stores the initial position of the object
        flipIt = GetComponent<SpriteRenderer>();//initializes the sprite renderer component
        //StartCoroutine(offset());//starts the coroutine for the groundglow lag
        tiltSpeed = 0.5f;
        jumpSound = GetComponent<AudioSource>();//initializes this audio source

    }

    private void Update()
    {
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);//sets the jump sound volume to the player pref
        jumpSound.volume = sfxVolume;//sets the jump sound to the player pref

        Rigidbody bunnyRb = GetComponent<Rigidbody>();//initializes the Rigidbody component
        bunnyRb.AddForce(new Vector3(0f, -50f, 0f));//constant downward force

        if (instructionsOn == true)//if instructionsOn is true, allow touch to cause bunny to jump
        {

            //bunnyRb.AddForce(Input.acceleration.x * 200f, 0f, 0f);//the input (-1 to 1) will be multiplied by 200f to control horizontal movement
            //bunnyRb.AddForce(0f, -50f, 0f);//this is the constant downward "gravity" force applied to the player
            /*if (Input.acceleration.x < 0)//if device is tilted to the left, the sprite will be flipped on its x axis
            {
                flipIt.flipX = true;
            }
            else if (Input.acceleration.x >= 0)//otherwise the sprite will face to the right
            {
                flipIt.flipX = false;
            }*/

            Vector3 pos = transform.position;
            Animator animator = GetComponent<Animator>();//initializes the Animator component

            if (Input.acceleration.x < -0.1f && isGrounded == true)//if device is tilted to the left, the sprite will be flipped on its x axis
            {
                flipIt.flipX = true;
                pos.x -= tiltSpeed;
                transform.position = pos;
                isMoving = true;
                animator.SetBool("isMoving", isMoving);

            }
            else if (Input.acceleration.x < -0.1f && isGrounded == false)
            {
                flipIt.flipX = true;
                pos.x -= tiltSpeed;
                transform.position = pos;
            }
            else if (Input.acceleration.x > 0.1f && isGrounded == true)//otherwise the sprite will face to the right
            {
                flipIt.flipX = false;
                pos.x += tiltSpeed;
                transform.position = pos;
                isMoving = true;
                animator.SetBool("isMoving", isMoving);
            }
            else if (Input.acceleration.x > 0.1f && isGrounded == false)
            {
                flipIt.flipX = false;
                pos.x += tiltSpeed;
                transform.position = pos;
            }
            else if (Input.acceleration.x > -0.1f && Input.acceleration.x < 0.1f)
            {
                isMoving = false;
                animator.SetBool("isMoving", isMoving);
            }



           /* Vector3 vel = bunnyRb.velocity;

           
            if (vel.magnitude > 0.01f || vel.magnitude < -0.01f)//if there is more input than these, set running animation
                {
                isMoving = true;
                animator.SetBool("isMoving", isMoving);
            }
            else if (vel.magnitude> -0.001f && vel.magnitude < 0.001f)//otherwise, if inputs are small, set idle animation
            {
                isMoving = false;
                animator.SetBool("isMoving", isMoving);
            }*/

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded==true)
            {
                animator.SetTrigger("Start");//plays the jump roll animation to start 
                jumpSound.Play();//plays the audio for jump
                bunnyRb.AddForce(new Vector3(0f, 750f, 0f), ForceMode.Force);//adds 750f upwards force
                //isGrounded = false;
            }

            //pos = gameObject.transform.position;//sets pos vector3 with the player's position
        }

        if(instructionsOn==false)
        {
            Animator animator = GetComponent<Animator>();//initializes the Animator component
            animator.SetBool("isMoving", false);//forces bunny to stop running on title screen
            
        }

       



       
    }

    /*IEnumerator offset()//a coroutine to update the position of the ground glow after X seconds
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);//waits for this many seconds before updating the new location of the light
            light.transform.position = pos - new Vector3(0, 0, 3);//offsets the Z transform by -3 of the light
            yield return new WaitForSeconds(0.35f);
            light1.transform.position = pos - new Vector3(0, 0, 3);
            yield return new WaitForSeconds(0.45f);
            light2.transform.position = pos - new Vector3(0, 0, 3);
            yield return new WaitForSeconds(0.55f);
            light3.transform.position = pos - new Vector3(0, 0, 3);
        }


    }*/

    public void ResetBunnyPos()
    {
        if (instructionsOn == false)//if instructions are off, reset the position of the bunny
        {
            gameObject.transform.position = originalPos;
            isGrounded = true;
        }

    }
}
