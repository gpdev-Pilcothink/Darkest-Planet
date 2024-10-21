using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //public GameObject hitEffectPrefab; // 화살이 목표물에 맞았을 때 생성될 효과 프리팹

    private void OnCollisionEnter(Collision collision)
    {
        // 화살이 목표물에 충돌했을 때 호출됩니다.
        if (collision.collider.tag == "Enemy") // 목표물 태그를 사용하거나 다른 방법을 사용해 식별합니다.
        {
            Debug.Log("Arrow!!!!!!!!!!!!!");
            // 목표물에 맞았을 때 효과를 생성합니다.
            //Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            // 화살을 파괴합니다.
            //Destroy(gameObject, 5f);
        }

        if (collision.collider.tag == "Design")
        {
            Debug.Log("PRACTICE!!!!!!!!");
            Destroy(gameObject, 5f);
        }
    }
}
