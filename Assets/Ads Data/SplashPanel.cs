﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public bl_SceneLoader bl_SceneLoader;


    void Awake() {

        GameAnalyticsSDK.GameAnalytics.Initialize();
    
    }
    
    void Start()
    {
        StartCoroutine("changeScene");
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(0.1f);
        AdmobAdsManager.Instance.LoadInterstitialAd();
        bl_SceneLoader.LoadLevel("MenuScene");
    }
}
