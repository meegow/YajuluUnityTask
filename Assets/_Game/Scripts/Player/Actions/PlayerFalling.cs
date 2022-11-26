using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    public bool StartFalling{ get; set; }

    private float timeBetweenDamageChecksHolder;
    private IDamageable idamageable;
    [SerializeField] private int fallingDamage;
    [SerializeField] private float timeBetweenDamageChecks;

    private void Awake() 
    {
        timeBetweenDamageChecksHolder = timeBetweenDamageChecks;

        if(gameObject.TryGetComponent(out IDamageable idamageable))
        {
            this.idamageable = idamageable;
        }
    }


    void Update() 
    {
        if(!StartFalling)
        {
            timeBetweenDamageChecksHolder = timeBetweenDamageChecks;
            return;
        }

        timeBetweenDamageChecksHolder -= Time.deltaTime;

        if(timeBetweenDamageChecksHolder <= 0)
        {
            idamageable.AddDamage(fallingDamage);
            timeBetweenDamageChecksHolder = timeBetweenDamageChecks;
        }
    }
}
