using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //the speed at which the power up moves 
    [SerializeField]
    private float speed = 3.0f;

    //the integer ID corresponding to each power up
    //0 is triple shot
    //1 is speed booster
    //2 is shield
    [SerializeField]
    private int powerupID;

    //this is an audioclip that plays when a player picks up a power up
    [SerializeField]
    private AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    //this method moves the power up downwards at the specified speed
    // if the power up moves below -4.5 y then it is destroyed 
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        } 
    }
    //this method is called when another object collides with the power up
    // if it collides with the player it checks which power up was collected
    //triggers the power up and destroys the obect
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")


        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(clip, transform.position);

            if (player != null)
            {

                if (powerupID == 0)
                {
                   player.TripleShotActive();
                }
                else if (powerupID == 1)
                {
                    Debug.Log("Collected Speed Boost");
                }
                else if (powerupID == 2)
                {
                    Debug.Log("Shields Collected");
                }

                switch(powerupID)
                {
                    case 0:
                            player.TripleShotActive();
                        break;
                    case 1:
                            player.SpeedBoostActive();
                        break;

                    case 2:
                        player.ShieldsActive();
                        break;

                     default:
                        Debug.Log("Default Value");
                        break;

                }
                
            }

            Destroy(this.gameObject);
        }
    }
}
