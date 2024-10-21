using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRReinforceScript : MonoBehaviour
{
    // Start is called before the first frame update

    public VRPlyaerController playerController;

    private void Start()
    {
        // VRPlayerController 스크립트를 가져오기
        playerController = FindObjectOfType<VRPlyaerController>();
    }
    public void OnReinforceButtonClicked()
    {
        Debug.Log("Return!! Invoke!");
        Reinforce();
    }
    public void Reinforce()
    {
        playerController.ReinforceCharacter();
    }
}
