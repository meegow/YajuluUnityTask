using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalMovementSpeed;
    [SerializeField] private float horizontalMovementSpeed;
    [SerializeField] private float playerTiltAngle;
    [SerializeField] private Transform player;

    public void MovePlayer(Rigidbody rigidBody, int direction, bool isForwardMovement)
    {
        if(isForwardMovement)
        {
            rigidBody.MovePosition(transform.position + Vector3.forward * Time.deltaTime * verticalMovementSpeed);
            player.rotation = Quaternion.Euler(0f, 0f, playerTiltAngle * direction);
            return;
        }

        rigidBody.MovePosition(transform.position + new Vector3(
            direction * horizontalMovementSpeed, 0f, verticalMovementSpeed) * Time.deltaTime );

        player.rotation = Quaternion.RotateTowards(player.rotation, 
            Quaternion.Euler(0f, 0f, playerTiltAngle * direction * -1), 10f);
    }
}
