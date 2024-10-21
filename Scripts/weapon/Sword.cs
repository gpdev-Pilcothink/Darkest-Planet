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
    public AudioSource audioSource;  // AudioSource 변수 추가
    public GameObject object1;  // 오브젝트 1에 대한 참조
    public GameObject object2;  // 오브젝트 2에 대한 참조
    public GameObject object3;  // 오브젝트 3에 대한 참조
    private int ch_enhance;

    private bool isGrabbed = false; // 무기를
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

        // 물체의 부모를 변경하여 카메라 또는 다른 하위 루트로 이동
        // 컨트롤러의 위치를 기준으로 손 식별
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

        // 물체의 부모를 원래 위치로 돌려놓음
        transform.SetParent(originalParentTransform);
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
    }
    // Update is called once per frame
    void Start()
    {
        ch_enhance = 0;
        // AudioSource 컴포넌트 가져오기
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
