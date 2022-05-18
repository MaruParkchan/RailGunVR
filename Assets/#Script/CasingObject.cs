using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingObject : MonoBehaviour
{
    [SerializeField] private float casingSpin = 1.0f;
    private Rigidbody rigid;
    private AudioSource audio;

    public void Init(Vector3 direction)
    {
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        rigid.velocity = new Vector3(direction.x, 1.0f, direction.z);
        rigid.angularVelocity = new Vector3(Random.Range(-casingSpin, casingSpin), Random.Range(-casingSpin, casingSpin), Random.Range(-casingSpin, casingSpin));
    }

    private void OnCollisionEnter(Collision collision)
    {
        audio.Play();
    }
}
