using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    public float fadeSpeed;
    public float timeToLoadMainMenu;
    public Image faderImage;
    public delegate void OnResetGame();
    public static OnResetGame onResetGame;
    public delegate void OnLoadMainMenu();
    public static OnLoadMainMenu onLoadMainMenu;

    private Color imageColor;
    private float fadeAmount;
   [SerializeField] private Text scoreText;
   [SerializeField] private FloatVariable score;

    void OnEnable() 
    {
        imageColor = faderImage.color;
        SetFinalScoreUI();
    }

    public void SetFinalScoreUI()
    {
        scoreText.text = score.FloatValue.ToString();
    }

    public void OpenMainMenu()
    {
        StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen()
    {
        while(faderImage.color.a < 1)
        {
            fadeAmount = faderImage.color.a + (fadeSpeed * Time.deltaTime);
            faderImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
            yield return null;
        }

        onResetGame?.Invoke();
        yield return new WaitForSeconds(timeToLoadMainMenu);

        while(faderImage.color.a > 0)
        {
            fadeAmount = faderImage.color.a - (fadeSpeed * Time.deltaTime);
            faderImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, fadeAmount);
            yield return null;
        }

        onLoadMainMenu?.Invoke();
    }
}
