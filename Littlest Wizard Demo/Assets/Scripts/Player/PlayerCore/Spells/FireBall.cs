using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    int FireballDMG = 6;           //Dmg of the Enemy
    float falloff = 5;               //The falloff for the projectile

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")       //Checks if the collider hit the player
        {
            other.gameObject.GetComponent<PlayerLogic>().curPlayerHealth -= FireballDMG;            //Does damage to the player
            Destroy(gameObject, falloff);                                                           //Removes Object from Game world
        }
    }
}
