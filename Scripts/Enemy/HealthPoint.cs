using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    private Renderer objectRenderer;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            objectRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.curHealth = enemy.maxHealth;
        }
    }
}
