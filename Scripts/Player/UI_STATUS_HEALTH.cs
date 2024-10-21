using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_STATUS_HEALTH : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;

    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();
        textMeshProComponent.text = "HEALTH";
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.health;
        int total_tmp = player.total_health;
        textMeshProComponent.text = "HEALTH " + tmp.ToString()+"/"+total_tmp.ToString();
    }
}
