using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;

    public delegate void OnPlatformRotationInput(bool rotateLeft);
    public static OnPlatformRotationInput onPlatformRotationInput;


    void Update()
    {
        PlayerInput();
        FlipPlatformInput();
    }

    void FlipPlatformInput()
    {
        if(!GameManager.startGamePlay)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            onPlatformRotationInput?.Invoke(true);
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            onPlatformRotationInput?.Invoke(false);
        }

    }

    void PlayerInput()
    {
        if(!GameManager.startGamePlay)
        {
            return;
        }

        float horizontalAxis = Input.GetAxisRaw(Constants.HORIZONTAL_INPUT_AXIS);

        if(horizontalAxis != 0)
        {
            if(horizontalAxis > 0)
            {
                playerStateController.ChangePlayerState(PlayerStates.RightMovement);
            }
            else if(horizontalAxis < 0)
            {
                playerStateController.ChangePlayerState(PlayerStates.LeftMovement);
            }
        }
        else
        {
            playerStateController.ChangePlayerState(PlayerStates.ForwardMovement);
        }
    }
}
