using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    [Header("Health and Mana")]
    public int maxPlayerHealth = 100;
    public int curPlayerHealth;
    public int maxMana = 50;
    public int curMana;
    [Space]

    [Header("Dmg objects")]
    public Transform projectileStart;           //The location from which the projectile is fired
    public GameObject projectilePrefab;         //The prefab that is used as a projectile
    public GameObject healObject;               //The reffrence to the heal object;
    public GameObject StunObj;                  //The refrence to the stun object
    public GameObject AOEObj;                   //The refrence to the aoe object
    [Space]

    [Header("Basic Attack Timer")]              //This is the Timer for the basic attack, which is the lighning
    public float attackTimer;
    public float attackCooldown = 1f;
    [Space]

    [Header("Ability Cooldown")]                //This is the timer for all the spell cooldowns
    public float timer;
    public float cooldown = 2.5f;
    [Space]

    [Header("Ability Duration Timers")]         //This is the timer for the duration that any Spell is enabled
    public float massTimer;
    public float massColldown = 2f;
    public bool timerOn;
    [Space]

    [Header("Refrence to 'Bars'")]             //These are the reffrence to the sliders which are responsible for the Health, Mana and Cooldown Bar
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider spellCooldownSlider;
    [Space]

    [Header("Dialouge Trigger")]
    public bool inDialouge;

    private float falloff = 10f;              //This value is the falloff of the projectile, so that the player cant cheese the enemy

    void Start()
    {
        //Initiating all the valuse
        curPlayerHealth = maxPlayerHealth;
        curMana = maxMana;
        timer = cooldown;
        attackTimer = attackCooldown;
        massTimer = massColldown;
        timerOn = false;
        inDialouge = false;

        //These three are respongible for setting the Health, Mana and Cooldown bars
        healthSlider.maxValue = maxPlayerHealth;
        healthSlider.value = maxPlayerHealth;

        manaSlider.maxValue = maxMana;
        manaSlider.value = maxMana;

        spellCooldownSlider.maxValue = cooldown;
        spellCooldownSlider.value = timer;
    }

    void Update()
    {

        //These Update the 'Bar' Values in the UI
        healthSlider.value = curPlayerHealth;
        manaSlider.value = curMana;
        spellCooldownSlider.value = timer;

        //Timer counts down till an ability can be used
        if (!inDialouge)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            if (timer < 0)
                timer = 0;
            if (timer == 0)
            {
                //After timer reaches 0, a function can be called, and the timer will reset
                HealPlayer();
                Stun();
                AOE();
            }

            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;
            if (attackTimer < 0)
                attackTimer = 0;
            if (attackTimer == 0)
            {
                FireProjectile();
            }
        }

        /*
         * This timer controlls how long the duration of the AOE/Stun ability is on
         * This method kinda brute forces it, but i wanted both abilities to be tied to 
         * a collider so that i can bypass using raycasts
         */
        if (timerOn == true)
        {
            massTimer -= Time.deltaTime;
            if (massTimer <= 0)
                massTimer = 0;
            if (massTimer == 0)
            {
                AOEObj.SetActive(false);
                StunObj.SetActive(false);
                healObject.SetActive(false);
                timer = cooldown;
                massTimer = massColldown;
                timerOn = false;
            }
        }

        //Checks if the health exeeds or is below "0" then sets it apropriatly
        if (curPlayerHealth > maxPlayerHealth)
            curPlayerHealth = maxPlayerHealth;
        if (curPlayerHealth < 0)
            PlayerDeath();

        //Checks if the mana exeeds or is below "0" then sets it apropriatly
        if (curMana > maxMana)
            curMana = maxMana;
        if (curMana < 0)
            curMana = 0;
    }

    //Main code responsible for firing a "Lighning Bold" to smite down enemies
    void FireProjectile()
    {

        if (Input.GetMouseButtonDown(0))
        {
            GameObject lightningProjectile = Instantiate(projectilePrefab) as GameObject;                               //Instanciates the projectile GameObject
            lightningProjectile.transform.position = projectileStart.position + gameObject.transform.forward;           //Setting the position of the projectile
            Rigidbody rb = lightningProjectile.GetComponent<Rigidbody>();                                               //Getting reffrence to the projectiles Rigidbody component
            lightningProjectile.transform.rotation = projectileStart.rotation;                                          //Sets the rotation of the projectile so that it doenst look wonky when spawning
            rb.velocity = projectileStart.forward * 50;                                                                 //Sets the velocity of the projectile

            attackTimer = attackCooldown;                                                                               //Ressets the Attack Timer
            Destroy(lightningProjectile, falloff);                                                                      //Destroys the projectile after a given time (falloff)
            
        }
    }

    //Function to heal the player, self explanitory
    void HealPlayer()
    {
        int manaCost = 6;
        if (Input.GetKeyDown(KeyCode.Alpha1) && timerOn != true && curMana >= manaCost)     //If key pressed and still have enough mana
        {
            healObject.SetActive(true);
            healObject.gameObject.transform.Rotate(Vector3.up * (80 * Time.deltaTime));     //Rotates the Heal Object
            curPlayerHealth += 10;                                                          //Heal player
            curMana -= manaCost;                                                            //Remove mana 
            timerOn = true;
         
        }
    }

    //Function that handles the Stun object
    void Stun()
    {
        int manaCost = 6;
        if (Input.GetKeyDown(KeyCode.Alpha2) && timerOn != true && curMana >= manaCost)
        {
            StunObj.SetActive(true);            //Sets the object to be active, for more info on how the dmg is calculated/stun works, look at the "IceGround" Script
            curMana -= manaCost;
            timerOn = true;
         
        }
    }

    //Function that handles the AOE object
    void AOE()
    {
        int manaCost = 6;
        if (Input.GetKeyDown(KeyCode.Alpha3) && timerOn != true && curMana >= manaCost)
        {
            AOEObj.SetActive(true);         //Sets the object to be active, for more info on how the dmg is calculated, look at the "FireBlast" Script
            curMana -= manaCost;
            timerOn = true;
            
        }
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);         //Ressets the Level Upon Player Death
    }

}
