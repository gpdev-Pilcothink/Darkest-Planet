using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_STATUS_NEEDSTONE : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textMeshProComponent;
    public VRPlyaerController player;

    void Start()
    {
        player = FindObjectOfType<VRPlyaerController>();

    }

    // Update is called once per frame
    void Update()
    {
        int tmp = player.stone;
        textMeshProComponent.text = "YOUR STONE: " + tmp.ToString();
    }
}
