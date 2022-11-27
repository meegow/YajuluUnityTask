using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    [Header("Health Bar State Colors")]
    public Color healthBarFull;
    public Color healthBarHalf;

    private int maxHealth;
    private Image healthBarImage;
    [Space]
    [Header("UI Variables")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text distanceText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private ScoreNotification scoreNotification;
    [Space]
    [Header("Scriptable Variables")]
    [SerializeField] private FloatVariable score;
    [SerializeField] private FloatVariable distance;

    void OnEnable() 
    {
        healthBarImage = healthSlider.fillRect.GetComponent<Image>();
        scoreText.text = distanceText.text = "0";

        PlayerHealth.onUpdatePlayerHealthBar += UpdatePlayerHealthUI;
        PlayerHealth.onInitializePlayerHealthBar += InitializePlayerHealthUI;
        ScoreManager.onScoreIncrease += DisplayScoreNotification;
    }

    void OnDisable()
    {
        PlayerHealth.onUpdatePlayerHealthBar -= UpdatePlayerHealthUI;
        PlayerHealth.onInitializePlayerHealthBar -= InitializePlayerHealthUI;
        ScoreManager.onScoreIncrease -= DisplayScoreNotification;
    }

    void Update()
    {
        if(!GameManager.startGamePlay)
        {
            return;
        }

        SetScoreUI();
    }

    void UpdatePlayerHealthUI(int health)
    {
        healthSlider.value = health;

        if (healthSlider.value <= maxHealth / 2)
        {
            healthBarImage.color = healthBarHalf;
        }
    }

    void InitializePlayerHealthUI(int maxHealth)
    {
        this.maxHealth = maxHealth;
        healthSlider.value = maxHealth;
        healthBarImage.color = healthBarFull;
    }

    void SetScoreUI()
    {
        scoreText.text = score.FloatValue.ToString();
        distanceText.text = Mathf.Round(distance.FloatValue).ToString();
    }

    void DisplayScoreNotification(int addedValue)
    {
        scoreNotification.gameObject.SetActive(true);
        scoreNotification.GetComponent<ScoreNotification>().SetAddedScore(addedValue);
    }
}
