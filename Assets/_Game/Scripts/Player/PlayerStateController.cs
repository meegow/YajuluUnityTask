using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public bool isAnimationFinishPlaying{ get; set; }
    public delegate void OnGameOver();
    public static OnGameOver onGameOver;

    private bool checkForIdleAnim;
    private Vector3 initialPosition;
    private Vector3 modelOriginPosition;
    private PlayerStates currentState = PlayerStates.ForwardMovement;
    private PlayerStates previousState = PlayerStates.ForwardMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform model;
    [SerializeField] private GameObjectVariable player;
    [SerializeField] private PlayerGroundCollider groundCollider;

    [Header("Player Actions")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerFalling playerFalling;
    [SerializeField] private PlayerHealth playerHealth;
    
    void OnEnable()
    {
        UIGameOver.onResetGame += RevivePlayer;
        UIMainMenu.onStartGame += InitializePlayerOnGameStart;
    }

    void OnDisable()
    {
        UIGameOver.onResetGame += RevivePlayer;
        UIMainMenu.onStartGame -= InitializePlayerOnGameStart;
    }

    void Awake()
    {
        player.ThisGameObject = this.gameObject;
        initialPosition = this.transform.position;
        modelOriginPosition = model.transform.localPosition;
        isAnimationFinishPlaying = true;
        groundCollider.SetRigidBody(rigidBody);
    }

    void FixedUpdate()
    {
        PlayerStateUpdates();
    }
    
    void PlayerStateUpdates()
    {
        FixPlayerModelPositionAfterAnimFinishPlaying();
        
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
                if(isAnimationFinishPlaying)
                {
                    animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                }
                break;

            case PlayerStates.LeftMovement:
                if(playerFalling.IsGrounded && isAnimationFinishPlaying)
                {
                    animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                }
                break;

            case PlayerStates.RightMovement:
                if(playerFalling.IsGrounded && isAnimationFinishPlaying)
                {
                    animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                }
                break;

            case PlayerStates.Hurt:
                animator.Play(Constants.PLAYER_HURT_ANIMATION);
                isAnimationFinishPlaying = false;
                checkForIdleAnim = true;
                break;

            case PlayerStates.Falling:
                playerFalling.PlayerFallingState(true);
                animator.Play(Constants.PLAYER_FALLING_ANIMATION);
                break;

             case PlayerStates.Grounded:
                playerFalling.PlayerFallingState(false);
                break;

            case PlayerStates.Dead:
                animator.Play(Constants.PLAYER_DEATH_ANIMATION);
                playerFalling.PlayerFallingState(false);
                rigidBody.useGravity = false;
                rigidBody.velocity = Vector3.zero;
                StartCoroutine(GameOverDelay());
                break;

            case PlayerStates.Revive:
                isAnimationFinishPlaying = true;
                checkForIdleAnim = false;
                model.transform.localPosition = modelOriginPosition;
                animator.Play(Constants.PLAYER_IDLE_ANIMATION);
                this.transform.position = initialPosition;
                rigidBody.useGravity = true;
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
                returnVal = true;
                break;

            case PlayerStates.RightMovement:
                returnVal = true;
                break;

               case PlayerStates.Hurt:
                returnVal = true;            
                break;

            case PlayerStates.Falling:
                // The only states that can take over from Falling
                if (newState.Equals(PlayerStates.RightMovement) || 
                    newState.Equals(PlayerStates.LeftMovement) ||
                    newState.Equals(PlayerStates.Hurt) || 
                    newState.Equals(PlayerStates.Dead) ||
                    newState.Equals(PlayerStates.Grounded))
                    returnVal = true;
                else
                    returnVal = false;
                break;

             case PlayerStates.Grounded:
                returnVal = true;
                break;

            case PlayerStates.Dead:
                if (newState.Equals(PlayerStates.Revive))
                    returnVal = true;
                else
                    returnVal = false;
                break;

            case PlayerStates.Revive:
                if (newState.Equals(PlayerStates.ForwardMovement))
                    returnVal = true;
                else
                    returnVal = false;
                break;

        }
        return returnVal;
    }

    /// <summary>
    /// Check if there is any reason this state should not be allowed to begin.
    /// </summary>
    /// <param name="newState">the new state of the player to transition to</param>
    /// <returns>Value of true means 'Abort'. Value of false means 'Continue'.</returns>
    bool checkIfAbortOnStateCondition(PlayerStates newState)
    {
        bool abortStateTransition = false;

        switch (newState)
        {
            case PlayerStates.ForwardMovement:
                if(!playerFalling.IsGrounded)
                {
                    abortStateTransition = true;
                }
                break;

            case PlayerStates.LeftMovement:
            
                break;

            case PlayerStates.RightMovement:
            
                break;

            case PlayerStates.Hurt:        
                break;

            case PlayerStates.Falling:
                if(!isAnimationFinishPlaying)
                {
                    abortStateTransition = true;
                }
                break;

            case PlayerStates.Grounded:
                break;

            case PlayerStates.Dead:
                break;

            case PlayerStates.Revive:
                break;
        }
    
        return abortStateTransition;
    }

    /// <summary>
    /// Handles special cases and workaround for the following cases:
    /// 1- Hurt animation makes model offset on z-axis after finish playing
    /// 2- Handle playing the forward animation after finish playing hurt animation
    /// </summary>
    void FixPlayerModelPositionAfterAnimFinishPlaying()
    {
        if(model.transform.localPosition != modelOriginPosition && ((playerFalling.IsGrounded
            && isAnimationFinishPlaying) || (!playerFalling.IsGrounded && isAnimationFinishPlaying)))
        {
            model.transform.localPosition = modelOriginPosition;
        }

        if(!checkForIdleAnim)
        {
            return;
        }

        if(isAnimationFinishPlaying & currentState.Equals(PlayerStates.ForwardMovement))
        {
            checkForIdleAnim = false;
            animator.Play(Constants.PLAYER_IDLE_ANIMATION);
        }
    }

    void InitializePlayerOnGameStart()
    {
        playerHealth.InitializeHealth();
    }

    void RevivePlayer()
    {
        ChangePlayerState(PlayerStates.Revive);
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.5f);
        onGameOver?.Invoke();
    }

    public bool IsPlayerGrounded()
    {
        return playerFalling.IsGrounded;
    }
}
