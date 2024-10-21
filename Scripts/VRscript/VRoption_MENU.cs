using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VRoption_MENU : MonoBehaviour
{
    public Button loadButton; // Unity �����Ϳ��� ������ �����ؾ� �մϴ�.

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
        Debug.Log("���� ��ư�� ���Ƚ��ϴ�!");
        // ���� ��ư�� ���� ������ ���⿡ �߰��ϼ���.
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
