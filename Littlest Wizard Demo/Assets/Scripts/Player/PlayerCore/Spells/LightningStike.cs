using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStike : MonoBehaviour
{

    PlayerLogic playerLogic;
    int lightningDmg = 15;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")                                            //Checks if the collided object is a enemy
        {
            other.gameObject.GetComponent<Enemy>().curHealth -= lightningDmg;           //Deals damage
            Destroy(gameObject);                                                        //Removes the Object from game world
        }
    }

}
