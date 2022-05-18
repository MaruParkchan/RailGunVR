using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Animator ani;
    public bool isStart = false;

    private void Update()
    {
        ani.SetBool("Start",isStart);
    }

    private void OnTriggerEnter(Collider other)
    {
        isStart = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isStart = false;
    }
}
