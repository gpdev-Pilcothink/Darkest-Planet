using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
//using System.Diagnostics;

public class Enemy : MonoBehaviour
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
    public double minDodgeDistance;
    public double maxDodgeDistance;
    public int live = 1;

    VRPlyaerController player;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    Animator anim;

    public bool isDodging = false;      // 회피 중인지 체크
    public float dodgeSpeed = 6.0f;     // 회피할 때의 이동 속도
    private float dodgeDuration = 1.0f; // 회피 지속 시간
    public float dodgeInterval = 3.0f;  // 회피 대기 시간
    private float dodgeStartTime = 0f;
    public float dodgeDistance = 0.5f;



    public AudioSource audioSource;
    public AudioClip attackAudioClip;
    public AudioClip lowHealthAudioClip;
    public AudioClip otherAudioClip1;
    public AudioClip otherAudioClip2;

    //public Stopwatch stopwatch;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<VRPlyaerController>();

        Invoke("ChaseStart", 3);
    }

    void FixedUpdate()
    {
        FreezeVelocity();
        Targetting();
    }

    void Targetting()
    {
        float targetRadius = 1.2f;
        float targetRange = 0.3f;

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
        if (curHealth <= 0) yield break;

        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        // 피가 30%를 넘을 경우에만 NavMesh 비활성화
        if ((float)curHealth / maxHealth > 0.3f)
        {
            nav.enabled = false;
        }

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

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Update()
    {
        //stopwatch = new Stopwatch();

        // 죽어있을 경우 발동 안되게 Layer 검사 추가
        // 9번이 EnemyDead 판정
        if (gameObject.layer == 9) return;

        //// NavMesh 함수 실행 전 시간 측정 시작
        //stopwatch.Start();

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (nav.enabled)
        {
            // 회피 로직
            // 공격을 회피하기 위한 조건 추가
            if ((distanceToTarget <= maxDodgeDistance) && (distanceToTarget >= minDodgeDistance))
            {
                if (!isDodging && isChase && ((Time.time - dodgeStartTime) >= dodgeInterval))
                {
                    if (player.isWeaponGrabbed)
                    {
                        dodgeStartTime = Time.time;
                        StartCoroutine(Dodge());
                    }
                }
            }
            else
            {
                dodgeStartTime = Time.time;
            }

            if (!isDodging)
            {
                if ((float)curHealth / maxHealth <= 0.3f)
                {
                    meleeArea.enabled = false;
                    isChase = true;
                    isAttack = false;
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isWalk", true);

                    // "EnemyPoint" 태그를 가진 모든 오브젝트 검색
                    GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag("EnemyPoint");

                    // 활성화된 오브젝트 중에서 가장 가까운 오브젝트를 찾기 위한 초기화
                    float closestDistance = float.MaxValue;
                    Vector3 closestObjectPosition = Vector3.zero;

                    foreach (GameObject enemyPoint in enemyPoints)
                    {
                        if (enemyPoint.activeSelf)
                        {
                            // 현재 오브젝트와의 거리 계산
                            float objectDistance = Vector3.Distance(transform.position, enemyPoint.transform.position);

                            // 가장 가까운 오브젝트인지 확인
                            if (objectDistance < closestDistance)
                            {
                                closestDistance = objectDistance;
                                closestObjectPosition = enemyPoint.transform.position;
                            }
                        }
                    }

                    // 가장 가까운 오브젝트의 위치를 목표 위치로 설정
                    nav.SetDestination(closestObjectPosition);
                    nav.speed = 2.4f;
                    nav.isStopped = !isChase;
                }
                else
                {
                    nav.SetDestination(target.position);
                    nav.speed = 2f;

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
            }
        }

        //// NavMesh 함수 실행 후 시간 측정 종료
        //stopwatch.Stop();

        //// 실행 시간 출력
        //UnityEngine.Debug.Log("NavMesh Time : " + stopwatch.ElapsedMilliseconds / 1000000 + "ns");

        //// 시간 측정 초기화
        //stopwatch.Reset();
    }

    IEnumerator Dodge()
    {
        isDodging = true;

        ///////////////////////////////////좌-우 랜덤회피//////////////////////////////////////////////////

        // 회피 방향을 무작위로 선택 (좌측 또는 우측)
        int dodgeDirection = Random.Range(0, 2);            // 0 또는 1 (좌측 또는 우측)
        Vector3 dodgeDirectionVector = Vector3.zero;

        if (dodgeDirection == 0)    // 좌측으로 회피
        {
            dodgeDirectionVector = transform.TransformDirection(Vector3.left);   // 좌측으로 이동
        }
        else                        // 우측으로 회피
        {
            dodgeDirectionVector = transform.TransformDirection(Vector3.right);  // 우측으로 이동
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////

        // 회피 동작을 시작하고 일정 시간 동안 대기
        Vector3 dodgeTargetPosition = transform.position + dodgeDirectionVector * dodgeDistance;
        nav.SetDestination(dodgeTargetPosition);
        nav.speed = dodgeSpeed;

        // 회피 동작 종료 후 다시 쫒아오기 시작
        yield return new WaitForSeconds(dodgeDuration);
        isDodging = false;
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
                //Debug.Log("Melee : " + curHealth);

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
                //Debug.Log("Range : " + curHealth);

                StartCoroutine(OnDamage(reactVec));
                isDamaged = false;
            }
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        if (curHealth > 0)
        {
            if(live>0)
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
            live = 0;
            gameObject.layer = 9;
            reactVec = Vector3.zero;
            isChase = false;
            nav.enabled = false;

            anim.SetTrigger("doDie");

            player.stone += 1000;
            Destroy(gameObject, 3);
        }
    }
}
