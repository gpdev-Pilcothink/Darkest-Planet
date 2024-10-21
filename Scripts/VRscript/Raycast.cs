using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float rayLength = 500.0f; // ������ ����
    public LayerMask interactableLayer; // ��ȣ�ۿ��� �� �ִ� ���̾�
    public LineRenderer lineRenderer; // ������ �ð�ȭ�ϱ� ���� LineRenderer

    void Start()
    {
        if (lineRenderer == null)
        {
            // LineRenderer ������Ʈ�� ���ٸ� �ڵ����� �߰�
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // �⺻ LineRenderer ����
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f; // ���� �κ��� �ʺ�
        lineRenderer.endWidth = 0.01f;   // �� �κ��� �ʺ�
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength, interactableLayer))
        {
            // ������ ��ȣ�ۿ� ������ ���̾��� ��ü�� �浹�� ���
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    // �ش� ��ü�� ��ȣ�ۿ� ������ ���
                    //interactable.Interact();
                }
            }
        }
        else
        {
            // ������ �ƹ� ��ü�͵� �浹���� �ʴ� ���
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
        // ���� ���� ����
        OpenDoor();
    }

    private void OpenDoor() 
    {
        // �� ���� ���� ����
    }
}
 */
