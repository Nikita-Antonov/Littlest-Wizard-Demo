using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*
     * This script is soly responsible for keeing track of the player
     * The instance can later be refferenced though any scrip if it is needed
     */

    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject player;
}
