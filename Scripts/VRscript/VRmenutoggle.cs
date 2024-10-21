using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;

public class VRmenutoggle : MonoBehaviour
{
    public GameObject originalPannel; // �츮�� ��� ���� �޴� �г�
    public GameObject anotherPannel; // �׻� ���� �ִ� �г�
    private bool isMenuActive = true; // �޴� �г��� Ȱ��ȭ ���¸� ����

    private void Start()
    {
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // Start �Ǵ� Back ��ư
        {
            Debug.Log("��� �޴�!");
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