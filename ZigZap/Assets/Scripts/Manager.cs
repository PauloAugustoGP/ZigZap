using System;
using System.Collections;
using System.Collections.Generic;
using FuseMisc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] Transform targetParent;
    [SerializeField] Player target;
    [SerializeField] Level level;

    [SerializeField]List<string> zoneIDS;
    enum ZoneOffset
    {
        zoneExtraAd,
        zoneDoubleCoin,
        zoneBonusCoin
    }

    public bool hasConnection;

    public bool extraAd;
    public bool dcAd;
    public bool bcAd;

    AsyncOperation async;


    void Awake()
    {
        FuseSDK.SessionStartReceived += FuseSDK_SessionStartReceived;
        FuseSDK.SessionLoginError += FuseSDK_SessionLoginError;

        FuseSDK.AdAvailabilityResponse += FuseSDK_AdAvailabilityResponse;
        FuseSDK.AdWillClose += FuseSDK_AdWillClose;
        FuseSDK.RewardedAdCompletedWithObject += FuseSDK_RewardedAdCompletedWithObject;

        //FuseSDK.IAPOfferAcceptedWithObject += FuseSDK_IAPOfferAcceptedWithObject;
        //FuseSDK.VirtualGoodsOfferAcceptedWithObject += FuseSDK_VirtualGoodsOfferAcceptedWithObject;

        //FuseSDK.PurchaseVerification += FuseSDK_PurchaseVerification;

        //FuseSDK.GameConfigurationReceived += FuseSDK_GameConfigurationReceived;

        //FuseSDK.NotificationAction += FuseSDK_NotificationAction;
        //FuseSDK.NotificationWillClose += FuseSDK_NotificationWillClose;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("InitialSetup"))
            SetupPlayerPrefs();

        StartCoroutine("Load");
    }

    private void OnDestroy()
    {
        FuseSDK.SessionStartReceived -= FuseSDK_SessionStartReceived;
        FuseSDK.SessionLoginError -= FuseSDK_SessionLoginError;

        FuseSDK.AdAvailabilityResponse -= FuseSDK_AdAvailabilityResponse;
        FuseSDK.AdWillClose -= FuseSDK_AdWillClose;
        FuseSDK.RewardedAdCompletedWithObject -= FuseSDK_RewardedAdCompletedWithObject;
    }

    private void FuseSDK_RewardedAdCompletedWithObject(RewardedInfo obj)
    {
        //user watched REWARDED video to completion
        if (obj.RewardItem == "Coins")
        {
            //give player coins
            //target.SubCoin(1000, obj.RewardAmount);
        }
        else if (obj.RewardItem == "Life")
        {
            //Save Player life
            target.extraLife -= obj.RewardAmount;

            target.ResetPlayer(level.GetCheckpointPosition(), level.GetCheckpointOrientation());

            target.ResumeRun();
        }
        else if (obj.RewardItem == "CoinDoubler")
        {
            //Double Coins
            target.DoubleCoin();
            target.ResumeRun();
        }
    }

    private void FuseSDK_AdWillClose()
    {
        //throw new NotImplementedException();
        //every time an ad is closed
    }

    private void FuseSDK_AdAvailabilityResponse(bool arg1, FuseError arg2)
    {
        //if an ad is available to be displayed
    }

    private void FuseSDK_SessionLoginError(FuseError obj)
    {
        //session start failed
        //throw new NotImplementedException();

        target.extraLife = 0;

        hasConnection = false;

        extraAd = true;
        dcAd = true;
        bcAd = true;
    }

    private void FuseSDK_SessionStartReceived()
    {
        //session start receiived
        // throw new NotImplementedException();

        target.extraLife = 1;

        hasConnection = true;

        extraAd = false;
        dcAd = false;
        bcAd = false;
    }

    IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("Game");

        async.allowSceneActivation = false;

        yield return async;
    }

    public void ChangeScene()
    {
        CompareHighscore();

        async.allowSceneActivation = true;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ShowExtraLifeAd()
    {
        extraAd = true;
        target.StopRun();

        int index = (int)ZoneOffset.zoneExtraAd;

        FuseSDK.PreloadAdForZoneID(zoneIDS[index]);

        FuseSDK.ShowAdForZoneID(zoneIDS[index]);
    }

    public void ShowDoubleCoinAd()
    {
        dcAd = true;
        target.StopRun();

        int index = (int)ZoneOffset.zoneDoubleCoin;

        FuseSDK.PreloadAdForZoneID(zoneIDS[index]);

        FuseSDK.ShowAdForZoneID(zoneIDS[index]);
    }

    public void ShowBonusCoinAd()
    {
        bcAd = true;

        int index = (int)ZoneOffset.zoneBonusCoin;

        FuseSDK.PreloadAdForZoneID(zoneIDS[index]);

        FuseSDK.ShowAdForZoneID(zoneIDS[index]);
    }

    void CompareHighscore()
    {
        int score = 0;
        score += (target.GetDistance() * 10);
        score += (target.GetCoins() * 2);
        score += (target.GetGems() * 5);

        if (score > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestDistance", target.GetDistance());
            PlayerPrefs.SetInt("BestCoins", target.GetCoins());
            PlayerPrefs.SetInt("BestGems", target.GetGems());
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    void SetupPlayerPrefs()
    {
        PlayerPrefs.SetInt("InitialSetup", 1);

        PlayerPrefs.SetInt("BestDistance", 0);
        PlayerPrefs.SetInt("BestCoins", 0);
        PlayerPrefs.SetInt("BestGems", 0);
        PlayerPrefs.SetInt("BestScore", 0);

        PlayerPrefs.SetInt("TotalCoins", 0);

        PlayerPrefs.SetInt("CurrentSkin", 0);

        PlayerPrefs.SetInt("Skin0", 1);
        PlayerPrefs.SetInt("Skin1", 0);
        PlayerPrefs.SetInt("Skin2", 0);
        PlayerPrefs.SetInt("Skin3", 0);
        PlayerPrefs.SetInt("Skin4", 0);
        PlayerPrefs.SetInt("Skin5", 0);
        PlayerPrefs.SetInt("Skin6", 0);
        PlayerPrefs.SetInt("Skin7", 0);
        PlayerPrefs.SetInt("Skin8", 0);
    }
}
