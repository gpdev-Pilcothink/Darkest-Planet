using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //public GameObject hitEffectPrefab; // ȭ���� ��ǥ���� �¾��� �� ������ ȿ�� ������

    private void OnCollisionEnter(Collision collision)
    {
        // ȭ���� ��ǥ���� �浹���� �� ȣ��˴ϴ�.
        if (collision.collider.tag == "Enemy") // ��ǥ�� �±׸� ����ϰų� �ٸ� ����� ����� �ĺ��մϴ�.
        {
            Debug.Log("Arrow!!!!!!!!!!!!!");
            // ��ǥ���� �¾��� �� ȿ���� �����մϴ�.
            //Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            // ȭ���� �ı��մϴ�.
            //Destroy(gameObject, 5f);
        }

        if (collision.collider.tag == "Design")
        {
            Debug.Log("PRACTICE!!!!!!!!");
            Destroy(gameObject, 5f);
        }
    }
}
