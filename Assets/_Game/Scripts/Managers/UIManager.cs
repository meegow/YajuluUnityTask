using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static bool isGamePlayCanvasActive;

    private GameObject mainMenuCanvasInstance;
    private GameObject gamePlayCanvasInstance;
    private GameObject gameOverCanvasInstance;
    [Space]
    [Header("UI Variables")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gamePlayCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    void OnEnable()
    {
        UIMainMenu.onStartGame += StartGamePlay;
        UIGameOver.onLoadMainMenu += InitializeUI;
        PlayerStateController.onGameOver += GameOver;
        GameManager.onInitializeGame += InitializeUI;
    }

    void OnDisable()
    {
        UIMainMenu.onStartGame -= StartGamePlay;
        UIGameOver.onLoadMainMenu -= InitializeUI;
        PlayerStateController.onGameOver += GameOver;
        GameManager.onInitializeGame -= InitializeUI;
    }

    void InitializeUI()
    {
        if(gameOverCanvasInstance != null)
        {
            Destroy(gameOverCanvasInstance);
        }

        mainMenuCanvasInstance = Instantiate(mainMenuCanvas);
    }

    void StartGamePlay()
    {
        if(mainMenuCanvasInstance != null)
        {
            Destroy(mainMenuCanvasInstance);
        }

        gamePlayCanvasInstance = Instantiate(gamePlayCanvas);
        isGamePlayCanvasActive = true;
    }

      void GameOver()
    {
        if(gamePlayCanvasInstance != null)
        {
            Destroy(gamePlayCanvasInstance);
            isGamePlayCanvasActive = false;
        }

        gameOverCanvasInstance = Instantiate(gameOverCanvas);
    }
}
