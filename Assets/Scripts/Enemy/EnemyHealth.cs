using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector] public float health;
    [SerializeField] private float maxHealth;
    RagdollManager ragdollManager;

    [HideInInspector] public bool isDead;


    private void Start()
    {
        health = maxHealth;
        ragdollManager = GetComponent<RagdollManager>();
    }

    public void TakeDamage(float damage)
    {
        if(health > 0)
        {
            health -= damage;
            Debug.Log("Hit");
            if (health <= 0)
            {
                EnemyDeath();
            }
        }
        

    }

    void EnemyDeath()
    {
        Debug.Log("Enemy Died");
        ragdollManager.TriggerRagdoll();

    }
}
