using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for managing the explosions of the game 
public class Explosionscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
