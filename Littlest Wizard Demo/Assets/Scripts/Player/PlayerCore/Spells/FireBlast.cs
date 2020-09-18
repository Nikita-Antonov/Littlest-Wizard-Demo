using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlast : MonoBehaviour
{
    //Dammage Variable;
    public int fireDmg = 5;
    private void Update()
    {
        gameObject.transform.Rotate(Vector3.up * (40 * Time.deltaTime));        //Simple line to have a spinning effect when activated
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")                                     //Checks if the collided object is an enemy
        {
            other.gameObject.GetComponent<Enemy>().curHealth -= fireDmg;        //Does damage
        }
    }

}
