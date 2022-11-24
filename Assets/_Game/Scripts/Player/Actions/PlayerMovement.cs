using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float verticalMovementSpeed;
    [SerializeField] private float horizontalMovementSpeed;

    public void MovePlayer(Rigidbody rigidBody, int direction, bool isForwardMovement)
    {
        if(isForwardMovement)
        {
            rigidBody.MovePosition(transform.position + Vector3.forward * Time.deltaTime * verticalMovementSpeed);
            return;
        }
Debug.Log("MovePlayer 1");
        rigidBody.MovePosition(transform.position + new Vector3(
            direction * horizontalMovementSpeed, 0f, verticalMovementSpeed) * Time.deltaTime );
    }
}
