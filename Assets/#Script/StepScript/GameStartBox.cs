using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBox : MonoBehaviour
{
    [SerializeField] private GameObject enemyListTarget;
    private bool isStart = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            if (enemyListTarget != null)
                enemyListTarget.SetActive(true);

            if (isStart == false)
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().GameStartMode();

            Destroy(gameObject);
        }
    }
}
