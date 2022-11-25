using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRepositionCollider : MonoBehaviour
{
    public bool fisrtTimePlayerPass;

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
            StartCoroutine(RenableCollider());
            onRepositionPlatform?.Invoke();
        }
    }

    IEnumerator RenableCollider()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<Collider>().enabled = true;
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
