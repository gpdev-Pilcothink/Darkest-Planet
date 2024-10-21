using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;

public class Reinforce : MonoBehaviour
{
    public Animator animator;
    private bool isInRange = false;
    public GameObject ReinforcePanel; // 우리가 방금 만든 메뉴 패널
    private bool isMenuActive = false; // 메뉴 패널의 활성화 상태를 추적
    public Button menuButtons;
    private void Start()
    {
        animator = GetComponent<Animator>();
        ReinforcePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        if (other.tag == "Player")
        {
            animator.SetBool("isContact", true);
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player"))
        if (other.tag == "Player")
        {
            animator.SetBool("isContact", false);
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange)
        {
            // A버튼플레이어가 상호작용 키를 누르면 대화를 시작할 수 있도록 설정
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                StartConversation();
            }
        }

        if (isMenuActive) 
        {
            // A 버튼을 누르면 현재 버튼의 클릭 이벤트 실행
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                menuButtons.onClick.Invoke();
            }

        }
    }

    private void StartConversation()
    {
        ToggleMenu();
        Debug.Log("NPC와 대화를 시작합니다.");
        // 대화 트리 또는 대화 옵션을 사용하여 대화를 관리
    }

    private void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        ReinforcePanel.SetActive(isMenuActive);
      
    }
}
