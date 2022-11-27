using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNotification : MonoBehaviour
{
    public RectTransform rectTransform;
    public Transform target;
    public Text scoreAdditionText;

    private int distanceScoreAddition;
    private bool moveTowardsTarget;
    private Vector2 initialPosition;
    [SerializeField] private FloatVariable score;
    [SerializeField] private float moveSpeed;

    void OnEnable() 
    {
        initialPosition = this.rectTransform.position;
        moveTowardsTarget = true;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        score.FloatValue += distanceScoreAddition;
        moveTowardsTarget = false;
        this.rectTransform.position = initialPosition;
    }

    void Update()
    {
        if(!moveTowardsTarget)
        {
            return;
        }

        rectTransform.position = Vector2.MoveTowards(rectTransform.position, target.position,
             moveSpeed * Time.deltaTime);

        if(Vector2.Distance(rectTransform.position, target.position) < 1f)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetAddedScore(int value)
    {
        distanceScoreAddition = value;
    }
}
