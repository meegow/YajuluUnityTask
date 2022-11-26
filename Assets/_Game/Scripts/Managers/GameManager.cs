using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool startGamePlay;

    void OnEnable() 
    {
        PlayerStateController.onGameOver += GameOver;
    }

    void OnDisable() 
    {
        PlayerStateController.onGameOver -= GameOver;
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
