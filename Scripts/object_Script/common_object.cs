using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class common_object : OVRGrabbable
{
    public Transform newParentTransform_left;
    public Transform newParentTransform_right;
    public Transform originalParentTransform;
    public AudioSource audioSource;  // AudioSource 변수 추가
    private bool isGrabbed = false; // 무기를
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
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
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;

        // 물체의 부모를 원래 위치로 돌려놓음
        transform.SetParent(originalParentTransform);
    }
    // Update is called once per frame

    void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource warning");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {

    }
}
