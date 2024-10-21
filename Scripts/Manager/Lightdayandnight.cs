using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightdayandnight : MonoBehaviour
{
    public float dayDurationMinutes = 6f; // 낮과 밤의 전환 속도 (분 단위)
    private float rotationSpeed; // Directional Light 회전 속도

    private void Start()
    {
        rotationSpeed = 360f / (dayDurationMinutes * 60f); // 초당 회전 각도 계산
    }

    private void Update()
    {
        // Directional Light을 y축을 기준으로 회전
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
