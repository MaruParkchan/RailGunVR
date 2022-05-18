using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Hp
{
    public float radius = 0;
    public float explosion = 0;
    public float upPower = 0;
    public Interaction interaction;
    public GameObject effect;

    public override void TakeDamage(int Damage)
    {
        hpValue -= Damage;

        if (hpValue <= 0)
            Booom();
    }

    public override void TakeDamage(int Damage, NameType type)
    {
       
    }

    private void Booom()
    {
        GameObject clone =  Instantiate(effect);

        clone.transform.position = this.transform.position;
        interaction.Explosion(radius, explosion, upPower);
        Destroy(gameObject);
    }
}
