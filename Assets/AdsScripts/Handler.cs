using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
    public enum AdsLoadingStatus{
    NotLoaded,
    Loading,
    Loaded,
    NoInventory
}
public abstract class Handler : MonoBehaviour
{

    public delegate void RewardUserDelegate();

    public delegate void AfterLoading();

    public static AdsLoadingStatus rAdStatus = AdsLoadingStatus.NotLoaded;
    public static AdsLoadingStatus riAdStatus = AdsLoadingStatus.NotLoaded;

    public static AdsLoadingStatus iAdStatus = AdsLoadingStatus.NotLoaded;

    public static AdsLoadingStatus smallBannerStatus = AdsLoadingStatus.NotLoaded;
    public static AdsLoadingStatus small2ndBannerStatus = AdsLoadingStatus.NotLoaded;
    public static AdsLoadingStatus mediumBannerStatus = AdsLoadingStatus.NotLoaded;
    public abstract bool IsInterstitialAdReady();
    public abstract void ShowInterstitialAd();

    public abstract void LoadInterstitialAd();


    public abstract bool IsSmallFirstBannerReady();
    public abstract void LoadFirstSmallBanner();

    public abstract void ShowSmallFirstBanner(AdPosition pos);
    public abstract void HideFirstSmallBannerEvent();

    //2nd Banner
    public abstract bool IsSecondBannerReady();
    public abstract void LoadSecondSmallBanner();

    public abstract void ShowSecondBanner(AdPosition pos);
    public abstract void HideSecondSmallBannerEvent();
    public abstract bool IsMediumBannerReady();

    public abstract void LoadMediumBanner();
    public abstract void ShowMediumBanner(AdPosition pos);
    public abstract void HideMediumBannerEvent();

    public abstract bool IsRewardedAdReady();
    public abstract void LoadRewardedVideo();

    public abstract void ShowRewardedVideo(RewardUserDelegate _delegate);

    public abstract void LoadRewardedInterstitial();

    public abstract void ShowRewardedInterstitialAd(RewardUserDelegate _delegate);
    public abstract bool IsRewardedInterstitialAdReady();

    public GameObject Loading;

    int a = 0;
    public int SystemRaM = 1024;
    public void ShowInt(AfterLoading afterLoading, bool IsTimeScaled = false)
    { 
        if (SystemRaM <=1024)
        {
            return;
        }
      
        Loading.GetComponent<LoadingAds>().IsTimeScaled = IsTimeScaled;
        LoadingAds.Notify = afterLoading;
        Loading.SetActive(true);
    }



    public void ShowRewardedAdsBoth(RewardUserDelegate _delegate)
    {
        if (a == 0)
        {
            ShowRewardedVideo(_delegate);
            a = 1;
        }
        else if (a == 1)
        {
            ShowRewardedInterstitialAd(_delegate);
            a = 0;
        }
    }
    public void ShowRewardVideo()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        ShowRewardedAdsBoth(GiveReward);
    }

    public void GiveReward()
    {
        Debug.Log("RewardGiven");
        Data.RewardedAdWatched();
    }
}
