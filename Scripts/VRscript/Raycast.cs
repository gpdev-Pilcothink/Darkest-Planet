using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float rayLength = 500.0f; // 광선의 길이
    public LayerMask interactableLayer; // 상호작용할 수 있는 레이어
    public LineRenderer lineRenderer; // 광선을 시각화하기 위한 LineRenderer

    void Start()
    {
        if (lineRenderer == null)
        {
            // LineRenderer 컴포넌트가 없다면 자동으로 추가
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // 기본 LineRenderer 설정
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f; // 시작 부분의 너비
        lineRenderer.endWidth = 0.01f;   // 끝 부분의 너비
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, interactableLayer))
        {
            // 광선이 상호작용 가능한 레이어의 객체와 충돌한 경우
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    // 해당 객체가 상호작용 가능한 경우
                    //interactable.Interact();
                }
            }
        }
        else
        {
            // 광선이 아무 객체와도 충돌하지 않는 경우
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * rayLength);
        }
    }
}

public interface IInteractable
{
    //void Interact();
}


/*
 using UnityEngine;

public class Door : MonoBehaviour, IInteractable 
{
    public void Interact() 
    {
        // 문을 여는 로직
        OpenDoor();
    }

    private void OpenDoor() 
    {
        // 문 여는 로직 구현
    }
}
 */
