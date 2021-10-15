using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class RewardAdControler : MonoBehaviour
{
    private RewardedAd rewardedAd;
    public GameObject rewardButton;
   

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestRewardedAd();
    }
    private void RequestRewardedAd()
    {

        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        this.rewardedAd = new RewardedAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }
    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }
    public void DoubleScore()
    {
        ScoreControl.scoreControl.playerScore = ScoreControl.scoreControl.playerScore * 2;
    }
  void Update()
    {
        if (rewardedAd.IsLoaded() == false)
        {
            rewardButton.SetActive(false);
        }
        else if (rewardedAd.IsLoaded() == true) 
            rewardButton.SetActive(true);
    }
}
