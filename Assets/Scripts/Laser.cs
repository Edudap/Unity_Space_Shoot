using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //this variable control how fast the laser moves
    [SerializeField]
    private float speed = 8.0f;


    void Start()
    {
        
    }

    //this method moves the laser object towards the top of the screen
    //the speed depends on the speed variable
    //the code checks if the laser has a parent object
    //if it does the parent object and all objects that come after are destroyed
    //the code then destroy the laser object itself 
    void Update()
    {
        
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        
        if (transform.position.y > 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
