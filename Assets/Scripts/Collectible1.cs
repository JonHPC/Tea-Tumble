using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible1 : MonoBehaviour {


    private AudioSource pickUpSound;

	// Use this for initialization
	void Start () {
        pickUpSound = GetComponent<AudioSource>();
        pickUpSound.volume = PlayerPrefs.GetFloat("sfxVolume");
	}
	
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            pickUpSound.Play();
        }
    }
}
