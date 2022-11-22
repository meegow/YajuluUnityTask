using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform platformRoot;
    public Transform[] platforms;

    private int platformIndex;
    private float platformWidth;
    private float targetRotation = 1;
    private float targetAngle;
    private bool reverseShuffle;
    private bool canRotatePlatform;
    private bool rotatePlatformLeft;
    [SerializeField] private float platformRotationSpeed;

    private void OnEnable()
    {
        InputManager.onPlatformRotationInput += StartFlipPlatform;
        PlatformRepositionCollider.onRepositionPlatform += RepositionPlatform;
    }

    private void OnDisable()
    {
        InputManager.onPlatformRotationInput -= StartFlipPlatform;
        PlatformRepositionCollider.onRepositionPlatform -= RepositionPlatform;
    }

    void Awake()
    {
        platformIndex = 0;
    }

    void Start()
    {
        targetAngle = platformRoot.eulerAngles.z;
        platformWidth = platforms[0].GetComponent<BoxCollider>().bounds.size.z;
    }

    void Update()
    {
        FlipPlatform();
    }
    
    void FlipPlatform()
    {
        float degreesPerSecond = platformRotationSpeed * Time.deltaTime;
        platformRoot.rotation = Quaternion.RotateTowards(platformRoot.rotation, 
            Quaternion.Euler(0f, 0f, targetAngle * targetRotation), degreesPerSecond);
    }

    void StartFlipPlatform(bool rotateLeft)
    {
        if(targetAngle == 360)
        {
            if(!rotatePlatformLeft && rotateLeft || rotatePlatformLeft && !rotateLeft)
            {
                targetAngle = 180;
            }
            else
            {
                targetAngle = 180;
                platformRoot.eulerAngles = Vector3.zero;
            }
            
        }
        else
        {
            targetAngle += 180;
        }
   
        if (rotateLeft && targetRotation > 0)
        {
            targetRotation *= -1;
        }

        if (!rotateLeft && targetRotation < 0)
        {
            targetRotation *= -1;
        }
        
        // Debug.Log("targetAngle "+targetAngle);
        // Debug.Log("targetRotation "+targetRotation);
        rotatePlatformLeft = rotateLeft;
        canRotatePlatform = true;
    }

    void RepositionPlatform()
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
