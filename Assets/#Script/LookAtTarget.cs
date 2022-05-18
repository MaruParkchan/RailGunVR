using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    private Transform target;

    private void OnEnable()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        transform.LookAt(target);
    }
}
