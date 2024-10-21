using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class Bow : OVRGrabbable
{
    public GameObject arrowPrefab; // 화살 프리팹
    public Transform arrowSpawnPoint; // 화살 발사 지점
    public float arrowSpeed = 10f; // 화살 발사 속도
    public float maxDrawDistance = 1.5f; // 최대 조준 거리
    private bool isDrawing = false; // 활을 조준 중인지 여부
    private GameObject currentArrow; // 현재 화살
    private FixedJoint arrowJoint; // 화살과 활을 연결하기 위한 조인트

    //
    private bool isGrabbed = false; // 활을 잡은 상태인지 여부
    public Transform newParentTransform_left;
    public Transform newParentTransform_right;
    public Transform originalParentTransform;
    private AudioSource audioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public VRPlyaerController controller;
    public GameObject object1;  // 오브젝트 1에 대한 참조
    public GameObject object2;  // 오브젝트 2에 대한 참조
    public GameObject object3;  // 오브젝트 3에 대한 참조
    //
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //잡았을때
    {
        base.GrabBegin(hand, grabPoint);
        isGrabbed = true;
        PlaySound(sound1);
        Vector3 controllerPosition = hand.transform.position;
        Vector3 objectPosition = transform.position;
        float distanceToControllerLeft = Vector3.Distance(controllerPosition, newParentTransform_left.position);
        float distanceToControllerRight = Vector3.Distance(controllerPosition, newParentTransform_right.position);

        if (distanceToControllerLeft < distanceToControllerRight)
        {
            transform.SetParent(newParentTransform_left); // 왼손의 경우
        }
        else
        {
            transform.SetParent(newParentTransform_right); // 오른손의 경우
        }
        if (controller.enhance >= 1 || controller.enhance < 3)
        {
            object1.SetActive(true);
            object2.SetActive(false);
            object3.SetActive(false);
        }
        else if (controller.enhance >= 3 || controller.enhance < 15)
        {
            object1.SetActive(false);
            object2.SetActive(true);
            object3.SetActive(false);
        }
        else if (controller.enhance >= 16)
        {
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(true);
        }
        else
        {
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(false);
        }

    }

    // 놓아질 때 호출됩니다.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //놓았을때
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;
        transform.SetParent(originalParentTransform);
        //여기부터 처리할 명령어 입력


    }
    //
    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame

    void Update()
    {
        // Oculus 컨트롤러의 버튼 상태 확인
        bool fireButtonPressed = OVRInput.Get(OVRInput.Button.Three);

        if (isGrabbed)
        {
            // 활 조준 중인 경우
            if (isDrawing)
            {
                // 조준 거리 제한
                float drawDistance = Vector3.Distance(transform.position, currentArrow.transform.position);
                if (drawDistance > maxDrawDistance)
                {
                    drawDistance = maxDrawDistance;
                }

                // 화살 스케일 조절 (조준 거리에 따라)
                Vector3 newScale = new Vector3(1f, 1f, drawDistance);
                currentArrow.transform.localScale = newScale;

                // 버튼을 떼면 화살 발사
                if (!fireButtonPressed)
                {
                    PlaySound(sound2);
                    FireArrow();
                }
            }
            else
            {
                // 버튼을 누를 때 화살 생성 및 조준 시작
                if (fireButtonPressed)
                {
                    PlaySound(sound3);
                    StartDrawing();
                }
            }
        }
    }

    void StartDrawing()
    {
        // 화살 프리팹을 인스턴스화하여 조준 시작
        currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation * Quaternion.Euler(0, 0, 90));
        currentArrow.SetActive(true);
        isDrawing = true;

        // Fixed Joint를 생성하여 화살과 활을 연결
        arrowJoint = currentArrow.AddComponent<FixedJoint>();
        arrowJoint.connectedBody = arrowSpawnPoint.GetComponent<Rigidbody>();
    }

    void FireArrow()
    {
        if (currentArrow != null)
        {
            // 화살에 속도를 주어 발사
            Rigidbody arrowRigidbody = currentArrow.GetComponent<Rigidbody>();
            arrowRigidbody.velocity = -arrowSpawnPoint.right * arrowSpeed;
            arrowRigidbody.useGravity = true;

            // 조준 종료 및 초기화
            isDrawing = false;
            currentArrow = null;

            // Fixed Joint 제거 (화살과 활 분리)
            if (arrowJoint != null)
            {
                Destroy(arrowJoint);
            }
        }
    }
    void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
