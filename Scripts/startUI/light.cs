using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Light blinkingLight;
    public float startDelay = 5f;    // ���� �� 5�� ���
    public float blinkInterval = 5f; // 5�ʸ���
    public int blinkCount = 2;       // �� �� �����̱�

    private void Start()
    {
        if (blinkingLight == null)
        {
            blinkingLight = GetComponent<Light>();
        }
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            for (int i = 0; i < blinkCount; i++)
            {
                blinkingLight.enabled = false;
                yield return new WaitForSeconds(0.05f); // ���� ���� ���¸� �����ϴ� �ð�
                blinkingLight.enabled = true;
                yield return new WaitForSeconds(0.05f); // ���� ���� ���¸� �����ϴ� �ð�
            }
            
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
