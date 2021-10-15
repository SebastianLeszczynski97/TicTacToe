using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public static ScoreControl scoreControl;
    public int playerScore;

    private void Awake()
    {
        if (scoreControl == null)
        {
            scoreControl = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (scoreControl != this)
        {
            Destroy(gameObject);
        }
    }
}
