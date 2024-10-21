using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;
using Unity.Collections;
using System.Linq.Expressions;

public class VRoption1 : MonoBehaviour
{
    public Button loadButton; // Unity �����Ϳ��� ������ �����ؾ� �մϴ�.
    public GameObject optionPanel; // �츮�� ��� ���� �޴� �г�
    public GameObject alwaysOnPanel; // �׻� ���� �ִ� �г�
    public List<Button> menuButtons = new List<Button>();
    public List<Button> menuButtons2 = new List<Button>();
    public VRPlyaerController controller;
    private int currentIndex = 0;
    private int currentIndex2 = 0;
    private float lastAxisY = 0;
    private float lastAxisY2 = 0;
    private bool isMenuActive = false; // �޴� �г��� Ȱ��ȭ ���¸� ����
    public int live=1;
    public bool death=false;
    private void Start()
    {
        HighlightButton(menuButtons[currentIndex]);
        HighlightButton(menuButtons2[currentIndex]);
        optionPanel.SetActive(false); // �ʱ⿡ �޴� �г��� ����ϴ�.
    }

    private void Update()
    {
        int live = controller.return_health();
        if (live <=0 && !death) // Start �Ǵ� Back ��ư
        {
            Debug.Log("��� �޴�!");
            ToggleMenu();
            Time.timeScale = 0;
            death = true;
        }

        if (PlayerPrefs.HasKey("health"))
        {
           

            if (isMenuActive) // �޴��� Ȱ��ȭ�� ��쿡�� �潺ƽ �Է� ó��
            {

                AdjustCurrentIndex();

                float thumbstickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

                // ���� �̵�
                if (thumbstickY > 0.5f && lastAxisY <= 0.5f)
                {
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = menuButtons.Count - 1;
                    HighlightButton(menuButtons[currentIndex]);
                }
                // �Ʒ��� �̵�
                else if (thumbstickY < -0.5f && lastAxisY >= -0.5f)
                {
                    currentIndex++;
                    if (currentIndex >= menuButtons.Count) currentIndex = 0;
                    HighlightButton(menuButtons[currentIndex]);
                }

                // A ��ư�� ������ ���� ��ư�� Ŭ�� �̺�Ʈ ����
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Time.timeScale = 1; // ���� �簳
                    menuButtons[currentIndex].onClick.Invoke();
                }

                lastAxisY = thumbstickY;
            }
        }
        else
        {

            if (isMenuActive) // �޴��� Ȱ��ȭ�� ��쿡�� �潺ƽ �Է� ó��
            {

                AdjustCurrentIndex();

                float thumbstickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

                // ���� �̵�
                if (thumbstickY > 0.5f && lastAxisY2 <= 0.5f)
                {
                    currentIndex2--;
                    if (currentIndex2 < 0) currentIndex2 = menuButtons2.Count - 1;
                    HighlightButton(menuButtons2[currentIndex2]);
                }
                // �Ʒ��� �̵�
                else if (thumbstickY < -0.5f && lastAxisY2 >= -0.5f)
                {
                    currentIndex2++;
                    if (currentIndex2 >= menuButtons2.Count) currentIndex2 = 0;
                    HighlightButton(menuButtons2[currentIndex2]);
                }

                // A ��ư�� ������ ���� ��ư�� Ŭ�� �̺�Ʈ ����
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Time.timeScale = 1; // ���� �簳
                    menuButtons2[currentIndex2].onClick.Invoke();
                }

                lastAxisY2 = thumbstickY;
            }
        }
    }

    private void AdjustCurrentIndex()
    {
        if (currentIndex >= menuButtons.Count || currentIndex < 0)
        {
            currentIndex = 0;
        }

        int originalIndex = currentIndex;
        while (!menuButtons[currentIndex].gameObject.activeInHierarchy)
        {
            currentIndex++;
            if (currentIndex >= menuButtons.Count)
            {
                currentIndex = 0;
            }

            // ��� ��ư�� �˻��� �Ŀ��� Ȱ��ȭ�� ��ư�� ã�� ���ϸ� ������ ����
            if (currentIndex == originalIndex)
            {
                break;
            }
        }
    }

    private void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        optionPanel.SetActive(isMenuActive);
        alwaysOnPanel.SetActive(!isMenuActive);

        if (isMenuActive)
        {
            if (loadButton != null)
            {
                if (!PlayerPrefs.HasKey("health"))
                {
                    loadButton.gameObject.SetActive(false);
                }
                else
                {
                    loadButton.gameObject.SetActive(true);
                }
            }; // ������ �Ͻ� ����
        }
        else
        {
            Time.timeScale = 1; // ���� �簳
        }
    }

    private void HighlightButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}