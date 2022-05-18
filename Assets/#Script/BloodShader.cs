using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShader : MonoBehaviour
{
    ParticleSystemRenderer render;
    private float value = -1;
    private void Awake()
    {
        render = GetComponent<ParticleSystemRenderer>();
    }

    private void Update()
    {
        if (value >= 1)
            return;

        value += Time.deltaTime * 2;

        render.material.SetFloat("_xValue", value);
    }

    private void OnEnable()
    {
        value = -1;
    }
}
