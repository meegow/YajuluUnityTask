using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRepositionCollider : MonoBehaviour
{
    public delegate void OnRepositionPlatform();
    public static OnRepositionPlatform onRepositionPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.PLAYER_TAG))
        {
            onRepositionPlatform?.Invoke();
        }
    }
}
