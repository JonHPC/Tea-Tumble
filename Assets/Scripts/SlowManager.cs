using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]

public class SlowManager : MonoBehaviour {

    //public Material greenMaterial;//gets the green material used with the Slow collectible

    public GameObject slowSound; //gets the prefab with the audio for the slow upgrade

    //public GameObject wall;

    public GameObject slowParticleSystem;//refences the slowPS prefab



    public float slowMoverSpeed;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //checks to see if player is colliding with upgrade
        {

            //wall.gameObject.GetComponent<Renderer>().material = greenMaterial;// sets the walls to green when slow is active
            //other.gameObject.GetComponent<Renderer>().material = redMaterial;//sets the player mesh to dark red to indicate upgrade received
            //other.gameObject.GetComponent<PlayerMovement>().upgradeSlow = true; //sets the Player Movement boolean "upgrade slow" to true
            other.gameObject.GetComponent<PlayerMovement>().SlowOn();//runs the SlowOn public function
            GameObject slowParticles = Instantiate(slowParticleSystem, transform.position, transform.rotation) as GameObject;
            ParticleSystem parts = slowParticles.GetComponent<ParticleSystem>();
            float totalDuration = parts.duration + parts.startLifetime;
            Destroy(slowParticles, totalDuration);

            GameObject clone = Instantiate(slowSound, transform.position, transform.rotation);//spawns the audio prefab gameobject
            AudioSource cloneAudio = clone.GetComponent<AudioSource>();//gets the audio source from that game object
            cloneAudio.volume = PlayerPrefs.GetFloat("sfxVolume");//sets the audio to the sfx volume slider
            cloneAudio.Play();
            Destroy(clone, cloneAudio.clip.length + 0.1f);//destroys the audio game object after the audio clip finishes playing
            gameObject.SetActive(false);
            Destroy(gameObject); //destroys this object and its parent


        }
    }

   private void FixedUpdate()
    {
        transform.Translate(Vector3.up * slowMoverSpeed * Time.deltaTime, Space.World);//moves the upgrades upwards every frame
    }


}
