using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightdayandnight : MonoBehaviour
{
    public float dayDurationMinutes = 6f; // ���� ���� ��ȯ �ӵ� (�� ����)
    private float rotationSpeed; // Directional Light ȸ�� �ӵ�

    private void Start()
    {
        rotationSpeed = 360f / (dayDurationMinutes * 60f); // �ʴ� ȸ�� ���� ���
    }

    private void Update()
    {
        // Directional Light�� y���� �������� ȸ��
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
