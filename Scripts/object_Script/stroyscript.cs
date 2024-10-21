using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stroyscript : MonoBehaviour
{
    public VRPlyaerController controller;
    public int story;
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.return_story() >= story)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
