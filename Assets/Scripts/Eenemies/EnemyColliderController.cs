using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderController : MonoBehaviour
{
    public int damageToPlayer;

    private EnemyStateController stateController;

    void Awake()
    {
        stateController = GetComponent<EnemyStateController>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " );
        if(other.gameObject.TryGetComponent(out IDamageable idamageable))
        {
            idamageable.AddDamage(damageToPlayer);
            stateController.ChangeEnemyState(EnemyStates.Dead);
        }
    }
}
