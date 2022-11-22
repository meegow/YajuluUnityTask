using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Vector3 offsetFromPlayer;
    public Transform player;

    private Transform thisTrans;

    void Start()
    {
        thisTrans = transform;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        transform.position = player.position + offsetFromPlayer;
    }

    /// <summary>
    /// Set the player transform for the camera to follow
    /// </summary>
    /// <param name="player"></param>
    // public void SetPlayer(Transform player)
    // {
    //     thos.player = player;
    // }
}
