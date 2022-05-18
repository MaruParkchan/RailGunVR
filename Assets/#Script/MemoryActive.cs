using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryActive : MonoBehaviour
{
    private MemoryPool memory;

    public float DeActiveTimer;

    private void OnEnable()
    {
        StartCoroutine(DeActive());
    }

    public void Setup(MemoryPool pool)
    {
        this.memory = pool;
    }

    IEnumerator DeActive()
    {
        yield return new WaitForSeconds(DeActiveTimer);
        memory.DeactivatePoolItem(this.gameObject);
    }
    
}
