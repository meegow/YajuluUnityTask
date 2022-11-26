using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private int maxHealth;
    private PlayerStateController stateController;
    [SerializeField] private int health;

    public delegate void OnGameOver();
    public static OnGameOver onGameOver;

    void Awake()
    {
        maxHealth = health;
        stateController = GetComponent<PlayerStateController>();
    }

    public void AddDamage(int damageAmount)
    {
        maxHealth -= damageAmount;

        if(maxHealth <= 0)
        {
            stateController.ChangePlayerState(PlayerStates.Dead);
            onGameOver?.Invoke();
            return;
        }

        stateController.ChangePlayerState(PlayerStates.Hurt);
    }
}
