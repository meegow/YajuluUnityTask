using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    private EnemyStates currentState = EnemyStates.Idle;
    private EnemyStates previousState = EnemyStates.Idle;

    [SerializeField] private Animator animator;
    [SerializeField] private EnemyDeathController deathController;

    public void ChangeEnemyState(EnemyStates newState)
    {
        if (checkIfAbortOnStateCondition(newState))
            return;

        if (!checkForValidStatePair(newState))
            return;

        switch (newState)
        {
            case EnemyStates.Idle:
                animator.Play(Constants.IDLE_ANIMATION);
                break;

            case EnemyStates.Dead:
                deathController.KillCharacter();
                break;
        }

        previousState = currentState;
        currentState = newState;
    } 

    bool checkForValidStatePair(EnemyStates newState)
    {
        bool returnVal = false;

        switch (currentState)
        {
            case EnemyStates.Idle:
                returnVal = true;
                break;

            case EnemyStates.Dead:
                // if (newState.Equals(EnemyStates.Run))
                //     returnVal = true;
                // else
                //     returnVal = false;
                returnVal = true;
                break;

        }
        return returnVal;
    }

    // check if there is any reason this state should not be allowed to begin.
    bool checkIfAbortOnStateCondition(EnemyStates newState)
    {
        bool abortStateTransition = false;

        switch (newState)
        {
            case EnemyStates.Idle:
                break;

            case EnemyStates.Dead:
        
                break;
        }

        // Value of true means 'Abort'. Value of false means 'Continue'.
        return abortStateTransition;
    }

}
