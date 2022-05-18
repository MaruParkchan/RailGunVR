using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    public bool isDie;
    public bool isShield;

    private void Update()
    {
        isDie = enemy.isDie;
        isShield = enemy.isShield;
    }

    public void TakeDamage(int damage)
    {
        enemy.TakeDamage(damage);
    }

    public void TakeDamage(int damage, NameType type)
    {
        enemy.TakeDamage(damage, type);
    }
}
