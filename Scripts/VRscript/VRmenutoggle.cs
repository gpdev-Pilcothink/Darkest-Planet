using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;

public class VRmenutoggle : MonoBehaviour
{
    public GameObject originalPannel; // 우리가 방금 만든 메뉴 패널
    public GameObject anotherPannel; // 항상 켜져 있는 패널
    private bool isMenuActive = true; // 메뉴 패널의 활성화 상태를 추적

    private void Start()
    {
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // Start 또는 Back 버튼
        {
            Debug.Log("토글 메뉴!");
            ToggleMenu();
        }

        
    }
    private void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        originalPannel.SetActive(isMenuActive);
        anotherPannel.SetActive(!isMenuActive);

       
    }
}