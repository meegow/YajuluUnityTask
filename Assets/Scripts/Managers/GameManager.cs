using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool startGamePlay;

    void OnEnable() 
    {
        PlayerHealth.onGameOver += GameOver;
    }

    void OnDisable() 
    {
        PlayerHealth.onGameOver -= GameOver;
    }

    void Start()
    {
        startGamePlay = true;
    }
    
    void GameOver()
    {
        startGamePlay = false;
    }

    void OnStartGamePlay()
    {
        startGamePlay = true;
    }
}
