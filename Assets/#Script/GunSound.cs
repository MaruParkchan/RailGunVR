using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSound : MonoBehaviour
{
    [SerializeField] Gun leftGun;
    [SerializeField] Gun rightGun;
    AudioSource audio;
    private bool isPlaying;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if((leftGun.isFireSound == true || rightGun.isFireSound == true) && isPlaying == false)
        {
            StartCoroutine(Sound());
        }
    }

    IEnumerator Sound()
    {
        isPlaying = true;
        audio.PlayOneShot(audio.clip);
        yield return new WaitForSeconds(0.05f);
        isPlaying = false;
    }

    public void PlaySound()
    {
        if (isPlaying)
            return;

        audio.PlayOneShot(audio.clip);
    }
}
