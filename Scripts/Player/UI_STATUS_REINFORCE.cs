using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_STATUS_REINFORCE : MonoBehaviour
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
        int tmp = 1000 + player.enhance * 10000;
        textMeshProComponent.text = "NEED STONE: " + tmp.ToString();
    }
}
