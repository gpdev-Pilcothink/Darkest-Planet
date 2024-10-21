using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button loadButton; // Unity 에디터에서 참조를 설정해야 합니다.

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