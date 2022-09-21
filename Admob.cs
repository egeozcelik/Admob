using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class Admob : MonoBehaviour
{
    [SerializeField] private const string _interstitialId = "";
    [SerializeField] private const string _rewardedId = "";


    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;
    private void Start()
    {
        RequestRewarded();
        RequestInterstitial();
    }
    #region Interstitial

    private void RequestInterstitial()
    {
        _interstitialAd = new InterstitialAd(_interstitialId);

        _interstitialAd.OnAdClosed += HandleAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(request);
    }


    private void HandleAdClosed(object sender, EventArgs e)
    {
        _interstitialAd.Destroy();
        RequestInterstitial();
    }

    public void DisplayInterstitial()
    {
        if (_interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
        }
    } 
    #endregion
    #region Rewarded

    private void RequestRewarded()
    {
        _rewardedAd = new RewardedAd(_rewardedId);

        _rewardedAd.OnUserEarnedReward += HandeUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        RequestRewarded();
    }

    private void HandeUserEarnedReward(object sender, Reward e)
    {
        Debug.Log("REWARD");
    }

    public void DisplayRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }

    private void OnDestroy()
    {
        _rewardedAd.OnUserEarnedReward -= HandeUserEarnedReward;
        _rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
        Debug.log("closed");
    } 
    #endregion
    

   

   

}
