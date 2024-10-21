using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmil_land : MonoBehaviour
{
    public float rotationSpeed = 360.0f; // 초당 회전 속도 (360도/초면 한 바퀴 돈다)

    void Start()
    {
        
    }

    void Update()
    {
        // y 축 주위로 회전
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
