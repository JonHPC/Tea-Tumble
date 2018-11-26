using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Animator bunnyRun;//references the bunny run animation
    public Rigidbody bunnyPhysics;// references the 2d rigidbody component of bunnyrun



    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        bunnyRun.SetTrigger("Start");//makes the bunny switch to the roll animation
        bunnyPhysics.isKinematic = false;//turns on physics on the rigidbody2d
        bunnyPhysics.AddForce(0f, 1000f,0f);//simulates a jump by adding force
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

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }



}
