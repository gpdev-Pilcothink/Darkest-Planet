using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_STATUS_DEX : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;

    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();
        textMeshProComponent.text = "DEX";
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.DEX;
        textMeshProComponent.text = "DEX " + tmp.ToString();
    }
}
