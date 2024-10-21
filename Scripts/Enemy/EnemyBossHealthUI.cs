using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthUI : MonoBehaviour
{
    public Transform target;                // 적 캐릭터(Transform)
    public GameObject healthBarPrefab;      // 체력바 UI 프리팹
    public GameObject healthBarInstance;   // 동적으로 생성된 체력바 인스턴스
    public Slider healthSlider;            // 체력바 슬라이더 컴포넌트
    public float yOffset;
    public EnemyBoss enemyBoss;

    private void Start()
    {
        CreateHealthBar();
    }

    private void Update()
    {
        // 적 캐릭터의 위치를 따라가도록 체력바 위치 업데이트
        if (target != null)
        {
            //Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
            //healthBarInstance.transform.position = screenPos;

            RectTransform uiTransform = healthBarInstance.GetComponent<RectTransform>();
            uiTransform.position = target.position + Vector3.up * yOffset;
        }

        UpdateHealth(enemyBoss.curHealth, enemyBoss.maxHealth);
    }

    private void CreateHealthBar()
    {
        // 체력바 UI 프리팹을 인스턴스화하고 적 캐릭터의 하위 오브젝트로 설정
        healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
    }

    // 적 캐릭터의 체력을 업데이트하는 메서드
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
