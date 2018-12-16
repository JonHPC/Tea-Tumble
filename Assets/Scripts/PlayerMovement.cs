
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]


public class PlayerMovement : MonoBehaviour
{

    public float tiltSpeed = 200f; //the speed the player moves
    public float jumpPower = 750f; //jump power
    public float collectibleScore = 1f;//initial points a collectible is worth

    public bool isGrounded = false; //checks to make sure the player is grounded to prevent jumping mid air
    public bool upgradeJuggernaut = false;// checks to see if the Juggernaut upgrade is active
    public bool upgradeSlow = false; //checks to see if the Slow upgrade is active
    public bool upgradeJackpot = false;//checks to see if Jackpot is active
    public bool superStatus = false;//checks to see if super is on
    public bool gamePaused = false;//checks to see if game is paused

    public int juggernautCharges = 5;//initial amount of obstacles/platforms the juggernaut can destroy
    public float slowDuration = 5f;//duration of the slow upgrade
    public float jackpotDuration = 10f;
    public float superCharge = 0f;//initial charge is 0 for super bar
    public float chargeAmount = 1f; //how much each collectible adds to the super charge bar

    public TextMeshProUGUI tmpScoreText;//UI
    public TextMeshProUGUI tmpUpgradeText;//UI
    public TextMeshProUGUI tmpHighScoreText;//UI
    public TextMeshProUGUI tmpGameOverScore;//game over screen score text
    public TextMeshProUGUI tmpGameOverNewHighScore; //game over screen, new high score text
    public Slider superBar;//references the super bar 
    public Button superButton;//references the super button

    public AudioSource jumpSound;
    public AudioSource pickUpSound;

    public GameObject audioManager;//music

    public Material defaultMaterial; //default player color
    public Material redMaterial;//juggernaut color
    public Material whiteMaterial; //default platform and wall color
    public Material purpleMaterial; //slow color

    public GameObject topWall; //top wall
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject deathWall;

    public GameObject collectible;//references Collectible prefab for the jackpot upgrade
    public GameObject collectibleJackpotParticleSystem;//references the particle system for when the collectible is hit when jackpot is active
    public GameObject superParticleSystem;//the particle animation that plays when the super is activated
    public GameObject superCollectibleParticleSystem;//references the particle system that plays when super is active
    public GameObject slowPanel;//a green panel that is applied to the whole screen when slow is activated

    public Material jackpotMaterial; //material for collectibles when Jackpot is active
    public Material tealMaterial; //default collectible1 material

    public CameraShake cameraShake;//references the camera shake script

    public GameManager gameManager;//references the Game Manager game object



    private Rigidbody playerRb; //referenced the Rigidbody component 
    private float score; //creates a float to store score values
    private float highScore;//creates a float to store high score values
    private float timer = 0f;
    private float jackpotTimer = 0f;
    private SpriteRenderer flipIt;//references the sprite renderer to flip the sprite when going left or right
    private Animator animator;//references the animator component to play the animations

    private Vector3 pos;//will store the player position
    public GameObject light;//the groundglow game object
    public GameObject light1;//ditto above
    public GameObject light2;//ditto above
    public GameObject light3;//ditto above







    private void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //gets the player's rigidbody

        jumpSound.volume = PlayerPrefs.GetFloat("sfxVolume"); //sets the jumpSound sfx to the player pref set on the menu
        pickUpSound.volume = PlayerPrefs.GetFloat("sfxVolume"); //ditto
        score = 0f; //initializes the score to be 0
        superCharge = 0f;//initializes the charge
        superStatus = false;//initializes the super status to off
        chargeAmount = 1f;//initializes charge amount
        isGrounded = false;
        gamePaused = false;//initializes
        collectibleScore = 1f;//initial score of collectibles
      
        tmpScoreText.text = "Score: " + score.ToString(); //initializes the score text
        tmpUpgradeText.text = "Upgrade: None"; //initializes the upgrade text

        highScore = PlayerPrefs.GetFloat("highScore", 0f); //gets the stored high score. Default value is 0 if no high score exists.
        tmpHighScoreText.text = "High Score: " + highScore.ToString(); // sets the high score text to the above value, from the previous high score

