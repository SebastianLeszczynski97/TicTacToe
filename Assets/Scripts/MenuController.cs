using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public GameObject rewardAdPopUp;
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void closeRewardAdPopUp()
    {
        rewardAdPopUp.SetActive(false);
    }
    public void openRewardAdPopUp()
    {
        rewardAdPopUp.SetActive(true);
    }
}
