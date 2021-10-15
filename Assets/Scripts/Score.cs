using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score; 

    void Update()
    {
        score.text = "High Score: " + ScoreControl.scoreControl.playerScore;
    }
}
