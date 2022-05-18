using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TTTEESST : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] NavMeshAgent nn;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        nn.SetDestination(target.position);

        Debug.Log(Vector3.Distance(transform.position, target.position));
    }
    private void Update()
    {
        Debug.Log("" + nn.remainingDistance);
        // nn.destination = target
    }
}
