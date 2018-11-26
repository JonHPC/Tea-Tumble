using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DeathScript : MonoBehaviour {


    public GameObject gameOverMenu;//references the game over menu game object
    public GameObject player; //references the player game object
    public bool playerDead;
    public bool superStatus = false;//checks to see whether the super is activated or not
    public bool fullCharge = false;//checks to see if the super bar is full

    public GameObject playerDeathParticles;//references the player death particle system

    private AudioSource explosion;
    private float subtractSuper = 1f;//subtracts this amount from the super bar


    void Start()
    {
        explosion = GetComponent<AudioSource>();//gets the audio component and sets it to the explosion AudioSource variable.
        explosion.volume = PlayerPrefs.GetFloat("sfxVolume");//sets the volume of the explosion variable to the playerpref
        playerDead = false;//initially sets player to not dead
        superStatus = false;
        fullCharge = false;
        Time.timeScale = 1f;//initializes the timescale to 1.

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            explosion.Play();//plays the explosion audio clip
            Instantiate(playerDeathParticles, player.GetComponent<Transform>().position, player.GetComponent<Transform>().rotation);
            Destroy(other.gameObject); //destroys the player on collision
            gameOverMenu.SetActive(true);//sets the game over menu to be active
            playerDead = true;//sets this boolean to true
            //Time.timeScale = 0f;//stops time upon death

            //Advertisement.Show("video");//plays a video ad after player dies
            ShowAd(2f);//shows the add after 2 second delay

        }
        
        else if(other.gameObject.CompareTag("Platform"))
        {
            Destroy(other.gameObject); //destroys platforms on collision

        }

        else if(other.gameObject.CompareTag("Collectibles"))
        {
            Destroy(other.gameObject);//destroys collectibles on collision

            if(superStatus==false && fullCharge==false)
            {
                player.GetComponent<PlayerMovement>().superCharge -= subtractSuper; //subtracts this amount from the super bar when a collectible is destroyed by the wall
                player.GetComponent<PlayerMovement>().superCharge = Mathf.Clamp(player.GetComponent<PlayerMovement>().superCharge, 0f, 100f);// restricts the value of the super charge to 0-100 to prevent negative values.
                player.GetComponent<PlayerMovement>().SetSuperCharge();//runs the SetSuperCharge function to update the bar.
            }
           

        }

        else if (other.gameObject.CompareTag("Upgrades"))
        {
            Destroy(other.gameObject);//destroys upgrades on collision
        }

        else if (other.gameObject.CompareTag("Obstacles"))
        {
            Destroy(other.gameObject);//destroys obstacles on collision
        }
    }

    IEnumerator delayedShowAd(float delay)
    {
        yield return new WaitForSeconds(delay);
        Advertisement.Show("video");//shows the ad after delay seconds
        Time.timeScale = 0f;//stops time after ad appears
    }
    void ShowAd(float delay)
    {
        StartCoroutine(delayedShowAd(delay)); //starts the coroutine for the delayedShowAd
    }
}
