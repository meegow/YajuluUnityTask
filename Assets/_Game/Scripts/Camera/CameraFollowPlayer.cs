using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Vector3 offsetFromPlayer;

    private Transform thisTrans;
    [SerializeField] private GameObjectVariable player;

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

        transform.position = player.GameObjectTransform.position + offsetFromPlayer;
    }
}
