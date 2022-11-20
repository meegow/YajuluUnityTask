using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;

    void Start()
    {
        
    }


    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if(!GameManager.startGamePlay)
        {
            return;
        }

        float horizontalAxis = Input.GetAxisRaw("Horizontal");

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
