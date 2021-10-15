using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;


public class GameController : MonoBehaviour
{
    public int whoseTurn;
    public int turnCounter;
    public GameObject[] turnIcons;
    public Sprite[] playIcons;
    public Button[] playableGridSpaces;
    public int[] markedSpaces;
    public bool win;
    public Text winnerMessage;
    public GameObject[] winningLines;
    public int winningSolutionIndex;
    public GameObject winnerPanel;
    public GameObject endGameMenu;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        GameSetup();
        RequestAds();
    }


    void GameSetup()
    {
        TurnInicialization();
        GridInicialization();
        MarkedSpacesInicialization();
        //RequestAds();

    }
    void TurnInicialization()
    {
        whoseTurn = 1;
        turnCounter = 0;
        win = false;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
    }
    void GridInicialization()
    {
        for (int i = 0; i < playableGridSpaces.Length; i++)
        {
            playableGridSpaces[i].interactable = true;
            playableGridSpaces[i].GetComponent<Image>().sprite = null;
        }

    }
    void MarkedSpacesInicialization()
    {
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -100;
        }
    }

    public void TicTacToeButton(int WhichNumer)
    {
        playableGridSpaces[WhichNumer].image.sprite = playIcons[0];
        playableGridSpaces[WhichNumer].interactable = false;

        MarkSpaceWithPlayerNumber(WhichNumer);
        IncrementTurn();

        if (whoseTurn == 0)
        {
            MarkGridWithCircleSprite(WhichNumer);
            CheckForWin();
            if (win != true)
                ChangeTurnToCross();
            else if (win == true)
            {
                WinnerDispaly(winningSolutionIndex);
                TurnOnEndGameMenu();
                AddPoint();
            }
        }
        else if (whoseTurn == 1)
        {
            MarkGridWithCrossSprite(WhichNumer);
            CheckForWin();
            if (win != true)
                ChangeTurnToCircle();
            else if (win == true)
            {
                WinnerDispaly(winningSolutionIndex);
                TurnOnEndGameMenu();
                AddPoint();
            }
        }
        if ((turnCounter == 9) && (win != true))
        {
            DrawDisplay();
            TurnOnEndGameMenu();
        }
    }

    public void IncrementTurn()
    {
        turnCounter++;
    }
    public void MarkSpaceWithPlayerNumber(int PlayerNumber)
    {
        markedSpaces[PlayerNumber] = whoseTurn + 1;
    }
    public void MarkGridWithCircleSprite(int GridNumber)
    {
        playableGridSpaces[GridNumber].image.sprite = playIcons[1];
    }
    public void MarkGridWithCrossSprite(int GridNumber)
    {
        playableGridSpaces[GridNumber].image.sprite = playIcons[0];
    }
    void ChangeTurnToCircle()
    {
        whoseTurn = 0;
        turnIcons[0].SetActive(false);
        turnIcons[1].SetActive(true);
    }
    void ChangeTurnToCross()
    {
        whoseTurn = 1;
        turnIcons[0].SetActive(true); ;
        turnIcons[1].SetActive(false);
    }
    void CheckForWin()
    {
        int solution1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int solution2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int solution3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int solution4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int solution5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int solution6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int solution7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int solution8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        var solutions = new int[] { solution1, solution2, solution3, solution4, solution5,
        solution6, solution7, solution8};

        for (int solutionIndex = 0; solutionIndex < solutions.Length; solutionIndex++)
        {
            if ((solutions[solutionIndex] == 3) || ((solutions[solutionIndex] == 6)))
            {
                Debug.Log("PLAYER " + whoseTurn + " won!");
                winningSolutionIndex = solutionIndex;
                win = true;
                return;
            }
        }
    }
    void WinnerDispaly(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoseTurn == 0)
            winnerMessage.text = " PLAYER O WINS!";
        else if (whoseTurn == 1)
            winnerMessage.text = "PLAYER X WINS!";

        winningLines[indexIn].SetActive(true);
    }
    void DrawDisplay()
    {
        winnerPanel.gameObject.SetActive(true);
        winnerMessage.text = "DRAW!";
    }

    void TurnOnEndGameMenu()
    {
        endGameMenu.SetActive(true);
    }
    void TurnOffWinnerPanel()
    {
        winnerPanel.SetActive(false);
    }
    void TurnOffAllWinningLines()
    {
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
    }
    void TurnOffEndGameMenu()
    {
        endGameMenu.SetActive(false);
    }
    public void RestartGame()
    {
        GameSetup();
        TurnOffWinnerPanel();
        TurnOffAllWinningLines();
        TurnOffEndGameMenu();
        ShowInterstitialAd();
        RequestAds();

    }
    private void RequestInterstitialAd()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    private void RequestRewardedAd()
    {
        string adUnitId = "3940256099942544/5224354917";
        this.rewardedAd = new RewardedAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

    }
    public  void RequestAds()
    {
        RequestInterstitialAd();
        RequestRewardedAd();
    }
    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }

    }
    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    public void AddPoint()
    {
        ScoreControl.scoreControl.playerScore++;
    }
   

}




