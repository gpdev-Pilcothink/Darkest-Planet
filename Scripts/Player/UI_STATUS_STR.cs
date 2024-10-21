using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_STATUS_STR : MonoBehaviour
{
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;


    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();
        textMeshProComponent.text = "STR";
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.STR;
        Debug.Log(tmp);
        textMeshProComponent.text = "STR " + tmp.ToString();
    }
}
