using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public float chasingDistance;
    public BoxCollider meleeArea;
    public Rigidbody rigid;
    public bool isDamaged = false;
    public Transform target;
    public bool isChase;
    public bool isAttack;
    public bool takeDamage = false;

    public AudioSource audioSource;
    public AudioClip attackAudioClip;
    public AudioClip lowHealthAudioClip;
    public AudioClip otherAudioClip1;
    public AudioClip otherAudioClip2;

    VRPlyaerController player;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<VRPlyaerController>();

        Invoke("ChaseStart", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer == 9) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        nav.SetDestination(target.position);
        nav.speed = 10f;

        if (distanceToTarget > chasingDistance)
        {
            anim.SetBool("isWalk", false);
            nav.isStopped = true;
        }
        else
        {
            anim.SetBool("isWalk", true);
            nav.isStopped = false;
        }
    }

    void FixedUpdate()
    {
        FreezeVelocity();
        Targetting();
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Targetting()
    {
        float targetRadius = 4f;
        float targetRange = 1f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
                                  targetRadius,
                                  transform.forward,
                                  targetRange,
                                  LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        nav.enabled = false;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(2f);
        meleeArea.enabled = false;

        // 공격이 끝나면 NavMeshAgent 활성화
        nav.enabled = true;

        anim.SetBool("isAttack", false);
        isAttack = false;
        isChase = true;
        audioSource.PlayOneShot(attackAudioClip);
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDamaged)
        {
            if (collision.collider.tag == "Melee")
            {
                isDamaged = true;
                VRWeapon weapon = collision.collider.gameObject.GetComponent<VRWeapon>();
                if (isDamaged)
                {
                    curHealth -= weapon.damage;
                }

                Vector3 reactVec = transform.position - collision.collider.transform.position;

                StartCoroutine(OnDamage(reactVec));
                isDamaged = false;
            }

            if (collision.collider.tag == "Range")
            {
                isDamaged = true;
                VRWeapon weapon = collision.collider.gameObject.GetComponent<VRWeapon>();
                if (isDamaged)
                {
                    curHealth -= weapon.damage;
                }

                Vector3 reactVec = transform.position - collision.collider.transform.position;

                StartCoroutine(OnDamage(reactVec));
                isDamaged = false;
            }
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        if (curHealth > 0)
        {
            audioSource.PlayOneShot(lowHealthAudioClip);
            isChase = false;
            nav.enabled = false;

            anim.SetBool("takeDamage", true);
            yield return new WaitForSeconds(1.0f);
            anim.SetBool("takeDamage", false);

            isChase = true;
            nav.enabled = true;

        }
        else
        {
            gameObject.layer = 9;
            reactVec = Vector3.zero;
            isChase = false;
            nav.enabled = false;

            anim.SetTrigger("doDie");

            Destroy(gameObject, 3);
        }
    }
}
