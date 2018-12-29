using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[System.Serializable]

public class GameManager : MonoBehaviour {


    //public float spawnDelay = 0f; //seconds until first platform is spawned
    public float spawnRate;//Inital time between platforms spawned
    public Transform PlatformSpawn; // references the SpawnPoint game object
    public GameObject collectible1; //the basic cube collectible
    public GameObject obstacle1; //a basic obstacle block
    public GameObject upgradeBlock; //the juggernaut upgrade block
    public GameObject slowBlock; //the slow upgrade block
    public GameObject jackpotBlock; //the jackpot upgrade block
    //public GameObject landingColor;//access the sprite
    //public Light lt;//replaces the above sprite with a point light
    //public int ltCount;//tracks the number of lights spawned
    //public Transform landingColorSpawn;//access the spawn point of the landing color sprite
    public GameObject[] platforms; //creates an array of platforms that can be spawned
    public Transform[] collectibleSpawn; //an array of collectible spawn points, also used to spawn obstacles
    public Transform[] upgradeSpawn;//an array of upgrade spawn points
    public int collectiblePerPlatform; //determines how many collectibles are spawned with each platform
    public TextMeshProUGUI tmpLevelText; //references the level text
    public TextMeshProUGUI tmpLevelPopup; //references the level popup
    public float platformSpeed;//speed the platforms move at once spawned

    public bool superStatus;// checks to see if the super is active or not
    public float superDuration; //initial super duration is 5 seconds

    public GameObject superParticles;//used to turn the super PS sound on and off

    public GameObject player; //references the player game object
    public GameObject deathWall;//references the death wall game object

    private float elapsedTime = 4f; //how much time initially needed to pass before each platform spawns
    private int levelCount = 1;
    private int toNextLevel = 0;//tracks number of platforms spawned, level increases when certain limit is hit
    private int toNextLevelCount = 9; //The initial amount of platforms that is spawned per level
    private float timer;//timer for the super buff
    

    [SerializeField] private int range = 10; // size of array of spawn points
    List<int> list = new List<int>(); //creates a new, empty list of integers
    private global::System.Object superParticlesSound;

    private void Start()
    {

        FillList();// runs the FillList function to create a list of random numbers used to spawn collectibles
        tmpLevelText.text = "Level: " + levelCount.ToString();
        superDuration = 5f;//initializes
        platformSpeed = 3f;//initializes the platform speed
        spawnRate = 3f;//initializes the spawn rate
        superStatus = false;//initializes the super status
        AudioSource superParticlesSound = superParticles.GetComponent<AudioSource>();//attaches the audio source to this variable
       
    }

    void FillList() // fills the newly created list with numbers from 0 to the range value
    {
        for (int i = 0; i < range; i++)
        {
            list.Add(i); 
        }
    }

