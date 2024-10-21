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

    public bool isDodging = false;      // ȸ�� ������ üũ
    public float dodgeSpeed = 6.0f;     // ȸ���� ���� �̵� �ӵ�
    private float dodgeDuration = 1.0f; // ȸ�� ���� �ð�
    public float dodgeInterval = 3.0f;  // ȸ�� ��� �ð�
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

        // �ǰ� 30%�� ���� ��쿡�� NavMesh ��Ȱ��ȭ
        if ((float)curHealth / maxHealth > 0.3f)
        {
            nav.enabled = false;
        }

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(2f);
        meleeArea.enabled = false;

        // ������ ������ NavMeshAgent Ȱ��ȭ
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

        // �׾����� ��� �ߵ� �ȵǰ� Layer �˻� �߰�
        // 9���� EnemyDead ����
        if (gameObject.layer == 9) return;

        //// NavMesh �Լ� ���� �� �ð� ���� ����
        //stopwatch.Start();

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (nav.enabled)
        {
            // ȸ�� ����
            // ������ ȸ���ϱ� ���� ���� �߰�
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

                    // "EnemyPoint" �±׸� ���� ��� ������Ʈ �˻�
                    GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag("EnemyPoint");

                    // Ȱ��ȭ�� ������Ʈ �߿��� ���� ����� ������Ʈ�� ã�� ���� �ʱ�ȭ
                    float closestDistance = float.MaxValue;
                    Vector3 closestObjectPosition = Vector3.zero;

                    foreach (GameObject enemyPoint in enemyPoints)
                    {
                        if (enemyPoint.activeSelf)
                        {
                            // ���� ������Ʈ���� �Ÿ� ���
                            float objectDistance = Vector3.Distance(transform.position, enemyPoint.transform.position);

                            // ���� ����� ������Ʈ���� Ȯ��
                            if (objectDistance < closestDistance)
                            {
                                closestDistance = objectDistance;
                                closestObjectPosition = enemyPoint.transform.position;
                            }
                        }
                    }

                    // ���� ����� ������Ʈ�� ��ġ�� ��ǥ ��ġ�� ����
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

        //// NavMesh �Լ� ���� �� �ð� ���� ����
        //stopwatch.Stop();

        //// ���� �ð� ���
        //UnityEngine.Debug.Log("NavMesh Time : " + stopwatch.ElapsedMilliseconds / 1000000 + "ns");

        //// �ð� ���� �ʱ�ȭ
        //stopwatch.Reset();
    }

    IEnumerator Dodge()
    {
        isDodging = true;

        ///////////////////////////////////��-�� ����ȸ��//////////////////////////////////////////////////

        // ȸ�� ������ �������� ���� (���� �Ǵ� ����)
        int dodgeDirection = Random.Range(0, 2);            // 0 �Ǵ� 1 (���� �Ǵ� ����)
        Vector3 dodgeDirectionVector = Vector3.zero;

        if (dodgeDirection == 0)    // �������� ȸ��
        {
            dodgeDirectionVector = transform.TransformDirection(Vector3.left);   // �������� �̵�
        }
        else                        // �������� ȸ��
        {
            dodgeDirectionVector = transform.TransformDirection(Vector3.right);  // �������� �̵�
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////

        // ȸ�� ������ �����ϰ� ���� �ð� ���� ���
        Vector3 dodgeTargetPosition = transform.position + dodgeDirectionVector * dodgeDistance;
        nav.SetDestination(dodgeTargetPosition);
        nav.speed = dodgeSpeed;

        // ȸ�� ���� ���� �� �ٽ� �i�ƿ��� ����
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
