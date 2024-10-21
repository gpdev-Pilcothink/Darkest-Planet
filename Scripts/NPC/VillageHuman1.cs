using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageHuman1 : MonoBehaviour
{
    public Animator animator;
    private bool isInRange = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
            // �÷��̾ ��ȣ�ۿ� Ű�� ������ ��ȭ�� ������ �� �ֵ��� ����
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartConversation();
            }
        }
    }

    private void StartConversation()
    {
        // ��ȭ ���� ������ ���⿡ �߰�
        Debug.Log("NPC�� ��ȭ�� �����մϴ�.");
        // ��ȭ Ʈ�� �Ǵ� ��ȭ �ɼ��� ����Ͽ� ��ȭ�� ����
    }
}
