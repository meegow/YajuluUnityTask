using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    private bool isGrounded;
    private Rigidbody rigidBody;
    [SerializeField] private PlayerStateController stateController;

    public void SetRigidBody(Rigidbody body)
    {
        rigidBody = body;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer(Constants.PLATFORM_LAYER)))
        {
            isGrounded = false;
            stateController.ChangePlayerState(PlayerStates.Grounded);
        }
    }

    void Update() 
    {
        if (rigidBody.velocity.y < -1 && !isGrounded)
        {
            isGrounded = true;
            stateController.ChangePlayerState(PlayerStates.Falling);
        }
    }
}
