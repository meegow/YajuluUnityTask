﻿using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public bool IsGrounded{ get; set; }
    private PlayerStates currentState = PlayerStates.ForwardMovement;
    private PlayerStates previousState = PlayerStates.ForwardMovement;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private PlayerStateVariable currentPlayerState;
    [SerializeField] private PlayerGroundCollider groundCollider;

    [Header("Player Actions")]
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        groundCollider.SetRigidBody(rigidBody);
    }

    void FixedUpdate()
    {
        PlayerStateUpdates();
    }
    
    void PlayerStateUpdates()
    {
        switch (currentState)
        {
            case PlayerStates.ForwardMovement:
                playerMovement.MovePlayer(rigidBody, 0, true);
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
        if(newState == currentState)
            return;

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
                if(IsGrounded)
                {
                    animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                }
                break;

            case PlayerStates.RightMovement:
                if(IsGrounded)
                {
                    animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                }
                
                break;

            case PlayerStates.Falling:
                IsGrounded = false;
                animator.Play(Constants.PLAYER_FALLING_ANIMATION);
                break;

             case PlayerStates.Grounded:
                IsGrounded = true;
                break;

            case PlayerStates.Dead:
               
                break;
        }

        previousState = currentState;
        currentState = newState;
        currentPlayerState.CurrentSate = currentState;
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
                returnVal = true;
                break;

            case PlayerStates.RightMovement:
                returnVal = true;
                break;

            case PlayerStates.Falling:
                // The only states that can take over from Falling
                if (newState.Equals(PlayerStates.RightMovement) || 
                    newState.Equals(PlayerStates.LeftMovement) || 
                    newState.Equals(PlayerStates.Grounded))
                    returnVal = true;
                else
                    returnVal = false;

                break;

             case PlayerStates.Grounded:
                returnVal = true;
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
                    if(!IsGrounded)
                    {
                        abortStateTransition = true;
                    }
                    break;

                case PlayerStates.LeftMovement:
               
                    break;

                case PlayerStates.RightMovement:
                
                    break;

                case PlayerStates.Falling:
               
                    break;

                case PlayerStates.Grounded:
                
                break;

                case PlayerStates.Dead:
           
                    break;
            }
      
        // Value of true means 'Abort'. Value of false means 'Continue'.
        return abortStateTransition;
    }
}
