using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : Hp
{
    [SerializeField] private GameObject bloodImage;
    [SerializeField] private float refreshTime;
    [SerializeField] private AudioClip[] hitSound = new AudioClip[2];
    [SerializeField] private AudioClip dieSound;
    private AudioSource audio;
    private int soundIndex = 0;
    [Header("LifeUI")]
    [SerializeField] private TextMeshProUGUI[] lifeUI = new TextMeshProUGUI[2];
    [Header("Gun")]
    [SerializeField] private Gun leftGun;
    [SerializeField] private Gun rightGun;

    private Animator ani;
    private bool isDamage;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();      
    }

    private void Start()
    {
        hpValue = PlayerPrefs.GetInt("LIFE");
    }

    private void Update()
    {
        for (int i = 0; i < lifeUI.Length; i++)
            lifeUI[i].text = "LIFE\n" + hpValue;

        PlayerPrefs.SetInt("LIFE", hpValue);

        if (hpValue <= 0)
        {
            if (isDie)
                return;

            isDie = true;
            leftGun.GunStopFire();
            rightGun.GunStopFire();
            ani.Play("PlayerDie");
            audio.clip = dieSound;
            audio.Play();
        }
    }

    public override void TakeDamage(int Damage)
    {
        if (isDamage || isDie)
            return;

        hpValue -= Damage;
        isDamage = true;
        StartCoroutine(ReFreshState());
    }


    public override void TakeDamage(int Damage, NameType type)
    {
        
    }

    public void StepTrigger()
    {
        ani.SetTrigger("NextStep");
    }

    IEnumerator ReFreshState() // 다시 공격 받기 위함
    {
        bloodImage.SetActive(true);
        HitSound();
        yield return new WaitForSeconds(refreshTime);
        bloodImage.SetActive(false);
        isDamage = false;
    }

    private void HitSound()
    {
        if (soundIndex >= 2)
            soundIndex = 0;

        audio.clip = hitSound[soundIndex];
        audio.Play();
        soundIndex++;
    }
}
