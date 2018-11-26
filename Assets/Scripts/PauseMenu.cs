using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
	
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
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // brings up the Pause menu
        Time.timeScale = 0f; //freezes time 
        GameIsPaused = true;
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
