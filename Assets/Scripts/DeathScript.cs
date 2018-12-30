using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DeathScript : MonoBehaviour {


    public GameObject gameOverMenu;//references the game over menu game object
    public GameObject retryButton;//references coroutine, reappears after 3 seconds
    public GameObject menuButton;//reference coroutine, reappears after 3 seconds
   //public GameObject quitButton;//references coroutine,reappears after 3 seconds
    public GameObject player; //references the player game object
    public bool playerDead;
    public bool superStatus = false;//checks to see whether the super is activated or not
    public bool fullCharge = false;//checks to see if the super bar is full

    public GameObject playerDeathParticles;//references the player death particle system

    public float enoughStars;//checks to see if the player has gained enough stars to turn off ads permanently
    public float totalStars;//the current amount of total stars

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
        enoughStars = 49999f;//once the player gets this many total stars, ads will be disabled forever
        totalStars = PlayerPrefs.GetFloat("totalStars", 0f);//sets the totalStars to the current amount of totalStars

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            explosion.Play();//plays the explosion audio clip
            Instantiate(playerDeathParticles, player.GetComponent<Transform>().position, player.GetComponent<Transform>().rotation);

            other.gameObject.SetActive(false);
            //Destroy(other.gameObject); //destroys the player on collision
            gameOverMenu.SetActive(true);//sets the game over menu to be active
            retryButton.SetActive(false);//initially sets to inactive
            menuButton.SetActive(false);//initially sets to inactive
            //quitButton.SetActive(false);//initially sets to inactive
            playerDead = true;//sets this boolean to true
            //Time.timeScale = 0f;//stops time upon death
            totalStars = PlayerPrefs.GetFloat("totalStars", 0f);//sets the totalStars to the current amount of totalStars
            //Advertisement.Show("video");//plays a video ad after player dies
            if (enoughStars>=totalStars)//plays ads only if the total star count is less than the "enoughStars" amount
            {
                ShowAd(3f);//shows the ad after 3 second delay
            }
            else
            {
                ShowNoAd(3f);//stops time after 3 seconds
            }


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

        else if (other.gameObject.CompareTag("LandingColor"))
        {
            Destroy(other.gameObject);//destroys the landing color sprites
        }
    }

    IEnumerator delayedShowAd(float delay)
    {
        yield return new WaitForSeconds(delay);//waits for delay seconds
        Advertisement.Show("video");//shows the ad after delay seconds
        Time.timeScale = 0f;//stops time after ad appears
        retryButton.SetActive(true);//reappears after 3 seconds
        menuButton.SetActive(true);//reappears after 3 seconds
        //quitButton.SetActive(true);//reappears after 3 seconds
    }
    void ShowAd(float delay)
    {
        StartCoroutine(delayedShowAd(delay)); //starts the coroutine for the delayedShowAd
    }

    IEnumerator delayedShowNoAd(float delay)
    {
        yield return new WaitForSeconds(delay);//waits for delay seconds
        Time.timeScale = 0f;//stops time after delay seconds
        retryButton.SetActive(true);//reappears after 3 seconds
        menuButton.SetActive(true);//reappears after 3 seconds
        //quitButton.SetActive(true);//reappears after 3 seconds
    }

    void ShowNoAd(float delay)
    {
        StartCoroutine(delayedShowNoAd(delay));//stars the coroutine for the delayedShowNoAd
    }
}
