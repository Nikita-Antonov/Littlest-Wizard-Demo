using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //Enemy Health Values
    [Header("Enemy Health Values")]
    public int maxHealth = 40;
    public int curHealth;
    [Space]
    
    //Sees if the enemy is stunned
    [Header("Stun Check")]
    public bool isStunned;
    [Space]

    //Cooldowns for the Ability
    [Header("Ability Cooldown")]
    public float lookRadius = 10f;
    public float timer;
    public float cooldown = 2;
    [Space]

    //Timer for the stun
    [Header("Stun Timers")]
    public float stunTimer;
    public float stunCooldown = 5;
    [Space]

    Transform target;
    NavMeshAgent navAgent;

    //Game Object reffrence
    [Header("Used GameObjects")]
    public GameObject ProjectilePoint;
    public GameObject prefab;
    public GameObject stunSign;
    [Space]

    //Reffrence to the Health Bar
    [Header("Health bar Reffrence")]
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;

        timer = cooldown;
        stunTimer = stunCooldown;

        isStunned = false;
        target = PlayerManager.Instance.player.transform;
        navAgent = GetComponent<NavMeshAgent>();

        stunSign.SetActive(false);

        healthSlider.maxValue = maxHealth;
        healthSlider.value = curHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = curHealth;

        StunTimer();

        float distance = Vector3.Distance(target.position, transform.position);

        if (!isStunned)
        {
            if (distance <= lookRadius)
            {
                navAgent.SetDestination(target.position);
                lookRadius = 70;

                if (distance <= navAgent.stoppingDistance)
                {
                    FacePlayer();
                }

                if (distance <= 20)
                    AttackPlayer();
            }

        }

        if (curHealth <= 0)
            OnDeath();
    }

    //This Function is responisble for facing enemy at the player
    void FacePlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;                          //Gets the direction to the player
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    //Gets the rotation of the enemy to the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);    //Rotates the enemy
    }

    //This function allows the enemy to attack the player by flinging a fireball at him
    void AttackPlayer()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0;
        if (timer == 0)
        {
            GameObject lightningProjectile = Instantiate(prefab) as GameObject;
            lightningProjectile.transform.position = ProjectilePoint.transform.position + gameObject.transform.transform.forward * 2;
            Rigidbody rb = lightningProjectile.GetComponent<Rigidbody>();
            lightningProjectile.transform.rotation = ProjectilePoint.transform.rotation;
            rb.velocity = ProjectilePoint.transform.forward * 100;

            timer = cooldown;
        }
    }

    //This is a Gizmo function allowing anyone in the edditor to see the view radius of the enemy
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    //Function responsible to handle the time the enemy is stuned for
    void StunTimer()
    {
        if (isStunned == true)
        {
            stunSign.SetActive(true);
            if (stunTimer > 0)
                stunTimer -= Time.deltaTime;
            if (stunTimer < 0)
                stunTimer = 0;
            if (stunTimer == 0)
            {
                isStunned = false;
                stunTimer = stunCooldown;
                stunSign.SetActive(false);
            }
        }
    }

    //Upon the death of the Enemy, it removes it from thje game world
    void OnDeath()
    {
        Destroy(gameObject);
    }
}
