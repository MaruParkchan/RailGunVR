using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateType { idle, downState, UpState, wallWakeUp , SuicideBombing }
// idle 기본 서있는상태 , walk 걸어다니는상태 , downState 엎드린상태 , upState 누워있는상태 , wallWakeUp 땅에서 나온상태
// Idle , DownState, 
public enum EnemyWalk { One, Two, Three, Four, Random }
public enum EnemyAttack { One, Two, Three, Random }
public enum EnemyDies { One, Two, Three, Four, Random}

public class EnemyAnimator : MonoBehaviour
{
    public EnemyStateType fireState;
    [SerializeField] private EnemyWalk walkState;
    [SerializeField] private EnemyAttack attackState;
    [SerializeField] private EnemyDies dieState;
    private Animator ani;
    private Enemy enemy;
    private EnemyNavi enemyNavi;
    private int firstStateIndex = 0; // 기본 Idle -> 0 / 땅에서 나오는거 1 / 누워있는거 2 / 엎드려있는거 3 /
    private int idleIndex;
    private int walkIndex;
    private int attackIndex;
    private int dieIndex;
    private bool isAttack = false;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyNavi = GetComponent<EnemyNavi>();
        idleIndex = Random.Range(0, 3); // 3개 
        EnemyFirstStateInit();
        EnemyWalkInit();
        EnemyAttackInit();
        EnemyDiesInit();
        AnimatiorParametersInitialization();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            SetTrigger("NextState");

        if (ani.GetCurrentAnimatorStateInfo(0).IsTag("Walk")) // 달리기일때 speed = value
        {
            enemyNavi.Trace();
            enemy.isShield = false;
        } 
        else
        {
            enemyNavi.StopTrace();
        }

        if (ani.GetCurrentAnimatorStateInfo(0).IsName("WalkIndex") == true) // Walk 진입시 타격할수있게 그전엔 데미지 안들어가게 설정
            enemy.isShield = false;

        AttackRange();
    }

    private void AnimatiorParametersInitialization() // 파라미터 값 
    {
        SetInteger("FirstState", firstStateIndex);
        SetInteger("IdleIndex", idleIndex);
        SetInteger("WalkIndex", walkIndex);
        SetInteger("AttackIndex", attackIndex);
        SetInteger("DieIndex", dieIndex);
    }

    private void EnemyFirstStateInit()
    {
        switch (fireState)
        {
            case EnemyStateType.idle:
                firstStateIndex = 0;
                break;
            case EnemyStateType.wallWakeUp:
                firstStateIndex = 1;
                break;
            case EnemyStateType.UpState:
                firstStateIndex = 2;
                break;
            case EnemyStateType.downState:
                firstStateIndex = 3;
                break;
            case EnemyStateType.SuicideBombing:
                firstStateIndex = 4;
                break;
        }
    } // 처음 애니메이션 결정 - 땅속 , 누워있는거, 기본 등

    private void EnemyWalkInit()
    {
        switch(walkState)
        {
            case EnemyWalk.One:
                walkIndex = 0;
                break;

            case EnemyWalk.Two:
                walkIndex = 1;
                break;

            case EnemyWalk.Three:
                walkIndex = 2;
                break;

            case EnemyWalk.Four:
                walkIndex = 3;
                break;

            case EnemyWalk.Random:
                walkIndex = Random.Range(0, 4);
                break;
        }
    } // 애니메이션 달리기 모션 결정

    private void EnemyAttackInit()
    {
        switch (attackState)
        {
            case EnemyAttack.One:
                attackIndex = 0;
                break;

            case EnemyAttack.Two:
                attackIndex = 1;
                break;

            case EnemyAttack.Three:
                attackIndex = 2;
                break;

            case EnemyAttack.Random:
                attackIndex = Random.Range(0, 3);
                break;
        }
    } // 애니메이션 공격 모션 결정

    private void EnemyDiesInit()
    {
        switch (dieState)
        {
            case EnemyDies.One:
                dieIndex = 0;
                break;

            case EnemyDies.Two:
                dieIndex = 1;
                break;

            case EnemyDies.Three:
                dieIndex = 2;
                break;

            case EnemyDies.Four:
                dieIndex = 3;
                break;

            case EnemyDies.Random:
                dieIndex = Random.Range(0, 4);
                break;
        }
    }
    public void EnabledAnimatior() // 애니메이터 비활성화
    {
        ani.enabled = false;
    }

    public void EnemyMoveStart() // 적 움직임
    {
        SetTrigger("NextState");
    }

    public void SetBool(string name, bool isBool)
    {
        ani.SetBool(name, isBool);
    }

    public void SetInteger(string name, int num)
    {
        ani.SetInteger(name, num);
    }

    public void SetTrigger(string name)
    {
        ani.SetTrigger(name);
    }

    public void DieAnimation() // 죽을시 시작
    {
        StartCoroutine(Die());
    }

    private void AttackRange()
    {
        if(enemyNavi.TargetDistance() > enemy.AttackRange)
        {
            isAttack = false;
        }
        else if (enemyNavi.TargetDistance() <= enemy.AttackRange)
        {
           
            isAttack = true;
        }

        ani.SetBool("IsAttack", isAttack);
    }

    private IEnumerator Die()
    {
        ani.SetBool("Die", true);
        ani.SetInteger("DieIndex", dieIndex); // 죽는 애니메이션 3가지중 1개 재생
        yield return null;
    }
}
