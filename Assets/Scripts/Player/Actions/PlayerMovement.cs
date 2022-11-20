using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    public void MovePlayer(Rigidbody rigidBody, int direction, bool isForwardMovement)
    {
        if(isForwardMovement)
        {
            rigidBody.MovePosition(transform.position + Vector3.forward * Time.deltaTime * movementSpeed);
            return;
        }

        rigidBody.MovePosition(transform.position + new Vector3(direction, 0f, 1f) * Time.deltaTime * movementSpeed);
    }
}
