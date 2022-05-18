using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTrigger : MonoBehaviour
{
    public List<GameObject> clone;
    public int count;
    private void Update()
    {
        Debug.Log(clone.Count);

        for(int i = 0; i < clone.Count; i++)
        {
            if (clone[i] == null)
                clone.RemoveAt(i);
        }

        if (clone.Count == 0)
            Debug.Log("적 소멸");
    }
}
