using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Hp
{
    [SerializeField] private GameObject bullet01;
    [SerializeField] private GameObject bullet02;
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private GameObject endObject;
    [SerializeField] private AudioClip[] clips;
    private float fireRate;
    private Animator ani;
    private AudioSource audio;
    public SkinnedMeshRenderer render;
    private float value;
    private bool isShield = false;
    private int maxHpValue;
    private int remainder; // 나머지

    private bool isOne = false;
    private bool isTwo = false;

    private void Awake()
    {
        maxHpValue = hpValue;
        remainder = maxHpValue / 5;
        ani = GetComponent<Animator>();
        fireRate = 1.0f;
        audio = GetComponent<AudioSource>();
        StartCoroutine(First());
    }

    private void Update()
    {
        if (hpValue <= 0)
        {
            isDie = true;
        }
        else
        {
            HpShader();
        }

        ani.SetBool("isDie", isDie);
        render.material.SetFloat("_ValueAP", value);
    }

    public override void TakeDamage(int Damage)
    {
        if (isShield || isDie)
            return;

        hpValue -= Damage;

    }

    public override void TakeDamage(int Damage, NameType type)
    {
        
    }

    private void HpShader()
    {
        if ((remainder * 4) <= hpValue) // 80% 이상시 
        {
            fireRate = 2.0f;
            value = -1.0f;
        }
        else if (remainder * 3 <= hpValue) // 60% 이상
        {
            fireRate = 1.5f;
            value = -0.5f;
        }

        else if (remainder * 2 <= hpValue) // 40% 이상
        {
            fireRate = 1.0f;
            value = -0.2f;
        }
        else if (remainder <= hpValue) // 20% 이상
        {
            fireRate = 0.5f;
            value = 0;
        }
    }

    IEnumerator First()
    {
        yield return new WaitForSeconds(2.0f);
        isShield = false;
        isOne = true;
        StartCoroutine(Pattern01());
        StartCoroutine(PlaySound());
    }

    IEnumerator Pattern01()
    {
        int num = 0;
        GameObject clone;

        while (true)
        {
            if(isDie == true)
            {
                StartCoroutine(Die());
                yield break;
            }

            int value = Random.Range(0, 2);

            if (value == 0)
            {
                clone = bullet01;
            }
            else
            {
                clone = bullet02;
            }

            Instantiate(clone, transform.position, Random.rotation);
            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator Die()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Box");
        for (int i = 0; i < obj.Length; i++)
            Destroy(obj[i]);

        dieEffect.SetActive(true);
        value = 1.0f;
        yield return new WaitForSeconds(2.5f);
        endObject.SetActive(true);
        Destroy(gameObject);
    }


    private void SoundPlay(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(5.0f);
        SoundPlay(clips[0]);
        yield return new WaitForSeconds(8.0f);
        SoundPlay(clips[1]);
        yield return new WaitForSeconds(8.0f);
        SoundPlay(clips[2]);
    }

}
