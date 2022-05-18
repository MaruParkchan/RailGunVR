using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("컨트롤러")]
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean gripAction;
    public SteamVR_Action_Boolean sideAction;
    public SteamVR_Action_Single pos; // 트리거 위치값
    private MemoryPool memoryCasingItem; // 탄피 메모리풀
    private MemoryPool memoryHitEffectItem; // 총 히트 메모리풀
    private MemoryPool memoryBloodEffectItem; // 좀비 피 메모리풀
    private MemoryPool memoryHitTextItem; // hit Text 메모리풀
    [Header("--------")]
    [SerializeField] private Transform gunTrigger; // 총 트리거 회전값
    [SerializeField] private Transform firePoint;  // 총나가는 위치 및 방향
    [SerializeField] private GameObject casingPrefab; // 탄피 오브젝트
    [SerializeField] private Transform casingPoint; // 탄피 생성위치
    [Header("이펙트")]
    [SerializeField] private GameObject fireEffect; // 화염 이펙트
    [SerializeField] private GameObject bloodEffect; // 피 이펙트
    [SerializeField] private GameObject hitEffect; // 적을 맞출시 이펙트
    [SerializeField] private GameObject hitTextEffect;
    [Header("GunAbility")]
    [SerializeField] private float fireRate; // 총 속도
    [SerializeField] private int fireDamage; // 총 데미지
    [SerializeField] private int bulletCount;
    [Header("UI")]
    [SerializeField] TextMeshProUGUI bulletCountText;
    [SerializeField] GameObject reloadText;
    private bool isFire = false;
    [HideInInspector]
    public bool isFireSound = false; 
    [Header("Sound")]
    private AudioSource audio;
    [SerializeField] private AudioClip fireClip; // 발사소리
    [SerializeField] private AudioClip reloadClip; // 장전소리
    [SerializeField] private AudioClip fireNullClip; // 빈탄창소리

    private void Awake()
    {
        audio = GetComponent<AudioSource>();

        memoryCasingItem = new MemoryPool(casingPrefab);
        memoryHitEffectItem = new MemoryPool(hitEffect);
        memoryBloodEffectItem = new MemoryPool(bloodEffect);
        memoryHitTextItem = new MemoryPool(hitTextEffect);
    }

    public void Updated()
    {
        GunTriggerUpdate();
        BulletTextCountUpdate();

        if(bulletCount <= 0)
        {
            reloadText.SetActive(true);
        }
        else
        {
            reloadText.SetActive(false);
        }

        if (teleportAction.GetStateDown(handType))
        {
            isFire = false;
            isFireSound = false;
            bulletCount = 30;
            AudioPlay(reloadClip);
        }

        if (teleportAction.GetStateUp(handType))
        {
  
            Debug.Log("텔레포트땜");
        }

        if (gripAction.GetStateDown(handType))
        {         
            isFire = true;
            StartCoroutine(Fire());         
            Debug.Log("그립버튼누름");
        }

        if (gripAction.GetStateUp(handType))
        {
            isFire = false;
            isFireSound = false;
            Debug.Log("그립버튼땜");
        }

        if (sideAction.GetStateDown(handType))
            Debug.Log("사이드버튼누름");

        if (sideAction.GetStateUp(handType))
            Debug.Log("사이드버튼땜");
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (isFire == false || bulletCount <= 0)
            {
                AudioPlay(fireNullClip);
                isFireSound = false;
                yield break;
            }

            bulletCount--;
            fireEffect.SetActive(true);
            RayCastDirection();
            SpawnCasing(); // 탄피생성
            isFireSound = true;
            yield return new WaitForSeconds(fireRate);
           fireEffect.SetActive(false);
        }
    }

    private void GunTriggerUpdate() // 총 트리거 업데이트
    {
        gunTrigger.transform.localRotation = Quaternion.Euler(-30 * pos.GetAxis(handType),0,0);
    }

    private void RayCastDirection()
    {
        Ray ray;
        RaycastHit hit;

        if(Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100.0f))
        {
            EnemyHit enemyHit = hit.transform.GetComponent<EnemyHit>();
          //  Hp hp = hit.transform.root.GetComponent<Hp>();

            GameObject hitEffect = memoryHitEffectItem.ActivePoolItem();
            hitEffect.transform.position = hit.point;
            hitEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
            MemorlPoolSetUp(hitEffect, memoryHitEffectItem);

            if (hit.transform.tag == "Enemy" && enemyHit.isDie == false)
            {

                GameObject bloodEffect = memoryBloodEffectItem.ActivePoolItem();
                bloodEffect.transform.position = hit.point;
                bloodEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                MemorlPoolSetUp(bloodEffect, memoryBloodEffectItem);

                if (enemyHit.isShield == false)
                {
                    GameObject hitText = memoryHitTextItem.ActivePoolItem();
                    hitText.transform.position = hit.point;
                    MemorlPoolSetUp(hitText, memoryHitTextItem);
                }

                enemyHit.TakeDamage(fireDamage);
            }

            if(hit.transform.tag == ("Box"))
            {
                Hp hp = hit.transform.GetComponent<Hp>();

                hp.TakeDamage(fireDamage);
            }

            if(hit.transform.tag == "Boss")
            {
                Hp hp = hit.transform.GetComponent<Hp>();

                hp.TakeDamage(fireDamage);
            }

            if (hit.transform.name.Contains("Bomb"))
            {
                Hp hp = hit.transform.GetComponent<Hp>();

                hp.TakeDamage(fireDamage);
            }

        }
        Debug.DrawRay(firePoint.position, firePoint.forward * 100.0f, Color.red);
    }

    private void SpawnCasing() // 탄피생성
    {
        GameObject casingClone = memoryCasingItem.ActivePoolItem();
        casingClone.transform.position = casingPoint.position;
        casingClone.transform.rotation = Random.rotation;
        MemorlPoolSetUp(casingClone, memoryCasingItem);
        casingClone.GetComponent<CasingObject>().Init(transform.right);
    }

    private void MemorlPoolSetUp(GameObject target, MemoryPool memoryItem) // 메모리풀할 오브젝트 , 메모리해당할 Pool Data
    {
        target.GetComponent<MemoryActive>().Setup(memoryItem);
    }

    private void BulletTextCountUpdate() // 총 카운트
    {
        bulletCountText.text = "" + bulletCount;
    }

    private void SoundClip(AudioClip clip)
    {
        audio.clip = clip;
    }

    private void AudioPlay(AudioClip clip) 
    {
        audio.PlayOneShot(clip);
    }

    public void GunStopFire()
    {
        StopCoroutine("Fire");
    }
    
}
