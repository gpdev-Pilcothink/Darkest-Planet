using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiemove : MonoBehaviour
{
    private float moveSpeed = 500.0f; // �̵� �ӵ�
    private float moveDuration = 15f; // �̵��ϴ� �ð� ����

    private float timer = 0.0f; // ��� �ð��� �����ϴ� Ÿ�̸�

    private AudioSource audioSource; // AudioSource ������Ʈ

    private void Start()
    {
        // AudioSource ������Ʈ�� ã�ƿ�
        audioSource = GetComponent<AudioSource>();


    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 6.0f && timer < 6.3f)
        {
            // �̵� �ð��� �ݸ�ŭ�� ������ �̵�
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        else if (timer >6.3f && timer <8.0f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (timer > 14.7f)
        {
            // �̵� �ð��� ������ ���� �ڷ� �̵�
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (timer >= moveDuration)
        {
            timer = 0.0f;
        }
    }

}