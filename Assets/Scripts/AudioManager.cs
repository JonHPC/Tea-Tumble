using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {


    public AudioSource music;



	// Use this for initialization
	void Start () 
    {
        music.volume = PlayerPrefs.GetFloat("MusicVolume"); //gets the player pref volume that was set in the main menu

	}
	
    public void PauseLowerVolume()
    {
        music.volume = music.volume / 3f; //lowers the music volume when the pause button is pressed
    }

    public void ResumeRaiseVolume()
    {
        music.volume = music.volume * 3f; //raised the music volume back to what it was once game resumes
    }

    public void SlowVolume() //runs when the Slow Platform upgrade is activated
    {
        music.pitch = 0.7f; //adjusts the BGM pitch with relation to the timescale change
    }

    public void UnslowVolume()
    {
        music.pitch = 1f; //reverts audio to normal when Slow Platform upgrade ends
    }
}
