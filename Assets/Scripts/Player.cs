using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //the speed and speedMultiplier are variables that help me control the speed of my player. 
    [SerializeField]
    private float speed = 3.5f;

    [SerializeField]
    private float speedMultiplier = 2;


    //a variable to hold input for the horizontal axis
    public float horizontalInput;


    //prefab for the laser main weapon 
    [SerializeField]
    private GameObject laserPrefab;

    //prefab for the tripleshot power up
    [SerializeField]
    private GameObject TripleshotPrefab;

    //the variables FireRate and CanFire help me control when the player can fire the laser
    [SerializeField]
    private float FireRate = 0.5f;

    [SerializeField]
    private float CanFire = -1f;

    //this is just the number of lives the player has 
    [SerializeField]
    private int lives = 3;

    //this references the spawn manager of my game 
    private SpawnManager spawnManager;

    //these three boolean variable help me keep track if any of the power ups
    //are active
    [SerializeField]
    private bool isTripleShotactive = false;

    private bool isSpeedBoostActive = false;

    private bool isShieldsActive = false;

    //these game objects help me display the Shield and the Engines
    [SerializeField]
    private GameObject shieldVisualizer;

    [SerializeField]
    private GameObject rightEngine;

    [SerializeField]
    private GameObject leftEngine;



    //this is to help keep track of our score in the game 
    [SerializeField]
    private int score;

    //a reference to the game UImanager
    private UImanager Uimanager;

    //laserSound and audioSource handle the effects when a laser is fired
    [SerializeField]
    private AudioClip laserSound;

    
    private AudioSource audioSource;

    //with this method the player starting position is set
    //the spawn manager and UI manager are found
    //the audiosource component is set up 
    void Start()
    {
        
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        Uimanager = GameObject.Find("Canvas").GetComponent<UImanager>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is null!!");
        }

        else
        {
            audioSource.clip = laserSound;
        }
    }

    //this method is for handling the player movement and
    //for checking if the player is pressing the space key
    //and checking if the player can or not fire a laser 
    void Update()
    {
        CalculateMovement();

        

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > CanFire)
        {
            FireLaser();
        }

       
        
    }

    //this method help us fire the laser
    //it checks if the triple shot is active
    //if it is not active, it will shot a normal laser 
    void FireLaser()
    {
        CanFire = Time.time + FireRate;

        if (isTripleShotactive == true)
        {
            Instantiate(TripleshotPrefab, transform.position, Quaternion.identity); 
        }

        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
        }

        audioSource.Play();
    }
    //this is called when the player takes damage
    //if the shield is active, it will deactivate it
    //it decreased the players life by one
    //it checks if the player has less than one life
    //if the player has no lives left it will call the death method from spawn manager
    public void Damage()
    {

        if (isShieldsActive == true)
        {
            isShieldsActive = false;
            shieldVisualizer.SetActive(false);
            return;
        }

       

        if (lives > 0) 
        {
            lives -= 1;

         if(lives == 2)
        {
                leftEngine.SetActive(true);
            }

        else if (lives == 1)
            {
                rightEngine.SetActive(true);
            }
        }

        Uimanager.updatelives(lives);

        if (lives < 1)
        {
            spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }

    }

    //this is for handling the players movement
    //it loops the player around the x axis
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

      

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        
        transform.Translate(direction * speed * Time.deltaTime);

      

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, 3.8f, 0);
        } 
       

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    //these next three methods activate and deactive the power ups 
    public void TripleShotActive()
    {
        isTripleShotactive = true;
        StartCoroutine(TripleShotPowerDownRoutine()); 
        
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotactive = false;
    }

    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void ShieldsActive()
    {
        isShieldsActive = true;
        shieldVisualizer.SetActive(true);
    }

    //this is method is called to add points to the player score
    public void AddScore(int points)
    {
        score += points;
        Uimanager.UpdateScore(score);
    }
}
