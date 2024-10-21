using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public Light blinkingLight;
    public float startDelay = 5f;    // 시작 후 5초 대기
    public float blinkInterval = 5f; // 5초마다
    public int blinkCount = 2;       // 두 번 깜빡이기

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
                yield return new WaitForSeconds(0.05f); // 불이 켜진 상태를 유지하는 시간
                blinkingLight.enabled = true;
                yield return new WaitForSeconds(0.05f); // 불이 꺼진 상태를 유지하는 시간
            }
            
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
