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
        stateController = GetComponent<PlayerStateController>();
    }

    IEnumerator InitializeHealthBar()
    {
        yield return new WaitUntil(() => UIManager.isGamePlayCanvasActive);
        onInitializePlayerHealthBar?.Invoke(maxHealth);
    }

    public void InitializeHealth()
    {
        maxHealth = health;
        StartCoroutine(InitializeHealthBar());
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
}
