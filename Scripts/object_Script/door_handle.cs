using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class door_handle : OVRGrabbable
{
    public Transform doorTransform; // ��(Transform)�� ���� ����
    private bool isGrabbed = false; // ������ ���� �������� ����
    private Vector3 initialHandPosition; // ���� �ʱ� ��ġ

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //�������
    {
        base.GrabBegin(hand, grabPoint);
        isGrabbed = true;
        initialHandPosition = hand.transform.position; // ���� �ʱ� ��ġ ����

    }

    // ������ �� ȣ��˴ϴ�.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //��������
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;
        //������� ó���� ��ɾ� �Է�


    }
    private void Update()
    {
        if (isGrabbed && grabbedBy != null)
        {
            // ���� ���� ��ġ�� �����ɴϴ�.
            Vector3 currentHandPosition = grabbedBy.transform.position;

            // ���� �̵� �Ÿ��� ����մϴ�.
            float moveDistance = Vector3.Dot(currentHandPosition - initialHandPosition, doorTransform.right);

            // ȸ�� ������ ����Ͽ� ���� ���ϴ�.
            float rotationAngle = moveDistance * 1.0f; // �� ���� ���� �̵� �Ÿ��� ���� ������ �� �ֽ��ϴ�.

            // ���� ȸ���� ������Ʈ�մϴ�.
            doorTransform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
        }
    }
}

