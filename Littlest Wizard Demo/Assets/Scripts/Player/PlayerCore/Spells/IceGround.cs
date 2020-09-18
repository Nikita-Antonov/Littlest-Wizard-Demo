using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    public int iceDmg = 5;

    private void Update()
    {
        gameObject.transform.Rotate(Vector3.up * (40 * Time.deltaTime));            //Simple line to have a spinning effect when activated
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")                                        //Checks if the collided object is an enemy
        {
            other.gameObject.GetComponent<Enemy>().curHealth -= iceDmg;             //Does damage
            other.gameObject.GetComponent<Enemy>().isStunned = true;                //Stuns the Enemy
        }
    }
}
