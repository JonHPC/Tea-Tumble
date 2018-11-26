using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleManager : MonoBehaviour {

    public GameObject obstacleExplosion;// references the game object with the obstacle explosion audio source

    public float obstacleMoverSpeed;

    public GameObject obstacleExplosionParticles;


    public void ObstacleSound()//is called by PlayerMovement to play the obstacle destruction sound
    {

        GameObject clone = Instantiate(obstacleExplosion, transform.position, transform.rotation); //spawn a copy of the empty game object with the obstacle explosion audio source
        AudioSource cloneAudio = clone.GetComponent<AudioSource>(); //gets the audio component of that clone
        cloneAudio.volume = PlayerPrefs.GetFloat("sfxVolume"); //sets the audio to the sfxvolume slider
        cloneAudio.Play(); //plays the audio clip
        Destroy(clone, cloneAudio.clip.length + 0.1f); //destroys the game object after the clip finishes playing

    }
    
    public void ObstacleExplosion()//is called by PlayerMovement to play explosion particles
    {
        //GameObject clone1 = Instantiate(obstacleExplosionParticles, transform.position, transform.rotation);//spawns a copy of the empty game object with the explosion particle system
        //ParticleSystem cloneParticles = clone1.GetComponent<ParticleSystem>();//gets the particle system component of that clone
        //cloneParticles.Play();//plays the particles for the explosion
        //ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;//gets the emission module of the particle system
        // em.enabled = true;//enables the emission module

        GameObject obstacleParticles = Instantiate(obstacleExplosionParticles, transform.position, transform.rotation) as GameObject;//spawns the particle system and plays it
        ParticleSystem parts = obstacleParticles.GetComponent<ParticleSystem>();//gets the component ParticleSystem and puts it onto "parts"
        float totalDuration = parts.duration + parts.startLifetime;//sets the totalDuration
        Destroy(obstacleParticles, totalDuration);//destroys the obstacleParticles gameobject after totalDuration
    }



    private void FixedUpdate()
    {

        transform.Translate(Vector3.up * obstacleMoverSpeed * Time.deltaTime, Space.World);//moves the obstacles upwards every frame
    }

    private void OnTriggerEnter(Collider other) // destroys any collectibles that spawn in the same spot as an obstacle
    {
        if(other.gameObject.CompareTag("Collectibles"))
        {
            Destroy(other.gameObject);
        }

    }
}
