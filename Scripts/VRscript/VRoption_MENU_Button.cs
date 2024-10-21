using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button loadButton; // Unity �����Ϳ��� ������ �����ؾ� �մϴ�.

    private void Start()
    {
        
    }
    private void Update()
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
        };
    }
}