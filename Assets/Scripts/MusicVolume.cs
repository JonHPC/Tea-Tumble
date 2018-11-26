using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour {

    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioSource music;
    public AudioSource sfx;
	

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f); //stores the slider value to a player pref tagged "MusicVolume"
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f); //sets the default voluem to these to 50%
    }

	// Update is called once per frame
	void Update () 
    {
        music.volume = musicSlider.value; //updates the music volume to the slider volume
        sfx.volume = sfxSlider.value;
	}

    public void VolumePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume", music.volume); //sets the tag "MusicVolume" to the music.volume variable
        PlayerPrefs.SetFloat("sfxVolume",sfx.volume);
    }
}
