using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {

    public Animator bunnyRun;//references the bunny run animation
    public Rigidbody bunnyPhysics;// references the 2d rigidbody component of bunnyrun
    public AudioSource menuSound;//references the audio source that will play when menu items are pressed

    public bool instructionsOn;//used for the instructions tutorial

    public GameObject bunnyRunScript;//accesses the bunny run object
    
   

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        AudioSource startAudio = GetComponent<AudioSource>();
        startAudio.Play();
        //bunnyRun.SetTrigger("Start");//makes the bunny switch to the roll animation
        //bunnyRun.SetBool("isGrounded", false);//same as above
        bunnyPhysics.isKinematic = false;//turns on physics on the rigidbody2d

        bunnyPhysics.AddForce(250f, 750f,0f);//simulates a jump by adding force
        StartGame(1f);//starts the game after 1 second
    }

    IEnumerator delayedStart(float delay)//used to load the game after delay seconds using a coroutine
    {
        yield return new WaitForSeconds(delay);
        //SceneManager.LoadScene("Main");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void StartGame(float delay)
    {
        StartCoroutine(delayedStart(delay)); //starts the coroutine for the delayedShowAd
    }

    public void QuitGame()//quits the application
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void MenuSound()
    {
        menuSound.Play();//plays a menu sound whenever a button is pressed, except for the Start button
    }

    public void InstructionsStart()
    {
        instructionsOn = true;// allows tapping the screen to make the bunny jump
        TitleBunny titleBunny = bunnyRunScript.GetComponent<TitleBunny>();
        titleBunny.instructionsOn = true;
        Animator bunnyAnimator = bunnyRunScript.GetComponent<Animator>();//gets the animator component
        bunnyAnimator.SetBool("instructionsOn", true);
    }

   

    public void InstructionsEnd()
    {
        instructionsOn = false;//when this fn runs, turns off the ability to jump when tapping the screen
        TitleBunny titleBunny = bunnyRunScript.GetComponent<TitleBunny>();//gets the TitleBunny script component
        titleBunny.instructionsOn = false;//sets the instructionsOn boolean to false
        Animator bunnyAnimator = bunnyRunScript.GetComponent<Animator>();//gets the animator component
        titleBunny.isGrounded = true;//set this boolean to true
        bunnyAnimator.SetBool("isGrounded", true);//sets the animation
        bunnyAnimator.SetBool("instructionsOn", false);
        //bunnyAnimator.SetTrigger("Reset");

    }
}
