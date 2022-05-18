using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavi : MonoBehaviour
{
    private Enemy enemy;
    private NavMeshAgent navi;
    private Transform target;
    private float distance;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        navi = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        navi.SetDestination(target.position);
        NavMeshSetUp();
    }

    private void Update()
    {
        if (enemy.isDie == false)
            distance = navi.remainingDistance;     
    }

    public void TargetTrace() // 타겟 추적
    {
        navi.SetDestination(target.position);
    }

    public void NavMeshAgentEnabled() // 네비 끄기
    {
        navi.enabled = false;
    }

    public void Trace() // Walk달릴때 지정된 Speed값 
    {
        navi.speed = enemy.Speed;
    }

    public void StopTrace() // 공격시 or Walk가 아닐시 작동 
    {
        navi.speed = 0;
    }

    public float TargetDistance()
    {
        return distance;
    }

    private void NavMeshSetUp()
    {
        navi.stoppingDistance = enemy.AttackRange;
    }
    
    public void Distance(float stopDistanceValue)
    {
        navi.stoppingDistance = stopDistanceValue;
    }
}
