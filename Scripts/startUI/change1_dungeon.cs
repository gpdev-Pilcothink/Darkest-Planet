using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class change1_dungeon : OVRGrabbable
{
    public VRPlyaerController playerController;
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //�������
    {
        base.GrabBegin(hand, grabPoint);

        //������� ó���� ��ɾ� �Է�
        Invoke("LoadNextScene", 0.1f);
    }

    // ������ �� ȣ��˴ϴ�.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //��������
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        //������� ó���� ��ɾ� �Է�


    }
    private void LoadNextScene()
    {
        if (playerController.return_story() < 1)
        {
            playerController.story = 1;
        }
        playerController.SavePlayerData();
        PlayerPrefs.SetString("GameState", "Continue");
        SceneManager.LoadScene("dungeon_one");
    }
}
