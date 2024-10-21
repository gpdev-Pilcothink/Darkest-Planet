using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_STATUS_STONE : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;

    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();
        textMeshProComponent.text = "STONE";
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.stone;
        textMeshProComponent.text = "STONE " + tmp.ToString();
    }
}
