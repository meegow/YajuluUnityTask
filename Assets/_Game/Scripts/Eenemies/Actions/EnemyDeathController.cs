using UnityEngine;
using PathologicalGames;

public class EnemyDeathController : MonoBehaviour, IOutOfScreenCollectable
{
    public void KillCharacter()
    {
        //TODO: Show Kill effect or death animation
        RemoveOutOfScreen();
    }

    public void RemoveOutOfScreen()
    {
        PoolManager.Pools[Constants.BOTS_POOL].Despawn(this.transform);
    }
}
