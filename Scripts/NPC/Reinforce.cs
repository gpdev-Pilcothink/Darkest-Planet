using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;

public class Reinforce : MonoBehaviour
{
    public Animator animator;
    private bool isInRange = false;
    public GameObject ReinforcePanel; // �츮�� ��� ���� �޴� �г�
    private bool isMenuActive = false; // �޴� �г��� Ȱ��ȭ ���¸� ����
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
            // A��ư�÷��̾ ��ȣ�ۿ� Ű�� ������ ��ȭ�� ������ �� �ֵ��� ����
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                StartConversation();
            }
        }

        if (isMenuActive) 
        {
            // A ��ư�� ������ ���� ��ư�� Ŭ�� �̺�Ʈ ����
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                menuButtons.onClick.Invoke();
            }

        }
    }

    private void StartConversation()
    {
        ToggleMenu();
        Debug.Log("NPC�� ��ȭ�� �����մϴ�.");
        // ��ȭ Ʈ�� �Ǵ� ��ȭ �ɼ��� ����Ͽ� ��ȭ�� ����
    }

    private void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        ReinforcePanel.SetActive(isMenuActive);
      
    }
}
