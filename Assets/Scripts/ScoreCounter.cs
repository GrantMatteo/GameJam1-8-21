using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    public float score;
    public Text textBox;

    void Start()
    {
        textBox.text = "Score: " + Mathf.Round(score).ToString();
    }

    public void addScore(float modifier)
    {
        score = score + modifier;
    }

    public void setScore(float newValue)
    {
        score = newValue;
    }

    public float getScore()
    {
        return score;
    }

    //Score is time in seconds at the moment
    void Update()
    {
        score += Time.deltaTime;
        textBox.text = "Score: " + Mathf.Round(score).ToString();
    }
}
