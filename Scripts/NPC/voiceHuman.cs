using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiceHuman : MonoBehaviour
{
    public Animator animator;
    private bool isInRange = false;
    public AudioClip audioClip1;
    public AudioClip audioClip2;

    private AudioSource audioSource;
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
        Debug.Log("NPC와 대화를 시작합니다.");

        // 랜덤으로 오디오 선택
        AudioClip selectedAudioClip = Random.Range(0f, 1f) > 0.5f ? audioClip1 : audioClip2;

        // 선택된 오디오를 재생
        audioSource.clip = selectedAudioClip;
        audioSource.Play();

        // 대화 트리 또는 대화 옵션을 사용하여 대화를 관리
    }
}
