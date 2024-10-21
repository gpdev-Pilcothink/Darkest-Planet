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
            // 플레이어가 상호작용 키를 누르면 대화를 시작할 수 있도록 설정
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartConversation();
            }
        }
    }

    private void StartConversation()
    {
        // 대화 시작 로직을 여기에 추가
        Debug.Log("NPC와 대화를 시작합니다.");
        // 대화 트리 또는 대화 옵션을 사용하여 대화를 관리
    }
}
