using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField] List<GameObject> enemys;
    [SerializeField] private float nextStepTime;
    [Header("플레이어 이동x 바로 리스폰할지")]
    [SerializeField] private bool isNextWave = false; // 플레이어가 이동하지않고 다음 리스폰할것인지?
    [SerializeField] private GameObject targetList;
    [Header("적들을 언제 IsTrigger할건지? ")]
    [SerializeField] private float isTriggerTimer;
    private Player player;
    private bool nextMove = false;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i].activeSelf == false)
                enemys[i].SetActive(true);
        }

        StartCoroutine(EnemyIsTrigger());
    }

    private void Update()
    {
        if (nextMove == false)
            ListUpdate();
    }

    private void ListUpdate()
    {
        for(int i = 0; i < enemys.Count; i++)
        {
            if(enemys[i] == null)
            {
                enemys.RemoveAt(i);
            }
        }

        if(enemys.Count == 0)
        {
            StartCoroutine(NextStep());
            nextMove = true;
        }
    }

    //private void OnEnable()
    //{
    //    for (int i = 0; i < enemys.Count; i++)
    //    {
    //        if (enemys[i].activeSelf == false)
    //            enemys[i].SetActive(true);
    //    }

    //    StartCoroutine(EnemyIsTrigger());
    //}

    IEnumerator NextStep()
    {
        yield return new WaitForSeconds(nextStepTime);
        if (isNextWave)
        {
            targetList.SetActive(true);
        }
        else
            player.StepTrigger();

        Destroy(gameObject);
    }

    IEnumerator EnemyIsTrigger()
    {
        yield return new WaitForSeconds(isTriggerTimer);
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<EnemyAnimator>().EnemyMoveStart();
        }
    }
}
