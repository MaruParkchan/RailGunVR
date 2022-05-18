using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntersity : MonoBehaviour
{
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;

    [SerializeField] private float loopTime;
    private Light light;

    private void Awake()
    {
        light = GetComponent<Light>();
        StartCoroutine(LightLoop());
    }

    private void Update()
    {
        
    }

    IEnumerator LightLoop()
    {
        while(true)
        {
            light.intensity = Random.Range(minValue, maxValue);
            yield return new WaitForSeconds(loopTime);
        }
    }
}
