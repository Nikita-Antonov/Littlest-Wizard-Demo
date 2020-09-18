using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickups : MonoBehaviour
{
    public bool Healing;            //Checks if its a healing poation, if not then its a mana potion
    public int amountToAdd;         //Amount of health/mana tha will be picked up on entering the collider of the potion

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")       //Makes sure that whatever entered the hitbox of the potion is a player
        {
            if(Healing)
                other.gameObject.GetComponent<PlayerLogic>().curPlayerHealth += amountToAdd;            //If healing is selected, then health is added
            if (!Healing)
                other.gameObject.GetComponent<PlayerLogic>().curMana += amountToAdd;                    //If healing is NOT selected, then mana is added
        }

        Destroy(gameObject);            //Destroys the potion form the game world, once the potion was "Consumed"
    }

}
