using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]

public class UpgradeManager : MonoBehaviour {

    //public Material redMaterial;//gets the red material to be used with the Juggernaut upgrade
    //public TextMeshProUGUI tmpUpgradeText;//gets the UI text for upgrades
    public GameObject upgradeSound; //gets the prefab with the audio for the upgrade

    public float upgradeMoverSpeed;//will sync with the platform speed variable in the gameManager script

    public GameObject upgradeParticleSystem;

    //public float desiredScale = 0.4f;//this will be used to scale down the strawberry
   

    void Start()
    {
       //desiredScale = 0.4f;//scales down the strawberry sprite upon spawning
    }



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
            Destroy(upgradeParticles, totalDuration);//destroys the particle system after the duration of the particle system animation

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
        //transform.localScale = new Vector3(desiredScale, desiredScale, desiredScale);//scales the strawberry sprite each frame
   }

    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime); // rotates the collectible to make it look cool
    }
}
