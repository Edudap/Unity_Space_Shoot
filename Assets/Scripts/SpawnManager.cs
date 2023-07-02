using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //the enemy prefab is the template game object for the enemy
    [SerializeField]
    private GameObject enemyPrefab;

    //the enemy container will parent all the instantiations of the enemy
    [SerializeField]
    private GameObject enemyContainer;

    //this array of objects will contain the power ups 
    [SerializeField]
    private GameObject[] powerUps;

    //a boolean variable that controls the spawning of enemies and power ups
    private bool stopSpawning = false;
    

    //this function start the enemy spawning and the power ups coroutines
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemeyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
    //this coroutine spawns an enemy at random place in x, between -8 and 8
    //and in y7, every 5 seconds. 

    IEnumerator SpawnEnemeyRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform; 
            yield return new WaitForSeconds(5.0f);
        }
    }

    //this coroutine spawns a power up in a similar way than the enemy coroutine
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    // this functions sets stop spawning to true when the game is over 
    public void onPlayerDeath()
    {
        stopSpawning = true;
    }


}
