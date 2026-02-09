using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeFX : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float forceMultiplier;
    public void ScreenShake(int facingDirection)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x*facingDirection,shakeDirection.y) *forceMultiplier;
        impulse.GenerateImpulse();

    }
}
