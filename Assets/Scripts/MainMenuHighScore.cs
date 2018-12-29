using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuHighScore : MonoBehaviour {

    
    public TextMeshProUGUI highScoreText;// displays the current high score on the main menu

    private float highScore; //variable for storing the retreived highScore Player Pref

	// Use this for initialization
	void Start () 
    {
        highScore = PlayerPrefs.GetFloat("highScore", 0f); //gets the high score player pref and stores it in this variable

        highScoreText.text = "HIGH SCORE: " + highScore.ToString();//sets the high score TMP to the current high score stored in the highScore variable

    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetFloat("highScore", 0f);
        highScore = 0f;
        highScoreText.text = "HIGH SCORE: " + highScore.ToString();
    }


}
