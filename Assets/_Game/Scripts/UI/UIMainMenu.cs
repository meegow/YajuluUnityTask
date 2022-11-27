using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public delegate void OnStartGame();
    public static OnStartGame onStartGame;

    public void StartGame()
    {
        onStartGame?.Invoke();
    }
}
