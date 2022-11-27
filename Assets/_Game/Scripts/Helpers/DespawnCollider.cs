using UnityEngine;

public class DespawnCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IOutOfScreenCollectable outOfScreenCollectable))
        {
            outOfScreenCollectable.RemoveOutOfScreen();
        }
    }
}
