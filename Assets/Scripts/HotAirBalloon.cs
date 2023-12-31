using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloon : Enemy

{
    private MoneyCollection moneyCollection;
    private PlayerHealth playerHealth;

    private GameObject plane;

    private Weapon weapon;
    public LaunchProjectile launchProjectile;
    private EnemySounds enemySounds;
    public Color color1 = Color.blue;
    public Color color2 = Color.red;

    [Header("Hot Air Balloon V1")]
    [Tooltip("Moves up/down")]
    public bool IsThisAHotAirBalloonV1;
    [Header("Hot Air Balloon V2")]
    [Tooltip("Stays still and drops cannonballs")]
    public bool IsThisAHotAirBalloonV2;
    [Header("Hot Air Balloon V3")]
    [Tooltip("Moves up/down and drops cannonballs")]
    public bool IsThisAHotAirBalloonV3;

    public float movementDuration = 5f;
    public override void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
            DropLoot();
            Destroy(gameObject);
        }
    }

    public override void Die()
    {
        enemySounds.PlayHotAirBalloonSound();

    }

    public override void DropLoot()
    {
        moneyCollection.moneyCollected += loot;
    }

    public override void InflictContactDamage(float amount)
    {
        playerHealth.TakeDamage(amount);
    }

    public override void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InflictContactDamage(contactDamage);
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            // weapon damage
            TakeDamage(weapon.projectile_Damage);
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            // Move up for movementDuration seconds
            for (float t = 0; t < movementDuration; t += Time.deltaTime)
            {
                transform.position += Vector3.up * movementSpeed * Time.deltaTime;
                yield return null;
            }

            // Move down for 3 seconds
            for (float t = 0; t < movementDuration; t += Time.deltaTime)
            {
                transform.position -= Vector3.up * movementSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }

    private void Awake()
    {
        plane = GameObject.FindGameObjectWithTag("Player");
        moneyCollection = plane.GetComponent<MoneyCollection>();
        playerHealth = plane.GetComponent<PlayerHealth>();
        weapon = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
        enemySounds = GameObject.FindWithTag("SoundManager").GetComponent<EnemySounds>();
    }

    public void Start()
    {
        if (IsThisAHotAirBalloonV1)
        {
            health = 1;
            loot = 10;
            contactDamage = 50;
            //start moving up/down
            StartCoroutine(Move());
        }
        else if (IsThisAHotAirBalloonV2)
        {
            health = 1;
            loot = 20;
            contactDamage = 50;
            movementSpeed = 5;

            //start dropping cannonballs
            launchProjectile.StartDroppinCannonBalls();
        }
        else if (IsThisAHotAirBalloonV3)
        {
            health = 1;
            loot = 20;
            contactDamage = 50;
            movementSpeed = 5;
            //start dropping cannonballs
            launchProjectile.StartDroppinCannonBalls();
            //start moving up/down
            StartCoroutine(Move());
        }
    }

    public void Update()
    {
        if (transform.position.x < plane.transform.position.x - 50)
        {
            Destroy(gameObject);
        }
    }
}