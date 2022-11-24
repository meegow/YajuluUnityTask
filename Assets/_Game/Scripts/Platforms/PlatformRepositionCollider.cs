using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRepositionCollider : MonoBehaviour
{
    private bool fisrtTimePlayerPass = true;

    public delegate void OnRepositionPlatform();
    public static OnRepositionPlatform onRepositionPlatform;

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(Constants.PLAYER_TAG))
        {
            return;
        }

        if(!fisrtTimePlayerPass)
        {
            onRepositionPlatform?.Invoke();
        }
    }
    
    private void OnTriggerExit(Collider other) 
    {
        if(!other.CompareTag(Constants.PLAYER_TAG))
        {
            return;
        }

        if(fisrtTimePlayerPass)
        {
            fisrtTimePlayerPass = false;
        }
    }
    
    /// <summary>
    /// Reset Data on game Over
    /// </summary>
    public void GameOver()
    {
        fisrtTimePlayerPass = true;
    }
}
