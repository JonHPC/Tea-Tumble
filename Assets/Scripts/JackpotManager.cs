using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotManager : MonoBehaviour {

    public GameObject jackpotSound;

    public float jackpotMoverSpeed;//will be references from the GameManager to match this mover to the platform speed

    public GameObject jackpotParticleSystem;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //checks to see if player is colliding with upgrade
        {


            //other.gameObject.GetComponent<PlayerMovement>().upgradeJackpot = true; //sets the Player Movement boolean "upgrade slow" to true
            other.gameObject.GetComponent<PlayerMovement>().JackpotOn();//runs the SlowOn public function
            GameObject jackpotParticles = Instantiate(jackpotParticleSystem, transform.position, transform.rotation) as GameObject;//spawns the jackpotPS 
            ParticleSystem parts = jackpotParticles.GetComponent<ParticleSystem>();//gets the particle system component
            float totalDuration = parts.duration + parts.startLifetime;//sets the total duration to the duration of the particle system plus the start lifetime
            Destroy(jackpotParticles, totalDuration);//destroys the jackpotParticles gameobject after the total duration

            GameObject clone = Instantiate(jackpotSound, transform.position, transform.rotation);//spawns the audio prefab gameobject
            AudioSource cloneAudio = clone.GetComponent<AudioSource>();//gets the audio source from that game object
            cloneAudio.volume = PlayerPrefs.GetFloat("sfxVolume");//sets the audio to the sfx volume slider
            cloneAudio.Play();
            Destroy(clone, cloneAudio.clip.length + 0.1f);//destroys the audio game object after the audio clip finishes playing
            gameObject.SetActive(false);
            Destroy(gameObject); //destroys this object and its parent


        }
    }

   

    private void FixedUpdate()//runs this code every physics frame update
    {
        transform.Translate(Vector3.up * jackpotMoverSpeed * Time.deltaTime, Space.World);//moves the upgrades upwards every frame
    }

    void Update()
    {
        transform.Rotate(new Vector3(0,45, 0) * Time.deltaTime); // rotates the collectible to make it look cool
    }
}
