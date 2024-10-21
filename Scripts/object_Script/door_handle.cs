using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class door_handle : OVRGrabbable
{
    public Transform doorTransform; // 문(Transform)에 대한 참조
    private bool isGrabbed = false; // 문고리를 잡은 상태인지 여부
    private Vector3 initialHandPosition; // 손의 초기 위치

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //잡았을때
    {
        base.GrabBegin(hand, grabPoint);
        isGrabbed = true;
        initialHandPosition = hand.transform.position; // 손의 초기 위치 저장

    }

    // 놓아질 때 호출됩니다.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //놓았을때
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;
        //여기부터 처리할 명령어 입력


    }
    private void Update()
    {
        if (isGrabbed && grabbedBy != null)
        {
            // 현재 손의 위치를 가져옵니다.
            Vector3 currentHandPosition = grabbedBy.transform.position;

            // 손의 이동 거리를 계산합니다.
            float moveDistance = Vector3.Dot(currentHandPosition - initialHandPosition, doorTransform.right);

            // 회전 각도를 계산하여 문을 엽니다.
            float rotationAngle = moveDistance * 1.0f; // 이 값은 손의 이동 거리에 따라 조절할 수 있습니다.

            // 문의 회전을 업데이트합니다.
            doorTransform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }
    }
}

