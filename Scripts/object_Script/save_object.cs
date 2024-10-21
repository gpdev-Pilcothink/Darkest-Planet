using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class save_object : OVRGrabbable
{
    public Transform newParentTransform_left;
    public Transform newParentTransform_right;
    public Transform originalParentTransform;

    private bool isGrabbed = false; // ���⸦
    public VRPlyaerController playerController;

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
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
        playerController.recovery_health();
        playerController.SavePlayerData();
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        isGrabbed = false;

        // ��ü�� �θ� ���� ��ġ�� ��������
        transform.SetParent(originalParentTransform);
    }

    // Update is called once per frame
}
