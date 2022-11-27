using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    public bool IsGrounded{ get; set; }

    private bool startFalling;
    private float timeBetweenDamageChecksHolder;
    private IDamageable idamageable;
    [SerializeField] private int fallingDamage;
    [SerializeField] private float timeBetweenDamageChecks;

    void Awake() 
    {
        timeBetweenDamageChecksHolder = timeBetweenDamageChecks;

        if(gameObject.TryGetComponent(out IDamageable idamageable))
        {
            this.idamageable = idamageable;
        }
    }

    void Update() 
    {
        if(!startFalling)
        {
            return;
        }

        timeBetweenDamageChecksHolder -= Time.deltaTime;

        if(timeBetweenDamageChecksHolder <= 0)
        {
            idamageable.AddDamage(fallingDamage);
            timeBetweenDamageChecksHolder = timeBetweenDamageChecks;
        }
    }

    public void PlayerFallingState(bool isFalling)
    {
        startFalling = isFalling;
        IsGrounded = true;

        if(isFalling)
        {
            IsGrounded = false;
            timeBetweenDamageChecksHolder = timeBetweenDamageChecks;
        }
    }
}
