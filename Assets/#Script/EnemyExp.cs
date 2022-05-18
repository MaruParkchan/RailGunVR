using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {

        }
    }
}
