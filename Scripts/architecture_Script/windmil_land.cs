using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmil_land : MonoBehaviour
{
    public float rotationSpeed = 360.0f; // �ʴ� ȸ�� �ӵ� (360��/�ʸ� �� ���� ����)

    void Start()
    {
        
    }

    void Update()
    {
        // y �� ������ ȸ��
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
