using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_STATUS_ENHANCE : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;

    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();
        textMeshProComponent.text = "ENHANCE";
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.enhance;
        textMeshProComponent.text = "ENHANCE " + tmp.ToString();
    }
}
