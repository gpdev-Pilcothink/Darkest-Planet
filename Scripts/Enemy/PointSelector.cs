using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSelector : MonoBehaviour
{
    public List<GameObject> enemyPointsList;
    public int numOfEnabledObjects = 3;

    void Start()
    {
        // "EnemyPoint" 태그를 가진 모든 오브젝트를 리스트에 추가
        GameObject[] enemyPoints = GameObject.FindGameObjectsWithTag("EnemyPoint");
        foreach (GameObject enemyPoint in enemyPoints)
        {
            enemyPointsList.Add(enemyPoint);
        }

        // 랜덤하게 numOfEnabledObjects개의 오브젝트 선택
        List<GameObject> selectedPoints = new List<GameObject>();

        while ((selectedPoints.Count < numOfEnabledObjects) && (enemyPointsList.Count > 0))
        {
            int randomIndex = Random.Range(0, enemyPointsList.Count);
            GameObject selectedPoint = enemyPointsList[randomIndex];

            // 선택한 오브젝트를 활성화 한 후 리스트에서 제거
            selectedPoint.SetActive(true);
            selectedPoints.Add(selectedPoint);
            enemyPointsList.RemoveAt(randomIndex);
        }

        // 선택하지 않은 나머지 오브젝트 비활성화
        foreach(GameObject remaingPoint in enemyPointsList)
        {
            remaingPoint.SetActive(false);
        }
    }

    // 추후 선택된 enemyPoint에 추가 조작을 가하고 싶을 경우 해당 부분에 코드 작성.
    void Update()
    {
        
    }
}
