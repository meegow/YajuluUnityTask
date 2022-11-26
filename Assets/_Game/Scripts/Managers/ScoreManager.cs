using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int maxDistanceToIncreaseScoreHolder;
    [SerializeField] private float distanceSpeedMultiplier;
    [SerializeField] private int distanceScoreAddition;
    [SerializeField] private int maxDistanceToIncreaseScore;
    [SerializeField] private FloatVariable score;
    [SerializeField] private FloatVariable distance;
    
    void Awake()
    {
        LoadGameStart();
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


    void LoadGameStart()
    {
        maxDistanceToIncreaseScoreHolder = maxDistanceToIncreaseScore;
        distance.FloatValue = score.FloatValue = 0;
    }
}
