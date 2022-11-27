using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool startGamePlay;

    public delegate void OnInitializeGame();
    public static OnInitializeGame onInitializeGame;

    void OnEnable() 
    {
        UIMainMenu.onStartGame += OnStartGamePlay;
        PlayerStateController.onGameOver += GameOver;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() 
    {
        UIMainMenu.onStartGame -= OnStartGamePlay;
        PlayerStateController.onGameOver -= GameOver;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeGame();
    }
    
    void GameOver()
    {
        startGamePlay = false;
    }

    void OnStartGamePlay()
    {
        startGamePlay = true;
    }

    void InitializeGame()
    {
        onInitializeGame?.Invoke();
    }
}
