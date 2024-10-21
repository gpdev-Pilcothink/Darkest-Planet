using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlyaerController : MonoBehaviour
{
    private bool isDead = false;
    private bool isDamage = false;
    public bool isWeaponGrabbed = false;
    public string gameState = "New";
    public int health = 500;
    public int total_health=500;
    public int STR = 10;
    public int DEX = 10;
    public int INT = 10;
    public int story = 0;
    public int stone = 0;
    public int enhance = 0;
    public int live = 1;
    private Rigidbody playerRigidbody; // �÷��̾� ĳ������ ������ٵ�
    private CharacterController characterController;
    private Vector3 moveDirection;

    void Start()
    {
        gameState = PlayerPrefs.GetString("GameState");//���̺� ���� �ҷ�����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // ����� ������Ʈ���� ������ ��������
        characterController = GetComponent<CharacterController>();
        if (gameState == "New")
        {
            Debug.Log("abc");
            total_health = 500;
            health = total_health;
            STR = 10;
            DEX = 10;
            INT = 10;
            story = 0;
            stone = 0;
            enhance = 0;
            live = 1;
        }
        else if (gameState == "Continue")
        {
            total_health = PlayerPrefs.GetInt("total_health");
            health = PlayerPrefs.GetInt("health");
            STR = PlayerPrefs.GetInt("STR");
            DEX = PlayerPrefs.GetInt("DEX");
            INT = PlayerPrefs.GetInt("INT");
            story = PlayerPrefs.GetInt("story");
            stone = PlayerPrefs.GetInt("stone");
            enhance = PlayerPrefs.GetInt("enhance");
            live = PlayerPrefs.GetInt("live");
        }
    }

    void Update()
    {
        Die();
    }

    private void Die()
    {
        if (health <= 0)
        {
            Debug.Log("death");
            live = 0;
        }
        else
        {
            live = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            if (!isDamage && !isDead)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                StartCoroutine(OnDamage());
            }
        }
    }

    private IEnumerator OnDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(0.05f);
        isDamage = false;
    }

    public void SavePlayerData()
    {

        PlayerPrefs.SetInt("total_health", total_health);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("STR", STR);
        PlayerPrefs.SetInt("DEX", DEX);
        PlayerPrefs.SetInt("INT", INT);
        PlayerPrefs.SetInt("story", story);
        PlayerPrefs.SetInt("stone", stone);
        PlayerPrefs.SetInt("enhance", enhance);
        PlayerPrefs.SetInt("live", live);
        Debug.Log("save!");
    }

    public void ReinforceCharacter()
    {
        // ��ȭ ��� ���
        int reinforceCost = 1000 + enhance * 10000;

        // stone�� ��ȭ ��� �̻��̾�� ��ȭ ����
        if (stone >= reinforceCost)
        {
            // ��ȭ�� �ʿ��� ��� �Ҹ�
            stone -= reinforceCost;

            // ��ȭ�� ���� ���� ������Ʈ
            total_health += 100;
            STR += 10;
            DEX += 10;
            INT += 10;

            // ��ȭ ���� ����
            enhance++;
        }
        else
        {
            Debug.Log("��ȭ�� �ʿ��� ���� ���� �����մϴ�.");
        }
    }
    public int return_enhance()
    {
        return enhance;
    }
    public void recovery_health()
    {
        health = total_health;
    }
    public int return_live()
    {
        return live;
    }
    public int return_health()
    {
        if(total_health!=0)
            return health;
        else
        {
            return 1;
        }
    }
    public int return_damage()
    {
        return STR + DEX + INT / 2;
    }
    public int return_story()
    {
        return story;
    }

}
