using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    private float bulletDamage;
    private Vector3 kickbackDir;
    private float kickBackForce;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    public void InitializeBullet(float damage, Vector3 kickBackDir, float kickBackForce)
    {
        bulletDamage = damage;
        this.kickbackDir = kickBackDir;
        this.kickBackForce = kickBackForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(bulletDamage);

            if(enemyHealth.health <= 0 && !enemyHealth.isDead)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(kickbackDir * kickBackForce, ForceMode.Impulse);
                enemyHealth.isDead = true;
            }

        }
        Destroy(this.gameObject);
    }
}
