using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSelector : MonoBehaviour
{
    public List<GameObject> enemyPointsList;
    public int numOfEnabledObjects = 3;

    void Start()
    {
        // "EnemyPoint" �±׸� ���� ��� ������Ʈ�� ����Ʈ�� �߰�
        GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag("EnemyPoint");
        foreach (GameObject enemyPoint in enemyPoints)
        {
            enemyPointsList.Add(enemyPoint);
        }

        // �����ϰ� numOfEnabledObjects���� ������Ʈ ����
        List<GameObject> selectedPoints = new List<GameObject>();

        while ((selectedPoints.Count < numOfEnabledObjects) && (enemyPointsList.Count > 0))
        {
            int randomIndex = Random.Range(0, enemyPointsList.Count);
            GameObject selectedPoint = enemyPointsList[randomIndex];

            // ������ ������Ʈ�� Ȱ��ȭ �� �� ����Ʈ���� ����
            selectedPoint.SetActive(true);
            selectedPoints.Add(selectedPoint);
            enemyPointsList.RemoveAt(randomIndex);
        }

        // �������� ���� ������ ������Ʈ ��Ȱ��ȭ
        foreach(GameObject remaingPoint in enemyPointsList)
        {
            remaingPoint.SetActive(false);
        }
    }

    // ���� ���õ� enemyPoint�� �߰� ������ ���ϰ� ���� ��� �ش� �κп� �ڵ� �ۼ�.
    void Update()
    {
        
    }
}
