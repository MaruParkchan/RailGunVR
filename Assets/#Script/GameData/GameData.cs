using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    /*
        Life 초기화 = 5

     */

    [SerializeField] private int lifeValue = 5;
    private void Awake()
    {
        PlayerPrefs.SetInt("LIFE", lifeValue);
    }
}
