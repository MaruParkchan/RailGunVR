using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NameType { Player, Enemy, Explosion, }
public abstract class Hp : MonoBehaviour
{
    [SerializeField] public int hpValue; // HP
    [SerializeField] private NameType nameType; // 오브젝트 타입 / 이름 
                     public NameType NameType { get { return nameType; } }


    public bool isDie = false;

    public abstract void TakeDamage(int Damage);

    public abstract void TakeDamage(int Damage, NameType type);

}
