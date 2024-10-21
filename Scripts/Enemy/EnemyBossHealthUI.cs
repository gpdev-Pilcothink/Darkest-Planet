using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthUI : MonoBehaviour
{
    public Transform target;                // �� ĳ����(Transform)
    public GameObject healthBarPrefab;      // ü�¹� UI ������
    public GameObject healthBarInstance;   // �������� ������ ü�¹� �ν��Ͻ�
    public Slider healthSlider;            // ü�¹� �����̴� ������Ʈ
    public float yOffset;
    public EnemyBoss enemyBoss;

    private void Start()
    {
        CreateHealthBar();
    }

    private void Update()
    {
        // �� ĳ������ ��ġ�� ���󰡵��� ü�¹� ��ġ ������Ʈ
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
        // ü�¹� UI �������� �ν��Ͻ�ȭ�ϰ� �� ĳ������ ���� ������Ʈ�� ����
        healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
    }

    // �� ĳ������ ü���� ������Ʈ�ϴ� �޼���
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
}
