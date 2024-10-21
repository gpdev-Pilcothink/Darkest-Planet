using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OculusSampleFramework;

public class Load_UI_Script : OVRGrabbable
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
    private void LoadNextScene()
    {
        PlayerPrefs.SetString("GameState", "Continue");
        SceneManager.LoadScene("DarkestPlanet");
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("health"))
        {
            // 데이터가 있으면 오브젝트 활성화
            gameObject.SetActive(true);
            Debug.Log("Load torch up!");
        }
        else
        {
            // 데이터가 없으면 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }
}

