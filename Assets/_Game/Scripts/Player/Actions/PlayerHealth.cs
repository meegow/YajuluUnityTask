using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    private int maxHealth;

    public delegate void OnGameOver();
    public static OnGameOver onGameOver;

    void Awake()
    {
        maxHealth = health;
    }

    public void AddDamage(int damageAmount)
    {
        maxHealth -= damageAmount;
Debug.Log("maxhealth " + maxHealth);
        if(maxHealth <= 0)
        {
            onGameOver?.Invoke();
        }
    }
}
