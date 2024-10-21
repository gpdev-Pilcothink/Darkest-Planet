using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_STATUS_INT : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;

    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();
        textMeshProComponent.text = "INT";
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.INT;
        textMeshProComponent.text = "INT " + tmp.ToString();
    }
}
