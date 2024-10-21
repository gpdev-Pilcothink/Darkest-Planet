using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class start_UI_Script : OVRGrabbable
{
    public VRPlyaerController playerController;
    // 그랩될 때 호출됩니다.
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)  //잡았을때
    {
        base.GrabBegin(hand, grabPoint);

        //여기부터 처리할 명령어 입력
        Invoke("LoadNextScene", 1f);
    }

    // 놓아질 때 호출됩니다.
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)  //놓았을때
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        //여기부터 처리할 명령어 입력


    }
    public void LoadNextScene()
    {
        PlayerPrefs.SetString("GameState", "New");
        SceneManager.LoadScene("DarkestPlanet");
    }
}

