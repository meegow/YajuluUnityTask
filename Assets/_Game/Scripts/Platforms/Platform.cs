using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Platform
{
    public Transform platform;
    public PlatformRepositionCollider repositionController;
    private Vector3 initialPosition;

    public void SetInitialPosition()
    {
        initialPosition = platform.position;
    }

    public void ResetPosition()
    {
        platform.position = initialPosition;
    }
}
