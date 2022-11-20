using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform player;
    public Transform platformRoot;
    public Transform[] platforms;

    private int platformIndex;
    private float platformWidth;
    private bool reverseShuffle;

    private void OnEnable()
    {
        PlatformRepositionCollider.onRepositionPlatform += RepositionPlatform;
    }

    private void OnDisable()
    {
        PlatformRepositionCollider.onRepositionPlatform -= RepositionPlatform;
    }

    void Awake()
    {
        platformIndex = 0;
    }

    void Start()
    {
        platformWidth = platforms[0].GetComponent<BoxCollider>().bounds.size.z;
    }
    

    public void RepositionPlatform()
    {
        if(platformIndex >= platforms.Length)
        {
            platformIndex = 0;
        }

        Vector3 newVector = new Vector3(0f, 0f, platformWidth);

        if(reverseShuffle)
        {
            platforms[platformIndex].position = (Vector3)platforms[platformIndex - 1].position + newVector;
            reverseShuffle = false;
        }
        else
        {
            platforms[platformIndex].position = (Vector3)platforms[platformIndex + 1].position + newVector;
            reverseShuffle = true;
        }
        
        platformIndex++;
    }
}
