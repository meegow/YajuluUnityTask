using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathController : MonoBehaviour
{
    public void KillCharacter()
    {
        //TODO: Show Kill effetc and animation
        Destroy(this.gameObject);
    }
}