    int GetNonRepeatRandom() // a function that returns a random integer from the list
    {
        if (list.Count == 0)
        {
            //return -1; // Maybe you want to refill
            FillList();
        }
        int rand = Random.Range(0, list.Count);
        int value = list[rand];
        list.RemoveAt(rand);
        return value;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;//sets elapsedTime to tick up every second

        if(superStatus==true)
        {
            timer += Time.deltaTime;
            player.GetComponent<PlayerMovement>().superCharge -= (Time.deltaTime * 20f);//subtracts from the super charge variable at a rate of 20% per second
            player.GetComponent<PlayerMovement>().SetSuperCharge();//updates the super charge bar with the new value
           /*if (timer >= superDuration) //checks to see when elapsed time has ran for 5 seconds
            {
                SuperOff();//deactivates the super charge
            }*/
            if(player.GetComponent<PlayerMovement>().superCharge<=0f)//if the super charge bar gets to or under 0% as it is going down, turn off the super charge.
            {
                SuperOff();
            }
        }

        if(elapsedTime > spawnRate)
        { //runs the below code every spawnRate seconds
            elapsedTime = 0f; //sets back to 0 for another spawnRate seconds
            //Debug.Log("Spawned"); //just checking to make sure fn runs
            toNextLevel++; //adds 1 to the toNextLevel counter, level will increase once this increases large enough

            int platformIndex = Random.Range(0, platforms.Length); //chooses a random entry in the array between 0 and the length of the array
            GameObject platform = Instantiate(platforms[platformIndex], PlatformSpawn.position, PlatformSpawn.rotation);//spawns a randon platform at the PlatformSpawn

            PlatformMover updateSpeed = platform.GetComponent<PlatformMover>(); //gets the platformmover from the recently spawned object and puts it only a variable
            updateSpeed.platformMoverSpeed = platformSpeed;//updates the speed of the spawned platform with the new platformSpeed


            if (superStatus==false)//if super is off, spawn random amts of collectibles per platform
            {
                collectiblePerPlatform = Random.Range(2, 10); //every platform spawns random amounts of collectibles
                for (int i = 0; i < collectiblePerPlatform; i++) //spawns x collectibles per set of platforms in a random spawn location
                {
                    //int collectibleSpawnIndex = Random.Range(0, collectibleSpawn.Length);//finds a random spawn point
                    int collectibleSpawnIndex = GetNonRepeatRandom();// spawns only on unique spawn points to prevent overlapping
                    GameObject collectible = Instantiate(collectible1, collectibleSpawn[collectibleSpawnIndex].position, collectibleSpawn[collectibleSpawnIndex].rotation);//instantiates a collectible at the above random point

                    CollectibleManager updateCollectibleSpeed = collectible.GetComponentInChildren<CollectibleManager>();//updates the speed of the collectible upwards
                    updateCollectibleSpeed.collectibleMoverSpeed = platformSpeed;//updates the speed of the spawned collectible with the current platformSpeed


                }
            }

            else if (superStatus == true)//if super is on, max out the amount of collectibles spawned per platform
            {
                collectiblePerPlatform = 10;
                for (int i = 0; i < collectiblePerPlatform;i++)
                {
                    GameObject collectible = Instantiate(collectible1, collectibleSpawn[i].position, collectibleSpawn[i].rotation); //spawns one collectible from each spawn point in the array
                    CollectibleManager updateCollectibleSpeed = collectible.GetComponentInChildren<CollectibleManager>();
                    updateCollectibleSpeed.collectibleMoverSpeed = platformSpeed;//updates the speed of the spawned collectible with the current platformSpeed
                }

               
            }




            if (Random.Range(1, 15) == 1)//chance to spawn a jackpot block per platform spawn
            {
                int jackpotSpawnIndex = Random.Range(0, upgradeSpawn.Length); //finds a random spawn point within the array of upgrade spawn points 
                GameObject jackpot = Instantiate(jackpotBlock, upgradeSpawn[jackpotSpawnIndex].position, upgradeSpawn[jackpotSpawnIndex].rotation); //creates an upgrade at that random spawn point

                JackpotManager updateJackpotSpeed = jackpot.GetComponent<JackpotManager>();//accesses the JackpotManager script in the newly spawned object
                updateJackpotSpeed.jackpotMoverSpeed = platformSpeed; //sets the new objects upward move speed to match the current platformSpeed

            }

            if (levelCount > 1)//starts spawning obstacles and juggernaut upgrades once it hits level 2
            {
                int obstacleSpawnIndex = Random.Range(1, collectibleSpawn.Length - 1); //spawns one obstacle block at a random spawn location per platform. Does not include the far left or right spawn points
                int randomObstacle = Random.Range(1, 3); //picks a random int 1-3
                if (randomObstacle == 2 && superStatus==false)//there is a 1/3 chance to spawn an obstacle, also, will not spawn obstacles if super is active
                {
                    GameObject obstacle = Instantiate(obstacle1, (collectibleSpawn[obstacleSpawnIndex].position-new Vector3(0f,0.75f,0f)), Quaternion.Euler(0f,253f,0f));//spawns an onstable with a negative 0.75Y to account for ObstacleCrystals weird origin

                    ObstacleManager updateObstacleSpeed = obstacle.GetComponent<ObstacleManager>();//gets the ObstacleManager script from the spawn obstacle
                    updateObstacleSpeed.obstacleMoverSpeed = platformSpeed;//updates the speed of the obstacle to match the platforms}

                }

                if (Random.Range(1, 20) == 1)// chance to spawn a juggernaut block per platform spawn 
                {
                    int upgradeSpawnIndex = Random.Range(0, upgradeSpawn.Length); //finds a random spawn point within the array of upgrade spawn points
                    GameObject upgrade = Instantiate(upgradeBlock, upgradeSpawn[upgradeSpawnIndex].position, upgradeSpawn[upgradeSpawnIndex].rotation); //creates an upgrade at that random spawn point

                    UpgradeManager updateUpgradeSpeed = upgrade.GetComponent<UpgradeManager>();//accesses the UpgradeManager script in the newly spawned object
                    updateUpgradeSpeed.upgradeMoverSpeed = platformSpeed;//sets the new objects upward move speed to match the current platformSpeed

                }
            }


            if (levelCount >2)//stars spawning slow upgrades at level 3
            {
                if (Random.Range(1, 25) == 1)//5% chance to spawn a slow upgrade block per platform spawn
                {
                    int slowSpawnIndex = Random.Range(0, upgradeSpawn.Length); //finds a random spawn point within the array of upgrade spawn points
                    GameObject slow = Instantiate(slowBlock, upgradeSpawn[slowSpawnIndex].position, upgradeSpawn[slowSpawnIndex].rotation);//creates an upgrade at that random spawn point

                    SlowManager updateSlowSpeed = slow.GetComponent<SlowManager>();//accesses the SlowManager script in the newly spawned object
                    updateSlowSpeed.slowMoverSpeed = platformSpeed; //sets the new objects upward move speed to match the current platformSpeed

                }
            }



        }

       


        if(toNextLevel>toNextLevelCount) //once 10 platforms have been spawned, level count will go up by 1
        {
            toNextLevel = 0; //resets to 0
            toNextLevelCount += 2; //each level spawns two more platforms before the next level
            levelCount++; // increases the level count

            if (levelCount < 3) // for the first 3 levels, do this code
            {
                spawnRate -= 1f;
                //spawnRate = Mathf.Clamp(spawnRate, 1f, 4f); //decreases the time between spawning platforms aka spawn more platforms
                platformSpeed += 1f;//every level increase ups the platformMoverSpeed
                elapsedTime -= 5f; // puts a 5 second delay in between platform spawns between levels
            }

            else if (levelCount >= 3)//after the above, do this code
            {
                spawnRate -= 0.15f; // increates the spawn rate
                spawnRate = Mathf.Clamp(spawnRate, 0.45f, 3f); //restricts the spawn rate between 0.6 seconds and 3 seconds
                platformSpeed += 0.5f;
                platformSpeed = Mathf.Clamp(platformSpeed, 1.5f, 7.5f); // restricts platform speeds
                elapsedTime -= 3f;
            }

            if (elapsedTime < 0f)
            {
                tmpLevelPopup.gameObject.SetActive(true); //when level transitions, set the popup to be true for 5 seconds (based off the above code
            }

        }

        if (elapsedTime > 0f)
        {
            tmpLevelPopup.gameObject.SetActive(false);//turns off the level popup
        }

        tmpLevelText.text = "LEVEL " + levelCount.ToString(); //constantly updates the level text with the current levelCount.
        tmpLevelPopup.text = "LEVEL " + levelCount.ToString();// updates the level popup with the current level

       /*if(player.GetComponent<PlayerMovement>().isGrounded == true && elapsedTime > 0.1f)//creates spot lights if the player is grounded
        {
            Instantiate(lt, landingColorSpawn.position, landingColorSpawn.rotation);//if the player is contacting a platform, spawn the landing color sprite
            lt.GetComponent<GroundGlow>().ltSpeed = platformSpeed;//updates the LandingMover script with the current platform speed
            //ltCount += 1;//adds 1 to the ltCount everytime a lt is spawned


        }*/

       
    }

