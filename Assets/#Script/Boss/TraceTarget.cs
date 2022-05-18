using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceTarget : Hp
{
    public float speed = 2.0f;
    public GameObject destroyEffect;
    private Transform target;
    private AudioSource audio;
    private Rigidbody rigid;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(isDie)
        {
            Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
            audio.PlayOneShot(audio.clip);
            Destroy(gameObject);
        }

        Trace();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.GetComponentInParent<Player>().TakeDamage(1);
            audio.PlayOneShot(audio.clip);
            Destroy(gameObject);
        }
    }

    private void Trace()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        Vector3 dir = (target.position - transform.position).normalized;       
        transform.position += dir * Time.deltaTime * speed;
    }

    public override void TakeDamage(int Damage)
    {
        if (isDie)
            return;

        hpValue -= Damage;

        if (hpValue <= 0)
            isDie = true;
    }

    public override void TakeDamage(int Damage, NameType type)
    {
        
    }
}
