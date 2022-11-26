using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private int maxHealth;
    private PlayerStateController stateController;
    [SerializeField] private int health;

    public delegate void OnUpdatePlayerHealthBar(int health);
    public static OnUpdatePlayerHealthBar onUpdatePlayerHealthBar;

    public delegate void OnInitializePlayerHealthBar(int maxHealth);
    public static OnInitializePlayerHealthBar onInitializePlayerHealthBar;

    void Awake()
    {
        maxHealth = health;
        onInitializePlayerHealthBar?.Invoke(maxHealth);
        stateController = GetComponent<PlayerStateController>();
    }

    public void AddDamage(int damageAmount)
    {
        maxHealth -= damageAmount;
        onUpdatePlayerHealthBar?.Invoke(maxHealth);

        if(maxHealth <= 0)
        {
            stateController.ChangePlayerState(PlayerStates.Dead);
            return;
        }

        stateController.ChangePlayerState(PlayerStates.Hurt);
    }

    void ResetOnStartGamePlay()
    {
        maxHealth = health;
        onInitializePlayerHealthBar?.Invoke(maxHealth);
    }
   
}
