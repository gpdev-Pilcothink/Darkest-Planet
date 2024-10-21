using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class Load_UI_Script : OVRGrabbable
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
    private void LoadNextScene()
    {
        PlayerPrefs.SetString("GameState", "Continue");
        SceneManager.LoadScene("DarkestPlanet");
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("health"))
        {
            // �����Ͱ� ������ ������Ʈ Ȱ��ȭ
            gameObject.SetActive(true);
            Debug.Log("Load torch up!");
        }
        else
        {
            // �����Ͱ� ������ ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }
}

