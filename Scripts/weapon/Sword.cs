using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class sword : OVRGrabbable
{
    public Transform newParentTransform_left;
    public Transform newParentTransform_right;
    public Transform originalParentTransform;
    public VRPlyaerController controller;
    public AudioSource audioSource;  // AudioSource ���� �߰�
    public GameObject object1;  // ������Ʈ 1�� ���� ����
    public GameObject object2;  // ������Ʈ 2�� ���� ����
    public GameObject object3;  // ������Ʈ 3�� ���� ����
    private int ch_enhance;

    private bool isGrabbed = false; // ���⸦
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        ch_enhance = controller.return_enhance();
        Debug.Log("enhance:" + ch_enhance);
        controller = GameObject.FindWithTag("Player").GetComponent<VRPlyaerController>();
        controller.isWeaponGrabbed = true;
        if (audioSource != null)
        {
            audioSource.Play();
        }
        base.GrabBegin(hand, grabPoint);
        isGrabbed = true;

        // ��ü�� �θ� �����Ͽ� ī�޶� �Ǵ� �ٸ� ���� ��Ʈ�� �̵�
        // ��Ʈ�ѷ��� ��ġ�� �������� �� �ĺ�
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
        if (ch_enhance >= 1 && ch_enhance < 3)
        {
            object1.SetActive(true);
            object2.SetActive(false);
            object3.SetActive(false);
        }
        else if (ch_enhance >= 3 && ch_enhance < 15)
        {
            object1.SetActive(false);
            object2.SetActive(true);
            object3.SetActive(false);
        }
        else if (ch_enhance >= 16)
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
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        controller.isWeaponGrabbed = false;

        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;

        // ��ü�� �θ� ���� ��ġ�� ��������
        transform.SetParent(originalParentTransform);
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
    }
    // Update is called once per frame
    void Start()
    {
        ch_enhance = 0;
        // AudioSource ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
    }
}
