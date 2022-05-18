using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0;

    private void Awake()
    {
        Destroy(this.gameObject,destroyTime);
    }
}
