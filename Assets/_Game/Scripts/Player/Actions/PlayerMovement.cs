using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStateController stateController;
    [SerializeField] private float verticalMovementSpeed;
    [SerializeField] private float horizontalMovementSpeed;
    [SerializeField] private float playerTiltAngle;
    [SerializeField] private float horizontalMovementSpeedOnFallingMultiplier;
    [SerializeField] private Transform player;

    void Awake()
    {
        stateController = GetComponent<PlayerStateController>();
    }

    public void MovePlayer(Rigidbody rigidBody, int direction, bool isForwardMovement)
    {
        if(isForwardMovement)
        {
            rigidBody.MovePosition(transform.position + Vector3.forward * Time.deltaTime * verticalMovementSpeed);
            player.rotation = Quaternion.Euler(0f, 0f, playerTiltAngle * direction);
            return;
        }

        float horizontalSpeed = horizontalMovementSpeed;

        if(!stateController.IsPlayerGrounded())
        {
            horizontalSpeed = horizontalMovementSpeed * horizontalMovementSpeedOnFallingMultiplier;
        }

        rigidBody.MovePosition(transform.position + new Vector3(
            direction * horizontalSpeed, 0f, verticalMovementSpeed) * Time.deltaTime );

        player.rotation = Quaternion.RotateTowards(player.rotation, 
            Quaternion.Euler(0f, 0f, playerTiltAngle * direction * -1), 10f);
    }
}