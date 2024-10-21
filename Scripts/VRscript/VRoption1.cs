using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;
using Unity.Collections;
using System.Linq.Expressions;

public class VRoption1 : MonoBehaviour
{
    public Button loadButton; // Unity 에디터에서 참조를 설정해야 합니다.
    public GameObject optionPanel; // 우리가 방금 만든 메뉴 패널
    public GameObject alwaysOnPanel; // 항상 켜져 있는 패널
    public List<Button> menuButtons = new List<Button>();
    public List<Button> menuButtons2 = new List<Button>();
    public VRPlyaerController controller;
    private int currentIndex = 0;
    private int currentIndex2 = 0;
    private float lastAxisY = 0;
    private float lastAxisY2 = 0;
    private bool isMenuActive = false; // 메뉴 패널의 활성화 상태를 추적
    public int live=1;
    public bool death=false;
    private void Start()
    {
        HighlightButton(menuButtons[currentIndex]);
        HighlightButton(menuButtons2[currentIndex]);
        optionPanel.SetActive(false); // 초기에 메뉴 패널을 숨깁니다.
    }

    private void Update()
    {
        int live = controller.return_health();
        if (live <=0 && !death) // Start 또는 Back 버튼
        {
            Debug.Log("토글 메뉴!");
            ToggleMenu();
            Time.timeScale = 0;
            death = true;
        }

        if (PlayerPrefs.HasKey("health"))
        {
           

            if (isMenuActive) // 메뉴가 활성화된 경우에만 썸스틱 입력 처리
            {

                AdjustCurrentIndex();

                float thumbstickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

                // 위로 이동
                if (thumbstickY > 0.5f && lastAxisY <= 0.5f)
                {
                    currentIndex--;
                    if (currentIndex < 0) currentIndex = menuButtons.Count - 1;
                    HighlightButton(menuButtons[currentIndex]);
                }
                // 아래로 이동
                else if (thumbstickY < -0.5f && lastAxisY >= -0.5f)
                {
                    currentIndex++;
                    if (currentIndex >= menuButtons.Count) currentIndex = 0;
                    HighlightButton(menuButtons[currentIndex]);
                }

                // A 버튼을 누르면 현재 버튼의 클릭 이벤트 실행
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Time.timeScale = 1; // 게임 재개
                    menuButtons[currentIndex].onClick.Invoke();
                }

                lastAxisY = thumbstickY;
            }
        }
        else
        {

            if (isMenuActive) // 메뉴가 활성화된 경우에만 썸스틱 입력 처리
            {

                AdjustCurrentIndex();

                float thumbstickY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

                // 위로 이동
                if (thumbstickY > 0.5f && lastAxisY2 <= 0.5f)
                {
                    currentIndex2--;
                    if (currentIndex2 < 0) currentIndex2 = menuButtons2.Count - 1;
                    HighlightButton(menuButtons2[currentIndex2]);
                }
                // 아래로 이동
                else if (thumbstickY < -0.5f && lastAxisY2 >= -0.5f)
                {
                    currentIndex2++;
                    if (currentIndex2 >= menuButtons2.Count) currentIndex2 = 0;
                    HighlightButton(menuButtons2[currentIndex2]);
                }

                // A 버튼을 누르면 현재 버튼의 클릭 이벤트 실행
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    Time.timeScale = 1; // 게임 재개
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

            // 모든 버튼을 검사한 후에도 활성화된 버튼을 찾지 못하면 루프를 종료
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
            }; // 게임을 일시 중지
        }
        else
        {
            Time.timeScale = 1; // 게임 재개
        }
    }

    private void HighlightButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}