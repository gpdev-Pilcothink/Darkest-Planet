using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRoption_MENU : MonoBehaviour
{
    public Button loadButton; // Unity 에디터에서 참조를 설정해야 합니다.

    public void OnReturnButtonClicked()
    {
        Debug.Log("Return!! Invoke!");
        Return();
    }

    public void OnTurnSaveButtonClicked()
    {
        Debug.Log("Return!! clicked!!");
        TurnSave();
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("종료 버튼이 눌렸습니다!");
        // 종료 버튼에 대한 로직을 여기에 추가하세요.
        Application.Quit();
    }

    private void Return()
    {
        Debug.Log("Return!!");
        PlayerPrefs.SetString("GameState", "Continue");
        SceneManager.LoadScene("startUI");
    }
    private void TurnSave()
    {
        PlayerPrefs.SetString("GameState", "Continue");
        SceneManager.LoadScene("DarkestPlanet");
    }
}
