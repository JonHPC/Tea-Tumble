using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {


    public AudioSource music;//references the BGM
    public Button muteButton;//references the mute button
    public Button unmuteButton;//references the unmute button
    public int muted;//tracks whether or not mute is active



	// Use this for initialization
	void Start () 
    {
        music.volume = PlayerPrefs.GetFloat("MusicVolume"); //gets the player pref volume that was set in the main menu


        muted = PlayerPrefs.GetInt("muted", 0);//takes the player pref for muted and sets it to false by default

        if (muted == 1)//if this is muted at beginning of the game, unmute it
        {
            AudioListener.pause = !AudioListener.pause;//mutes the audio like a toggle with the Mute Button
            PlayerPrefs.SetInt("muted", 0);//turns audio back on at start
        }

    }

    void Update()
    {
        muted = PlayerPrefs.GetInt("muted", 0);//keeps updating this int
        if (muted == 0)//if game is not muted, do this
        {
            muteButton.gameObject.SetActive(true);//keeps the mute button active
            unmuteButton.gameObject.SetActive(false);//hides the unmute button
        }
        else if (muted == 1)
        {
            muteButton.gameObject.SetActive(false);//hidse the mute button
            unmuteButton.gameObject.SetActive(true);//shows the unmute button

        }
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

    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;//mutes the audio like a toggle with the Mute Button
        PlayerPrefs.SetInt("muted", 1);//sets to muted
    }

    public void UnMute()
    {
        AudioListener.pause = !AudioListener.pause;//mutes the audio like a toggle with the Mute Button
        PlayerPrefs.SetInt("muted", 0);//sets this to not muted
    }
}
