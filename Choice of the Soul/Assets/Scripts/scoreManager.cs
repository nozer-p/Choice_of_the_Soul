using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public int score;
    public Text scoreDisplay;

    void Start()
    {
        scoreDisplay.text = "Score: 0";
    }

    private void Update()
    {
        scoreDisplay.text = "Score: " + score.ToString();
    }

    public void Kill()
    {
        score+=10;
    }
}
