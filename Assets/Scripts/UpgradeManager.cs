using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]

public class UpgradeManager : MonoBehaviour {

    //public Material redMaterial;//gets the red material to be used with the Juggernaut upgrade
    //public TextMeshProUGUI tmpUpgradeText;//gets the UI text for upgrades
    public GameObject upgradeSound; //gets the prefab with the audio for the upgrade

    public float upgradeMoverSpeed;

    public GameObject upgradeParticleSystem;

   
   



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //checks to see if player is colliding with upgrade
        {


            //other.gameObject.GetComponent<Renderer>().material = redMaterial;//sets the player mesh to dark red to indicate upgrade received
            //other.gameObject.GetComponent<PlayerMovement>().upgradeJuggernaut = true; //sets the Player Movement boolean "upgrade juggernaut" to true
            other.gameObject.GetComponent<PlayerMovement>().JuggernautOn();//runs the Juggernaut public function
            GameObject upgradeParticles = Instantiate(upgradeParticleSystem, transform.position, transform.rotation) as GameObject;
            ParticleSystem parts = upgradeParticles.GetComponent<ParticleSystem>();
            float totalDuration = parts.duration + parts.startLifetime;
            Destroy(upgradeParticles, totalDuration);

            GameObject clone = Instantiate(upgradeSound, transform.position, transform.rotation);//spawns the audio prefab gameobject
            AudioSource cloneAudio = clone.GetComponent<AudioSource>();//gets the audio source from that game object
            cloneAudio.volume = PlayerPrefs.GetFloat("sfxVolume");//sets the audio to the sfx volume slider
            cloneAudio.Play();
            Destroy(clone, cloneAudio.clip.length + 0.1f);//destroys the audio game object after the audio clip finishes playing
            gameObject.SetActive(false);
            Destroy(gameObject); //destroys this object 
            

        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * upgradeMoverSpeed * Time.deltaTime, Space.World);//moves the upgrades upwards every frame
   }
}
