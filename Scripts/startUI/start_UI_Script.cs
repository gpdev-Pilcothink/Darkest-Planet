using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class start_UI_Script : OVRGrabbable
{
    public VRPlyaerController playerController;
    // �׷��� �� ȣ��˴ϴ�.
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //�������
    {
        base.GrabBegin(hand, grabPoint);

        //������� ó���� ��ɾ� �Է�
        Invoke("LoadNextScene", 1f);
    }

    // ������ �� ȣ��˴ϴ�.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //��������
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        //������� ó���� ��ɾ� �Է�


    }
    public void LoadNextScene()
    {
        PlayerPrefs.SetString("GameState", "New");
        SceneManager.LoadScene("DarkestPlanet");
    }
}