        tmpGameOverNewHighScore.gameObject.SetActive(false);// sets the game over screen new high score text off at the beginning of the round

        //collectible.gameObject.GetComponent<Renderer>().material = tealMaterial;//set collctibles to default teal material
        collectible.gameObject.GetComponentInChildren<ParticleSystem>().startColor = new Color(0, 238, 250, 116);//sets the PS_Sparkle particle system to default blue


        //transform.Find("DefaultTrail").GetComponentInChildren<TrailRenderer>().enabled = true; //initializes blue default trail
        //transform.Find("JuggernautTrail").GetComponentInChildren<TrailRenderer>().enabled = false;

        flipIt = GetComponent<SpriteRenderer>();//gets the spriterender component 
        animator = GetComponent<Animator>();//gets the animator component

        transform.Find("JuggernautParticles").GetComponentInChildren<ParticleSystem>().enableEmission = false;//initializes the juggernaut particles to have emission off


        StartCoroutine(offset());//starts the coroutine for the groundglow lag
    }



    /* private void FixedUpdate() //update per frame for physics
     {

         playerRb.AddForce(Input.acceleration.x * 200f, 0f, 0f);
         playerRb.AddForce(0f, -30f, 0f);
         //playerRb.AddForce(Input.acceleration * tiltSpeed); //adds the Tilt* the tileSpeed variable to add force to the player rigidbody

         /*if(Input.touchCount > 0 && isGrounded == true && Input.GetTouch(0).phase == TouchPhase.Began) //makes sure there is only one touch and the jump only occurrs if the object is grounded.
         {
             playerRb.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Force); //adds the jumpPower of force to the player rigidbody to make it jump
             jumpSound.Play(); //plays jumpSound
             //Debug.Log("IOS");//makes sure it shows IOS
         }*/

    /*  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) //makes sure there is only one touch and the jump only occurrs if the object is grounded.
      {
          playerRb.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Force); //adds the jumpPower of force to the player rigidbody to make it jump
          jumpSound.Play(); //plays jumpSound
          //Debug.Log("IOS");//makes sure it shows IOS
      }
  }*/





    void OnTriggerEnter (Collider other)
    {

        /*if(other.gameObject.CompareTag("CollectiblesCollider")) //obsolete code, there is no longer any objects with this tag
        {
            Destroy(other.gameObject);
        }*/

        if (other.gameObject.CompareTag("Collectibles")) //makes sure the object being collided with is a collectible
        {
            score += collectibleScore; //adds to the score. It is 5 because there is the collider and the actual model
            SetScoreText(); //runs the SetScoreText function

            if(superStatus==false)
            {
                superCharge += chargeAmount; //adds 1 to the super bar.
                superCharge = Mathf.Clamp(superCharge, 0f, 100f);//restricts the value of superCharge between 0 and 100
                SetSuperCharge();//runs the Set SuperCharge function
            }
            if(superStatus==true)
            {
                superCharge += chargeAmount; //adds 1 to the super bar
                superCharge = Mathf.Clamp(superCharge, 0f, 100f);//restricts the value of superCharge between 0 and 100
                SetSuperCharge();//runs the Set SuperCharge fn


                GameObject superCollectiblePS = Instantiate(superCollectibleParticleSystem, transform.position, transform.rotation);//if super is active, spawn this particle system when colliding with the collectibles
                ParticleSystem superParts = superCollectiblePS.GetComponent<ParticleSystem>();//creates a particle system from this component
                float superTotalDuration = superParts.duration + superParts.startLifetime;
                Destroy(superCollectiblePS, superTotalDuration);//destroyes this particle system after the duration is up
            }
           if(upgradeJackpot==true)
            {
                GameObject collectibleJPPS = Instantiate(collectibleJackpotParticleSystem, transform.position, transform.rotation) as GameObject;//if jackpot is active, spawn this particle system when colliding with a collectible
                ParticleSystem parts = collectibleJPPS.GetComponent<ParticleSystem>();//creates a particle system called "parts" that gets this component
                float totalDuration = parts.duration + parts.startLifetime;//calculates the total duration 
                Destroy(collectibleJPPS, totalDuration);//destroys this particle system when the duration is done
            
            }



            pickUpSound.Play();//plays a pickup sound
            other.gameObject.SetActive(false);//sets the game object to false
            Destroy(other.gameObject); // destroys the game object

        }


        if(other.gameObject.CompareTag("Obstacles") && upgradeJuggernaut == true)//if juggernaut upgrade is activated, destroys obstacles on contact
        {

            juggernautCharges -= 1; // subtracts a juggernaut charge each collision

            score += collectibleScore * 2f; //player gets collectibleScore points times 2 for destroying an obstacle
            SetScoreText();//updates the score

            superCharge += chargeAmount * 5f;//destroying an obstacles give 5% charge, or 15% when jackpot is also active
            SetSuperCharge();//runs the Set SuperCharge function

            other.gameObject.GetComponent<ObstacleManager>().ObstacleSound();//runs the obstacle sound function to make a sound
            other.gameObject.GetComponent<ObstacleManager>().ObstacleExplosion();//runs the obstacle explosion function to start particles
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);//destroys the obstacle
            StartCoroutine (cameraShake.Shake(0.15f, 0.4f));//starts the coroutine Shake() with the following inputs

            if (juggernautCharges <= 0)
            {
                JuggernautOff();//if the charges run out, run JuggernautOff function
            }
        }

       

    }

  


    void SetScoreText()
    {
        //updates the score text with the new score
        tmpScoreText.text = "Stars collected: " + score.ToString();//updates the UI score text
        tmpGameOverScore.text = "Stars collected: " + score.ToString();//updates the Game Over final score text

        if (score > highScore){ 
            PlayerPrefs.SetFloat("highScore", score); //updates the High Score player pref if the current score if higher
            tmpGameOverNewHighScore.gameObject.SetActive(true);//if there is a new high score, this message will show on the game over screen
        }


    }

    public void SetSuperCharge()//updates the slider with the current super charge value
    {
        superBar.value = superCharge;//sets the super bar to match the value 

        if(superCharge >= 100f)
        {
            //superButton.gameObject.SetActive(true);//if the super charge value is equal to 100, make the superButton visible
            deathWall.GetComponent<DeathScript>().fullCharge = true;//sets the fullCharge variable in the DeathScript to true to prevent super bar loss
            gameManager.GetComponent<GameManager>().SuperOn();//turns the Super on
        }

        if(superCharge<100f)
        {
            //superButton.gameObject.SetActive(false);// sets the button invisible when the value is under 100
            
        }
    }

   public void SuperParticles()
    {
        /*GameObject superParticles = Instantiate(superParticleSystem, new Vector3 (0f,0f,0f), transform.rotation) as GameObject;//spawns particle system when super is activated as a game object
        ParticleSystem parts = superParticles.GetComponent<ParticleSystem>();//gets the particle system component and places it onto "parts"
        float totalDuration = parts.duration + parts.startLifetime;//sets the total duration of the particle lifespan onto the variable
        Destroy(superParticles, 3.0f);//destroy the super particles after the total duration is up*/

        superParticleSystem.GetComponent<ParticleSystem>().enableEmission = true;//enables emission on the particles once super is active
    }

    public void SuperParticlesOff()
    {
        superParticleSystem.GetComponent<ParticleSystem>().enableEmission = false;//disables emission on the particles once super is active
    }

    public void JuggernautOn()
    {
        //tmpUpgradeText.text = "Upgrade: Juggernaut";
        juggernautCharges = 5; //sets the charges back to 5
        upgradeJuggernaut = true;
        //gameObject.GetComponent<Renderer>().material = redMaterial; // sets the player material to red to indicate juggernate upgrade is active
        //transform.Find("DefaultTrail").GetComponentInChildren<TrailRenderer>().enabled = false;
        //transform.Find("JuggernautTrail").GetComponentInChildren<TrailRenderer>().enabled = true;
        transform.Find("JuggernautParticles").GetComponentInChildren<ParticleSystem>().enableEmission = true;


    }

    public void JuggernautOff()
    {
        upgradeJuggernaut = false; //sets to false to turn off upgrade functions reference the colliders
        //tmpUpgradeText.text = "Upgrade: None"; // sets upgrade text to default
        //gameObject.GetComponent<Renderer>().material = defaultMaterial; // upon juggernaut buff ending, revert player material back to original
        //transform.Find("DefaultTrail").GetComponentInChildren<TrailRenderer>().enabled = true;
        //transform.Find("JuggernautTrail").GetComponentInChildren<TrailRenderer>().enabled = false;
        //gameObject.GetComponentInChildren<ParticleSystem>().maxParticles = juggernautCharges;
        transform.Find("JuggernautParticles").GetComponentInChildren<ParticleSystem>().enableEmission = false;
       

    }

    public void SlowOn()
    {
        upgradeSlow = true;
        timer = 0f;//resets the timer variable
        Time.timeScale = 0.7f; //slows the game timescale down when the Slow Platform upgrade is activated
        audioManager.GetComponent<AudioManager>().SlowVolume(); // runs the SlowVolume function 
        slowPanel.SetActive(true);//turns on the slowPanel to show the slow effect

        //topWall.gameObject.GetComponent<Renderer>().material = greenMaterial; //turns surrounding walls green when slow is activated
        //bottomWall.gameObject.GetComponent<Renderer>().material = greenMaterial;
       //rightWall.gameObject.GetComponent<Renderer>().material = greenMaterial;
        //leftWall.gameObject.GetComponent<Renderer>().material = greenMaterial;

       
    }

    private void Update()// runs the below code every frame. Player jumpthis is also used as a buff timer
    {
        if (gamePaused == false)//is game is paused, dont allow player inputs
        {


            playerRb.AddForce(Input.acceleration.x * tiltSpeed, 0f, 0f);//the input (-1 to 1) will be multiplied by 200f to control horizontal movement
            playerRb.AddForce(0f, -50f, 0f);//this is the constant downward "gravity" force applied to the player
            if (Input.acceleration.x < 0)//if device is tilted to the left, the sprite will be flipped on its x axis
            {

                flipIt.flipX = true;
            }
            else if (Input.acceleration.x >= 0)//otherwise the sprite will face to the right
            {
                flipIt.flipX = false;
            }
            animator.SetBool("isGrounded", isGrounded);//sets isGrounded bool in the animation to false, to roll the bunny

            //playerRb.AddForce(Input.acceleration * tiltSpeed); //adds the Tilt* the tileSpeed variable to add force to the player rigidbody

            /*if(Input.touchCount > 0 && isGrounded == true && Input.GetTouch(0).phase == TouchPhase.Began) //makes sure there is only one touch and the jump only occurrs if the object is grounded.
            {
                playerRb.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Force); //adds the jumpPower of force to the player rigidbody to make it jump
                jumpSound.Play(); //plays jumpSound
                //Debug.Log("IOS");//makes sure it shows IOS
            }*/

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) //causes the player to jump, add "&& isGrounded == true" if you want to make it only jump when grounded
            {
                playerRb.AddForce(new Vector3(0f, jumpPower, 0f), ForceMode.Force); //adds the jumpPower of force to the player rigidbody to make it jump
                jumpSound.Play(); //plays jumpSound

            }

        }


        if (upgradeSlow == false)
        {
            timer += Time.deltaTime; // sets this variable to store elapsed time

        }

        else if (upgradeSlow == true)
        {
            timer += Time.deltaTime;
            //tmpUpgradeText.text = "Time: " + timer.ToString();

            if (timer >= slowDuration) //checks to see when elapsed time has ran for 5 seconds
            {
                SlowOff();
            }
        }

         if (upgradeJackpot == false)//same as above, tracks the timer for the jackpot
        {
            jackpotTimer += Time.deltaTime;
        }

        else if (upgradeJackpot == true)//runs the below code when jackpot upgrade is active
        {
            jackpotTimer += Time.deltaTime; // starts this timer and stores it i nthe jackpotTimer variable


            if (jackpotTimer >= jackpotDuration) //checks to see when elapsed time has ran for 5 seconds
            {
                JackpotOff();//runs this function 
            }
        }

        pos = gameObject.transform.position;//sets pos vector3 with the player's position

    }



    IEnumerator offset()//a coroutine to update the position of the ground glow after X seconds
    {
        while(true)
        {
            yield return new WaitForSeconds(0.25f);//waits for this many seconds before updating the new location of the light
            light.transform.position = pos-new Vector3(0,0,3);//offsets the Z transform by -3 of the light
            yield return new WaitForSeconds(0.35f);
            light1.transform.position = pos - new Vector3(0, 0, 3);
            yield return new WaitForSeconds(0.45f);
            light2.transform.position = pos - new Vector3(0, 0, 3);
            yield return new WaitForSeconds(0.55f);
            light3.transform.position = pos - new Vector3(0, 0, 3);
        }
       

    }


    private void SlowOff()
    {
        upgradeSlow = false;
        Time.timeScale = 1f;//sets time back to normal 
        timer = 0f; //resets the timer for the next time
        //tmpUpgradeText.text = "Time: " + timer.ToString();
        audioManager.GetComponent<AudioManager>().UnslowVolume(); //runs the UnslowVolume() fn to turn volume back to normal
        slowPanel.SetActive(false);//turns off the slowPanel after the slow buff runs out
        //topWall.gameObject.GetComponent<Renderer>().material = whiteMaterial; //turns the walls back to their normal color
        //bottomWall.gameObject.GetComponent<Renderer>().material = whiteMaterial;
        //rightWall.gameObject.GetComponent<Renderer>().material = whiteMaterial;
        //leftWall.gameObject.GetComponent<Renderer>().material = whiteMaterial;

    }

    public void JackpotOn()
    {
        if (upgradeJackpot == false)//checks to makde sure the jackpot upgrade isnt already active, so it wont multiple the charge amount again
        {
            chargeAmount = chargeAmount * 3f;//triples the amount of super charge gained from each collectible
        }

        upgradeJackpot = true;
        jackpotTimer = 0f;

        collectible.gameObject.GetComponentInChildren<ParticleSystem>().startColor = new Color(191,156,0,116);//sets  the next spawned collectibles to yellow

        foreach (GameObject collectibleColor in GameObject.FindGameObjectsWithTag("Collectibles"))//finds all currently spawned collectible objects and turns them yellow
        {
            if(upgradeJackpot == true)//runs only if the jacpit upgrade is active
            {
                collectibleColor.gameObject.GetComponentInChildren<ParticleSystem>().startColor = new Color(191, 156, 0, 116);
            }
        }



        //collectibleScore = collectibleScore * 2f ; // doubles the score of the collectibles

        /*collectible.gameObject.GetComponent<Renderer>().material = jackpotMaterial; //sets the next spawned collectible objects to jackpot material

        foreach (GameObject collectibleColor in GameObject.FindGameObjectsWithTag("Collectibles")) //finds all currently spawned collectible objects and turns them to jackpot material
        {
            if (upgradeJackpot==true)//only runs if the jackpot upgrade is active
            {
                collectibleColor.gameObject.GetComponent<Renderer>().material = jackpotMaterial;
            }
        }*/
    }

    private void JackpotOff()
    {
        upgradeJackpot = false;
        jackpotTimer = 0f;//resets the jackpotTimer
        //collectibleScore = collectibleScore / 2f; //reverts the score to default
        chargeAmount = chargeAmount / 3f;//reverts the charge amount back to default


        collectible.gameObject.GetComponentInChildren<ParticleSystem>().startColor = new Color(0, 238, 250, 116);//sets  the next spawned collectibles to teal

        foreach (GameObject collectibleColor in GameObject.FindGameObjectsWithTag("Collectibles"))//finds all currently spawned collectible objects and turns them teal
        {
            if (upgradeJackpot == false)//runs only if the jacpit upgrade is off
            {
                collectibleColor.gameObject.GetComponentInChildren<ParticleSystem>().startColor = new Color(0,238,250,116);//turns all the above back to their default color
            }
        }

        /*collectible.gameObject.GetComponent<Renderer>().material = tealMaterial; //changes the prefab collectibles' material back to default AKA future spawns are back to normal color

        foreach (GameObject collectibleColor in GameObject.FindGameObjectsWithTag("Collectibles"))//finds all the current collectibles in the scene
        {
            if(upgradeJackpot == false)//checks to see if the jackpot upgrade is off
            {
                collectibleColor.gameObject.GetComponent<Renderer>().material = tealMaterial;//turns all the above game objects back to their default color
            }
        }*/
    }
}
