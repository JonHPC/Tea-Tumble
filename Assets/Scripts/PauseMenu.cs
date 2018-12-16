using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject player;//accesses the player for the script to disable inputs
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else{
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // disables the pause menu
        Time.timeScale = 1f; // reverts time to normal
        GameIsPaused = false;
        PlayerMovement playerPaused = player.GetComponent<PlayerMovement>();//accesses the playerMovement script 
        playerPaused.gamePaused = false;//sets gamePaused to false
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // brings up the Pause menu
        Time.timeScale = 0f; //freezes time 
        GameIsPaused = true;
        PlayerMovement playerPaused = player.GetComponent<PlayerMovement>();
        playerPaused.gamePaused = true;//sets gamePaused to True so there are no player inputs during the pause screen
    }

    public void LoadMenu()//goes back to the title main menu
    {
        Time.timeScale = 1f;//resumes time scale
        SceneManager.LoadScene("Title");//loads the Title scene
        //Debug.Log("Loading Menu...");
    }

    public void QuitGame()//quits the application
    {
        Application.Quit();
        //Debug.Log("Quitting Game..."); //just to make sure it works
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");//reloads the current Main scene
    }
}
