using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Hp
{
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject dissvoleEffect;
    [SerializeField] private float speed; 
                     public float Speed { get { return speed; } }
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange;
                     public float AttackRange { get { return attackRange; } }

    [SerializeField] private AudioClip hitSoundClip;
    [SerializeField] private AudioClip dieSoudnClip;
    public SkinnedMeshRenderer render;
    private EnemyAnimator enemyAnimator;
    private EnemyNavi enemyNavi;
    private Transform target;
    private Player targetPlayer;
    private AudioSource audio;
    private float dissovleValue = -1.0f;
    public bool isShield = true;

    private void Awake()
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        enemyNavi = GetComponent<EnemyNavi>();
        audio = GetComponent<AudioSource>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        targetPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        dissovleValue = -1.0f;
        render.material.SetVector("_HitColor", Vector3.zero);
    }

    private void Update()
    {
        if (isDie)
        {
            render.material.SetFloat("_ValueAP", dissovleValue);
            return;
        }

        if (isShield == true) // 추적할때만 데미지 입게하기위한 bool
            return;
        LookAtTarget();
        enemyNavi.TargetTrace(); // Player 추적       
        Bomb();
    }

    public override void TakeDamage(int Damage)
    {
        if (isDie || isShield)
        {         
            return;
        }
    //    AudioToPlay(hitSoundClip);
        hpValue -= Damage;
        StartCoroutine(HitColorChange());
        if (hpValue <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public override void TakeDamage(int Damage, NameType type)
    {
        if (NameType.Explosion != type || isDie || isShield)
            return;

        hpValue -= Damage;
        StartCoroutine(HitColorChange());
        if (hpValue <= 0)
        {
            enemyAnimator.EnabledAnimatior();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        LayerName(this.gameObject.GetComponent<Transform>(),"Ignore Raycast");
        enemyAnimator.DieAnimation(); // 죽는애니메이션 재생
        dissvoleEffect.SetActive(true); // 디졸브 이펙트 재생 
        isDie = true;           // 죽었는지 확인 bool
        enemyNavi.NavMeshAgentEnabled(); // * 네비끄기 죽을시 계속 움직일수 있음
        StartCoroutine(Dissvole()); // 디졸브 이펙트 시작
        AudioToPlay(dieSoudnClip);
        yield return new WaitForSeconds(0.5f);

        IsColliderTrigger();
        yield return null;
    }  

    IEnumerator HitColorChange() // 피격시 빨갛게 표시되게함
    {
        render.material.SetVector("_HitColor", new Vector3(0, -1, -1));
        yield return new WaitForSeconds(0.05f);

        render.material.SetVector("_HitColor",Vector3.zero);
    }

    IEnumerator Dissvole()
    {
        while (true)
        {
            if (dissovleValue >= 1.0f)
            {
                Destroy(gameObject);
                yield break;
            }
            dissovleValue += Time.deltaTime;
            yield return null;
        }
    }

    public void Bomb()
    {
        if (enemyNavi.TargetDistance() < attackRange && enemyAnimator.fireState == EnemyStateType.SuicideBombing)
        {
            Instantiate(bloodEffect, transform.position, bloodEffect.transform.rotation);
            targetPlayer.TakeDamage(attackDamage);
            Destroy(this.gameObject);
            return;         
        }
    }

    private void IsColliderTrigger()
    {
        Collider[] col = GetComponentsInChildren<Collider>();
        foreach (Collider cols in col)
        {
            cols.isTrigger = true;
        }
    }

    private void LayerName(Transform target, string name)// 죽으면 레이캐스트 안맞게 ( 총에 ) 해당 Layer 전부 Ignore로 바꿈
    {
        target.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in target)
            LayerName(child, name);
    }

    private void LookAtTarget()
    {
        transform.LookAt(targetPlayer.transform);
    }

    public void EnemyAttack()
    {
        if(enemyNavi.TargetDistance() < attackRange)
        {          
            targetPlayer.TakeDamage(attackDamage);
            
        }
    }

    private void AudioToPlay(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
}
