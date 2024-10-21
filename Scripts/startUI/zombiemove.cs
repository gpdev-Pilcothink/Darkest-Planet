using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiemove : MonoBehaviour
{
    private float moveSpeed = 500.0f; // 이동 속도
    private float moveDuration = 15f; // 이동하는 시간 간격

    private float timer = 0.0f; // 경과 시간을 측정하는 타이머

    private AudioSource audioSource; // AudioSource 컴포넌트

    private void Start()
    {
        // AudioSource 컴포넌트를 찾아옴
        audioSource = GetComponent<AudioSource>();


    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 6.0f && timer < 6.3f)
        {
            // 이동 시간의 반만큼은 앞으로 이동
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
            // 이동 시간의 나머지 반은 뒤로 이동
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (timer >= moveDuration)
        {
            timer = 0.0f;
        }
    }

}