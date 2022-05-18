using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interaction : MonoBehaviour
{
    public NameType nameType;

    public void Explosion(float expPower, float radius) 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rigid = hit.GetComponent<Rigidbody>();
            if (rigid != null)
            {
                rigid.AddExplosionForce(expPower, transform.position, radius);
            }
        }
    }

    public void Explosion(float expPower, float radius, float upPower)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            //Enemy enemy = hit.transform.root.gameObject.GetComponent<Enemy>();

            //if (enemy != null)
            //{
            //    enemy.TakeDamage(1000, nameType);
            //}

            EnemyHit enemyHit = hit.transform.GetComponent<EnemyHit>();
            if(enemyHit != null)
            {
                enemyHit.TakeDamage(1000, nameType);
            }

            Rigidbody rigid = hit.GetComponent<Rigidbody>();

            if (rigid != null)
            {             
                rigid.AddExplosionForce(expPower, transform.position, radius, upPower, ForceMode.Impulse);
            }
        }       
    }
}
