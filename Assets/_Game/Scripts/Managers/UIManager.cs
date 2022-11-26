using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text distanceText;
    [SerializeField] private FloatVariable score;
    [SerializeField] private FloatVariable distance;

    void Update()
    {
        if(!GameManager.startGamePlay)
        {
            return;
        }

        SetScoreUI();
    }

    void SetScoreUI()
    {
        scoreText.text = score.FloatValue.ToString();
        distanceText.text = Mathf.Round(distance.FloatValue).ToString();
    }
}
