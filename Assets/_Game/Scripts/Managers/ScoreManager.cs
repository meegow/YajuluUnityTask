using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int maxDistanceToIncreaseScoreHolder;
    [SerializeField] private float distanceSpeedMultiplier;
    [SerializeField] private int distanceScoreAddition;
    [SerializeField] private int maxDistanceToIncreaseScore;
    [SerializeField] private FloatVariable score;
    [SerializeField] private FloatVariable distance;
    
    void OnEnable()
    {
        UIMainMenu.onStartGame += ResetOnStartGamePlay;
    }

    void OnDisable()
    {
        UIMainMenu.onStartGame -= ResetOnStartGamePlay;
    }

    void Update()
    {
        if(!GameManager.startGamePlay)
        {
            return;
        }

        CalculateScore();
    }

    void CalculateScore()
    {
        distance.FloatValue += distanceSpeedMultiplier * Time.deltaTime;
   
        if(Mathf.Round(distance.FloatValue) == maxDistanceToIncreaseScoreHolder && Mathf.Round(distance.FloatValue) > 1)
        {
            maxDistanceToIncreaseScoreHolder += maxDistanceToIncreaseScore;
            score.FloatValue += distanceScoreAddition;
        }
    }

    void ResetOnStartGamePlay()
    {
        maxDistanceToIncreaseScoreHolder = maxDistanceToIncreaseScore;
        distance.FloatValue = score.FloatValue = 0;
    }
}
