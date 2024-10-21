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
            // �÷��̾ ��ȣ�ۿ� Ű�� ������ ��ȭ�� ������ �� �ֵ��� ����
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartConversation();
            }
        }
    }

    private void StartConversation()
    {
        Debug.Log("NPC�� ��ȭ�� �����մϴ�.");

        // �������� ����� ����
        AudioClip selectedAudioClip = Random.Range(0f, 1f) > 0.5f ? audioClip1 : audioClip2;

        // ���õ� ������� ���
        audioSource.clip = selectedAudioClip;
        audioSource.Play();

        // ��ȭ Ʈ�� �Ǵ� ��ȭ �ɼ��� ����Ͽ� ��ȭ�� ����
    }
}
