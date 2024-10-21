using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRPlayerController))]



public class VReasyScript : MonoBehaviour
{
    public AudioSource audioSource;
    private float movementThreshold = 0.05f; // 주 섬스틱 움직임
    private Vector2 thumbstickInput;

    private OVRPlayerController player;
    [SerializeField]
    private float moveSpeedMultipler = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<OVRPlayerController>();
        player.SetMoveScaleMultiplier(moveSpeedMultipler);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)) // A 버튼
        {
            player.Jump();
            Debug.Log("Jump");
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            player.SetMoveScaleMultiplier(moveSpeedMultipler * 2.0f);
        }
        else
        {
            player.SetMoveScaleMultiplier(moveSpeedMultipler);
        }
        thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (thumbstickInput.magnitude > movementThreshold)
        {
            // 걷는 소리 재생
            if (!audioSource.isPlaying)
            {
                Debug.Log("good");
                audioSource.Play();
            }
        }
        else
        {
            // 주 섬스틱 입력이 임계값보다 작은 경우 (움직임이 감지되지 않을 때)
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
