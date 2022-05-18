using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private bool isFadeIn;
    [SerializeField] private bool isFadeOut;
    [SerializeField] private float fadeSpeed;
    private float alphaValue = 0;
    private void Awake()
    {
        image = GetComponent<Image>();

        if (isFadeIn)
        {
            alphaValue = 255f;
            StartCoroutine(FadeIn());
        }
        else if (isFadeOut)
        {
            alphaValue = 0;
            StartCoroutine(FadeOut());
        }   
    }

    private void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alphaValue / 255f);
    }

    IEnumerator FadeIn()
    {
        while(true)
        {
            if (alphaValue <= 0)
                yield break;

            alphaValue -= Time.deltaTime * 10 * fadeSpeed;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while(true)
        {
            if (alphaValue >= 255)
                yield break;

            alphaValue += Time.deltaTime * 10 * fadeSpeed;
            yield return null;
        }
    }
}