    public void SuperOn()
    {
        superStatus = true;//sets the super on
        player.GetComponent<PlayerMovement>().superStatus = true;
        deathWall.GetComponent<DeathScript>().superStatus = true;//prevents the death wall from subtracting super charge
        timer = 0f;//sets the super timer to 0 to initialize
        player.GetComponent<PlayerMovement>().SuperParticles();//runs the SuperParticles function
        AudioSource superParticlesSound = superParticles.GetComponent<AudioSource>();//attaches the audio source to this variable
        superParticlesSound.volume = 1;//resets the volume back to one
        superParticlesSound.Play();//plays the audio source on SuperPS2 when super is active

    }

    public void SuperOff()
    {
        superStatus = false; //sets the super off
        deathWall.GetComponent<DeathScript>().fullCharge = false;//sets the Death Script fullCharge to false so the death wall can subtract super bar again
        player.GetComponent<PlayerMovement>().superStatus = false;
        deathWall.GetComponent<DeathScript>().superStatus = false;//re-enables death wall ability to subtract super charge
        timer = 0f;
        player.GetComponent<PlayerMovement>().SuperParticlesOff();//runs the SuperParticlesOff function
        AudioSource superParticlesSound = superParticles.GetComponent<AudioSource>();//attaches the audio source to this variable
        //superParticlesSound.Stop();//stops this audio when super is done
        StartCoroutine(FadeSound());
    }

    IEnumerator FadeSound()
    {
        AudioSource superParticlesSound = superParticles.GetComponent<AudioSource>();//attaches the audio source to this variable
        while (superParticlesSound.volume>0.01f)
        {
            superParticlesSound.volume -= Time.deltaTime / 4f;//fades the volume over 4 seconds
            yield return null;
        }
    }
}
