using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OVRPlayerController))]



public class VReasyScript : MonoBehaviour
{
    public AudioSource audioSource;
    private float movementThreshold = 0.05f; // �� ����ƽ ������
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
        if (OVRInput.GetDown(OVRInput.Button.One)) // A ��ư
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
            // �ȴ� �Ҹ� ���
            if (!audioSource.isPlaying)
            {
                Debug.Log("good");
                audioSource.Play();
            }
        }
        else
        {
            // �� ����ƽ �Է��� �Ӱ谪���� ���� ��� (�������� �������� ���� ��)
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
