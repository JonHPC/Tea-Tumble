using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour {

    private AudioSource menuSound;

	// Use this for initialization
	void Start () {
        menuSound = GetComponent<AudioSource>();// initializes this audio component
	}
	
	// Update is called once per frame
	void Update () {

        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
        menuSound.volume = sfxVolume;
    }
}
