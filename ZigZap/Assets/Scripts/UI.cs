using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]List<GameObject> uiPanels;

    //Start Panel
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject highscoreStatistics;
    [SerializeField] Text bestDistance;
    [SerializeField] Text bestCcoins;
    [SerializeField] Text bestGems;
    [SerializeField] Text bestScore;

    //Shop Panel

    //Game Panel
    [SerializeField]Text distance;
    [SerializeField]Text coins;
    [SerializeField]Text gems;

    //Ads Panel
    [SerializeField]Text counter;
    [SerializeField]Image counterImage;

    //Statistics Panel
    [SerializeField]Text distanceAmount;
    [SerializeField]Text coinsAmount;
    [SerializeField]Text gemsAmount;
    [SerializeField]Text finalScore;
    [SerializeField]Button doubleAd;

    [SerializeField]Player target;

    [SerializeField]Manager manager;

    float timer;

    enum UIstage
    {
        startPanel,
        shopPanel,
        gamePanel,
        extraLifePanel,
        statisticsPanel
    }
    UIstage stage;

    bool bShowHighscore;
    bool bGame;
    bool bDone;

    void Start()
    {
        stage = UIstage.startPanel;

        timer = 5.0f;

        bShowHighscore = false;
        bGame = false;
    }

    void Update ()
    {
        ActivatePanel((int)stage);

        switch(stage)
        {
            case UIstage.startPanel:
                {
                    if (bShowHighscore)
                        mainPanel.SetActive(false);
                    else
                        mainPanel.SetActive(true);
                    

                    bestDistance.text = "" + PlayerPrefs.GetInt("BestDistance");
                    bestCcoins.text = "" + PlayerPrefs.GetInt("BestCoins");
                    bestGems.text = "" + PlayerPrefs.GetInt("BestGems");
                    bestScore.text = "" + PlayerPrefs.GetInt("BestScore");
                    

                    if (bGame)
                    {
                        stage = UIstage.gamePanel;
                        target.StartRun();
                    }
                }
                break;

            case UIstage.shopPanel:
                {
                    
                }
                break;

            case UIstage.gamePanel:
                {
                    if(target.dead)
                    {
                        if (target.extraLife == 1)
                            stage = UIstage.extraLifePanel;
                        else
                            stage = UIstage.statisticsPanel;
                    }
                    else
                    {
                        distance.text = "" + target.GetDistance();
                        coins.text = "" + target.GetCoins();
                        gems.text = "" + target.GetGems();
                    }
                }break;

            case UIstage.extraLifePanel:
                {
                    timer -= Time.deltaTime;
                    counter.text = "" + (int)timer;

                    counterImage.fillAmount = timer / 5;

                    if(manager.extraAd)
                    {
                        timer = 5;
                        
                        stage = UIstage.gamePanel;
                    }

                    if (timer <= 0)
                        stage = UIstage.statisticsPanel;
                }
                break;

            case UIstage.statisticsPanel:
                {
                    if (manager.dcAd) doubleAd.interactable = false;

                    distanceAmount.text = "" + target.GetDistance();
                    coinsAmount.text = "" + target.GetCoins();
                    gemsAmount.text = "" + target.GetGems();

                    int score = 0;
                    score += (target.GetDistance() * 10);
                    score += (target.GetCoins() * 2);
                    score += (target.GetGems() * 5);

                    finalScore.text = "" + score;
                }
                break;
        }
	}

    void ActivatePanel(int panel)
    {
        for (int i = 0; i < uiPanels.Count; i++)
        {
            if (i == panel)
                uiPanels[i].SetActive(true);
            else
                uiPanels[i].SetActive(false);
        }
    }

    public void ShowOrHideHighscoreStatistics()
    {
        if (bShowHighscore) bShowHighscore = false;
        else bShowHighscore = true;
    }

    public void OpenShop()
    {
        if (stage == UIstage.startPanel)
            stage = UIstage.shopPanel;
        else if (stage == UIstage.shopPanel)
            stage = UIstage.startPanel;
    }

    public void RunGame()
    {
        bGame = true;
    }
}
