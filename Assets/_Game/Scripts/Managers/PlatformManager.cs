using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform platformRoot;
    public Platform[] platforms;

    public delegate void OnInitializePlatforms();
    public static OnInitializePlatforms onInitializePlatforms;

    private int platformIndex;
    private float platformWidth;
    private float targetRotation = 1;
    private float targetAngle;
    private bool reverseShuffle;
    private bool canRotatePlatform;
    private bool rotatePlatformLeft;
    [SerializeField] private GameObjectVariable player;
    [SerializeField] private float platformRotationSpeed;

    private void OnEnable()
    {
        UIGameOver.onResetGame += ResetPlatforms;
        GameManager.onInitializeGame += InitializePlatforms;
        InputManager.onPlatformRotationInput += StartFlipPlatform;
        PlatformRepositionCollider.onRepositionPlatform += RepositionPlatform;
    }

    private void OnDisable()
    {
        UIGameOver.onResetGame -= ResetPlatforms;
        GameManager.onInitializeGame -= InitializePlatforms;
        InputManager.onPlatformRotationInput -= StartFlipPlatform;
        PlatformRepositionCollider.onRepositionPlatform -= RepositionPlatform;
    }

    void Update()
    {
        FlipPlatform();
    }

    void ResetPlatforms()
    {
        for (int platformIndex = 0; platformIndex < platforms.Length; platformIndex++)
        {
            platforms[platformIndex].ResetPosition();

            if (platformIndex == 0)
            {
                platforms[platformIndex].repositionController.Reset();
            }
        }

        this.platformIndex = 0;
        reverseShuffle = false;
        onInitializePlatforms?.Invoke();
    }

    void InitializePlatforms()
    {
        this.platformIndex = 0;
        targetAngle = platformRoot.eulerAngles.z;
        platformWidth = platforms[0].platform.GetComponent<BoxCollider>().bounds.size.z;

        for (int platformIndex = 0; platformIndex < platforms.Length; platformIndex++)
        {
            platforms[platformIndex].SetInitialPosition();

            if (platformIndex == 0)
            {
                platforms[platformIndex].repositionController.Reset();
            }
        }

        onInitializePlatforms?.Invoke();
    }

    void FlipPlatform()
    {
        if(!canRotatePlatform)
        {
            return;
        }

        platformRoot.RotateAround(player.GameObjectTransform.position, Vector3.forward * targetRotation,
            platformRotationSpeed * Time.deltaTime);

        if(Vector3.Distance(platformRoot.eulerAngles, new Vector3(0,0,targetAngle)) < 8f)
        {
            platformRoot.eulerAngles = new Vector3(0,0,targetAngle);
            canRotatePlatform = false;
        }
   
        // float degreesPerSecond = platformRotationSpeed * Time.deltaTime;
        // platformRoot.rotation = Quaternion.RotateTowards(platformRoot.rotation, 
        //     Quaternion.Euler(0f, 0f, targetAngle * targetRotation), degreesPerSecond);
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
   
        // if (rotateLeft && targetRotation > 0)
        // {
        //     targetRotation *= -1;
        // }

        // if (!rotateLeft && targetRotation < 0)
        // {
        //     targetRotation *= -1;
        // }
        
        if (rotateLeft && targetRotation < 0)
        {
            targetRotation *= -1;
        }

        if (!rotateLeft && targetRotation > 0)
        {
            targetRotation *= -1;
        }
        
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
            platforms[platformIndex].platform.position = 
                (Vector3)platforms[platformIndex - 1].platform.position + newVector;
            reverseShuffle = false;
        }
        else
        {
            platforms[platformIndex].platform.position = 
                (Vector3)platforms[platformIndex + 1].platform.position + newVector;
            reverseShuffle = true;
        }
        
        platformIndex++;
    }
}
