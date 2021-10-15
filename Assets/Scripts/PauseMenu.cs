using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject resumeButton;
    public GameObject pauseButton;

    public void StartPause()
    {
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void EndPause()
    {
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
    }



}
