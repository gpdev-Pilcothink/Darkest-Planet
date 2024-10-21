using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class Bow : OVRGrabbable
{
    public GameObject arrowPrefab; // ȭ�� ������
    public Transform arrowSpawnPoint; // ȭ�� �߻� ����
    public float arrowSpeed = 10f; // ȭ�� �߻� �ӵ�
    public float maxDrawDistance = 1.5f; // �ִ� ���� �Ÿ�
    private bool isDrawing = false; // Ȱ�� ���� ������ ����
    private GameObject currentArrow; // ���� ȭ��
    private FixedJoint arrowJoint; // ȭ��� Ȱ�� �����ϱ� ���� ����Ʈ

    //
    private bool isGrabbed = false; // Ȱ�� ���� �������� ����
    public Transform newParentTransform_left;
    public Transform newParentTransform_right;
    public Transform originalParentTransform;
    private AudioSource audioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public VRPlyaerController controller;
    public GameObject object1;  // ������Ʈ 1�� ���� ����
    public GameObject object2;  // ������Ʈ 2�� ���� ����
    public GameObject object3;  // ������Ʈ 3�� ���� ����
    //
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //�������
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
            transform.SetParent(newParentTransform_left); // �޼��� ���
        }
        else
        {
            transform.SetParent(newParentTransform_right); // �������� ���
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

    // ������ �� ȣ��˴ϴ�.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //��������
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;
        transform.SetParent(originalParentTransform);
        //������� ó���� ��ɾ� �Է�


    }
    //
    void Start()
    {
        // AudioSource ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame

    void Update()
    {
        // Oculus ��Ʈ�ѷ��� ��ư ���� Ȯ��
        bool fireButtonPressed = OVRInput.Get(OVRInput.Button.Three);

        if (isGrabbed)
        {
            // Ȱ ���� ���� ���
            if (isDrawing)
            {
                // ���� �Ÿ� ����
                float drawDistance = Vector3.Distance(transform.position, currentArrow.transform.position);
                if (drawDistance > maxDrawDistance)
                {
                    drawDistance = maxDrawDistance;
                }

                // ȭ�� ������ ���� (���� �Ÿ��� ����)
                Vector3 newScale = new Vector3(1f, 1f, drawDistance);
                currentArrow.transform.localScale = newScale;

                // ��ư�� ���� ȭ�� �߻�
                if (!fireButtonPressed)
                {
                    PlaySound(sound2);
                    FireArrow();
                }
            }
            else
            {
                // ��ư�� ���� �� ȭ�� ���� �� ���� ����
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
        // ȭ�� �������� �ν��Ͻ�ȭ�Ͽ� ���� ����
        currentArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation * Quaternion.Euler(0, 0, 90));
        currentArrow.SetActive(true);
        isDrawing = true;

        // Fixed Joint�� �����Ͽ� ȭ��� Ȱ�� ����
        arrowJoint = currentArrow.AddComponent<FixedJoint>();
        arrowJoint.connectedBody = arrowSpawnPoint.GetComponent<Rigidbody>();
    }

    void FireArrow()
    {
        if (currentArrow != null)
        {
            // ȭ�쿡 �ӵ��� �־� �߻�
            Rigidbody arrowRigidbody = currentArrow.GetComponent<Rigidbody>();
            arrowRigidbody.velocity = -arrowSpawnPoint.right * arrowSpeed;
            arrowRigidbody.useGravity = true;

            // ���� ���� �� �ʱ�ȭ
            isDrawing = false;
            currentArrow = null;

            // Fixed Joint ���� (ȭ��� Ȱ �и�)
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
