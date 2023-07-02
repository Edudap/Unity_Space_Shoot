using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // A simple private variable to help determine the speed of my enemy
    //It is saved as a serialize field so I can access it directly from unity
    [SerializeField]
    private float speed = 4.0f;

    // a private variable that will reference the object of my player
    private Player player;

    //a private variable to help me handle the animations of the enemy
    private Animator anim;

    //Private variable to assist me by playing an explosion audio when my enemy is destroyed
    private AudioSource audioSource;

    //a method to find the player in the scene and get the player component.
    //also gets the animator and the audiosource component of the enemy
    //if they are not found it will log an error, so I can know when something went wrong

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        audioSource = GetComponent<AudioSource>();

        if (player == null)
        {
            Debug.LogError("The Player is NULL!");
        }

        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Anim is NULL!");
        }
    }

    //this method moves the enemy downward at the speed defined by the speed variable
    //if my enemy moves below y -5, it will reposition it randomly between -8 and 8 x and 7y

    void Update()
    {
       
        transform.Translate(Vector3.down * speed * Time.deltaTime);

       

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8);
            transform.position = new Vector3( randomX, 7, 0);
        }
        

    }

    //this method is called when the 2d collider of the enemy comes into contact with other 2d collider
    //if the enemy collides with the player it calles the "damage function" of the player
    //if the enemy collides with the laser it plays the death animation and destroy enemy and laser

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }


            anim.SetTrigger("OnEnemyDeath");
            speed = 0;

            audioSource.Play();

            Destroy(this.gameObject, 2.8f);

            
        }

        

        if (other.tag == "Laser")
        {
            anim.SetTrigger("OnEnemyDeath");
            speed = 0;

            Destroy(this.gameObject, 2.8f);

            if(player != null)
            {
                player.AddScore(10);
            }

            audioSource.Play();

            Destroy(other.gameObject);

           
        }
    }
}
