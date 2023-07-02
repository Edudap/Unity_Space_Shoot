using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //variable to help me set the speed at which the asteroid will rotate
    [SerializeField]
    private float rotateSpeed = 3.0f;

    //a prefab to be assigned in the unity editor
    [SerializeField]
    private GameObject explosionPrefab;

    private SpawnManager spawnManager;


    //this method finds the spawn manager object
    //retrieves the spawn manager component from it
    //stores it in the spawn manager variable 
    private void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>(); 
    }
    // Start is called before the first frame update
    

    // Update is called once per frame

    //rotates the asteroid along the z axis
    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    //this is to create the initial explosion when the laser comes in contact with asteroid
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.2f);
        }

    }

}

