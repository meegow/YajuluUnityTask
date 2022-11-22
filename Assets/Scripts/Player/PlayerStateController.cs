﻿using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    // private bool isGrounded;
    private PlayerStates currentState = PlayerStates.ForwardMovement;
    private PlayerStates previousState = PlayerStates.ForwardMovement;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;

    [Header("Player Actions")]
    [SerializeField] private PlayerMovement playerMovement;


    // private void Awake()
    // {
    //     jumpBounceAction = GetComponent<PlayerJumpBounceAction>();
    //     verticalDashAction = GetComponent<PlayerVerticalDash>();
    //     playerStunned = GetComponent<PlayerStunned>();
    //     deathAction = GetComponent<PlayerDeathAction>();
    //     swingWeapon = GetComponent<PlayerSwingWeapon>();
    //     playerInvincibility = GetComponent<PlayerInvincibility>();
    //     playerJugerNut = GetComponent<PlayerJugerNut>();
    //     jumpstartController = GetComponent<PlayerJumpstartController>();
    // }

    void FixedUpdate()
    {
        PlayerStateUpdates();
    }
    
    void PlayerStateUpdates()
    {
        switch (currentState)
        {
            case PlayerStates.ForwardMovement:
                playerMovement.MovePlayer(rigidBody, 1, true);
                break;

            case PlayerStates.LeftMovement:
                playerMovement.MovePlayer(rigidBody, -1, false);
                break;

            case PlayerStates.RightMovement:
                playerMovement.MovePlayer(rigidBody, 1, false);
                break;

            case PlayerStates.Falling:
                
                break;
        }
    }

    public void ChangePlayerState(PlayerStates newState)
    {
        if (checkIfAbortOnStateCondition(newState))
            return;

        if (!checkForValidStatePair(newState))
            return;

        switch (newState)
        {
            case PlayerStates.ForwardMovement:
                animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                break;

            case PlayerStates.LeftMovement:
                animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                break;

            case PlayerStates.RightMovement:
                 animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                break;

            case PlayerStates.Falling:
                animator.Play(Constants.PLAYER_FALLING_ANIMATION);
                break;

            case PlayerStates.Dead:
               
                break;
        }

        previousState = currentState;
        currentState = newState;
    } 

    bool checkForValidStatePair(PlayerStates newState)
    {
        bool returnVal = false;

        switch (currentState)
        {
            case PlayerStates.ForwardMovement:
                returnVal = true;
                break;

            case PlayerStates.LeftMovement:
                // if (newState.Equals(PlayerStates.Run))
                //     returnVal = true;
                // else
                //     returnVal = false;
                returnVal = true;
                break;

            case PlayerStates.RightMovement:
                returnVal = true;
                break;

            case PlayerStates.Falling:
                if (newState.Equals(PlayerStates.ForwardMovement))
                    returnVal = true;
                else
                    returnVal = false;

                break;

            case PlayerStates.Dead:
                // The only state that can take over from kill is resurrect
                // if(newState.Equals(PlayerStates.Revive))
                //     returnVal = true;
                // else
                //     returnVal = false;
                break;

        }
        return returnVal;
    }

    // check if there is any reason this state should not be allowed to begin.
    bool checkIfAbortOnStateCondition(PlayerStates newState)
    {
        bool abortStateTransition = false;

            switch (newState)
            {
                case PlayerStates.ForwardMovement:
                    break;

                case PlayerStates.LeftMovement:
                    // if (!CanSwingWeapon)
                    // {
                    //     abortStateTransition = true;
                    // }
                    break;

                case PlayerStates.RightMovement:
                    // if(IsJumping || currentState.Equals(PlayerStates.Stunned))
                    // {
                    //     abortStateTransition = true;
                    // }
                    break;

                case PlayerStates.Falling:
               
                    break;

                case PlayerStates.Dead:
           
                    break;
            }
      
        // Value of true means 'Abort'. Value of false means 'Continue'.
        return abortStateTransition;
    }
}
