using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STARTING_VRPlayerController : MonoBehaviour
{
 

    void Start()
    {
        if (!PlayerPrefs.HasKey("isFirstRun"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("isFirstRun", 1);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

