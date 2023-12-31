using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float spawnLocation;
    public float movementSpeed;
    public float contactDamage;
    public int loot;

    public abstract void Attack();
    public abstract void DropLoot();
    public abstract void Die();
    public abstract void InflictContactDamage(float amount);
    public abstract void TakeDamage(float amount);

}
