using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Gun leftGun;
    [SerializeField] private Gun rightGun;

    private bool isStart = false;

    private void Update()
    {
        if(isStart && player.isDie == false)
        {
            leftGun.Updated();
            rightGun.Updated();
        }
    }

    public void GameStartMode()
    {
        isStart = true;
    }
}
