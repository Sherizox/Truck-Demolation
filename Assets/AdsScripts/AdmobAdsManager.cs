


using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Advertisements;



[Serializable]
public class ADMobID
{
    [Header("Unity ID")]
    public string UnityId;
    [Header("Unity Interstitial_ID")]
    public string Unity_Interstitial_ID;
    [Header("Unity RewardedVideo_ID")]
    public string Unity_RewardedVideo;
    [Header("AdMobAppID")]
    public string AdmobAPPID;

    [Header("Intersitial_High_Ecpm")]
    public string Intersitial_High_Ecpm;

    [Header("Intersitial_Medium_Ecpm")]
    public string Intersitial_Medium_Ecpm;

    [Header("Intersitial_Low_Ecpm")]
    public string Intersitial_Low_Ecpm;

   
    [HideInInspector]
    public string smallBanner_First_High_Ecpm;

    [Header("Small_banners_First_Medium_Ecpm")]
    public string SmallBanner_First_Medium_Ecpm;

    [Header("Small_banners_First_Low_Ecpm")]
    public string Small_banners_First_Ecpm;

    [HideInInspector]
    public string smallBanner_Second_High_Ecpm;

    [Header("Small_banners_Second_Medium_Ecpm")]
    public string SmallBanner_Second_Medium_Ecpm;

    [Header("Small_banners_Second_Low_Ecpm")]
    public string Small_banners_Second_Ecpm;

   
    [HideInInspector]
    public string MediumBanner_High_Ecpm;

    [Header("MediumBanner_Medium_Ecpm")]
    public string MediumBanner_Medium_Ecpm;
    [Header("MediumBanner_Low_Ecpm")]
    public string MediumBanner_Ecpm;

    [Header("RewardedVideo_High_Ecpm")]
    public string RewardedVideo_High_Ecpm;
    [Header("RewardedVideo_Medium_Ecpm")]
    public string RewardedVideo_Medium_Ecpm;
    [Header("RewardedVideo_Low_Ecpm")]
    public string RewardedVideo_Low_Ecpm;
    [Header("RewardedInt_High_Ecpm")]
    public string RewardedInt_High_Ecpm;
    [Header("RewardedInt_Medium_Ecpm")]
    public string RewardedInt_Medium_Ecpm;
    [Header("RewardedInt_Low_Ecpm")]
    public string RewardedInt_Low_Ecpm;
}




public class AdmobAdsManager : Handler, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public bool EnableTestModeAds;

    //public string FireBaseIntersitialHighEcpm;
    //public string FireBaseIntersitialMediumEcpm;

    //// public string FireBaseFirstBannerHighEcpm;
    //public string FireBaseFirstSmallBannerMediumEcpm;


    //// public string FireBaseSecondBannerHighEcpm;
    //public string FireBaseSecondSmallBannerMediumEcpm;

    //public string FireBaseMediumBannerMediumEcpm;
#if UNITY_IOS
    public string ADSSwitch;
#endif
    private bool ServerAds = true;

    public ADMobID AndroidAdmob_ID = new ADMobID();
    public ADMobID IosAndroid_ID = new ADMobID();
    public ADMobID TestAdmob_ID = new ADMobID();
    [HideInInspector]
    public ADMobID ADMOB_ID = new ADMobID();

    public static bool isSmallBannerLoadedFirst = false;
    public static bool isSmallBannerLoadedSecond = false;
    public static bool isMediumBannerLoaded = false;
    bool isAdmobInitialized = false;

    #region IntersitialAds_Var
    [HideInInspector]
    public InterstitialAd Interstitial_High_Ecpm;

    [HideInInspector]
    public InterstitialAd Interstitial_Medium_Ecpm;

    [HideInInspector]
    public InterstitialAd Intersitial_Low_Ecpm;


    public delegate void InterstitialMediumEcpm();
    public static event InterstitialMediumEcpm Int_Medium_Ecpm;

    public delegate void InterstitialLowEcpm();
    public static event InterstitialLowEcpm Int_Low_Ecpm;
    public delegate void InterstitialUnity();
    public static event InterstitialUnity Int_Unity;

    public static bool Interstitial_HighEcpm = true, Interstitial_MediumEcpm = false, Interstitial_LowEcpm = false, UnityAds = false;


    #endregion

    #region SmallBanner_Var
    [HideInInspector]
    public BannerView SmallBanner_First_ECPM;
    [HideInInspector]
    public BannerView SmallBanner_First_High_Ecpm;
    [HideInInspector]
    public BannerView SmallBanner_First_Medium_Ecpm;

    public delegate void SmallBannerFirstMediumEcpm();
    public static event SmallBannerFirstMediumEcpm First_Small_Banner_Medium_Ecpm;

    public delegate void SmallFirstBannerLow();
    public static event SmallFirstBannerLow First_Small_Banner_Low_Ecpm;

    public static bool /*FirstBanner_High_Ecpm = false,*/ FirstBanner_Medium_Ecpm = true, FirstBanner_Low_Ecpm = false;


    /// <summary>
    /// 2nd Banner
    /// </summary>
    [HideInInspector]
    public BannerView SmallBanner_2nd_ECPM;
    [HideInInspector]
    public BannerView SmallBanner_2nd_High_Ecpm;

    [HideInInspector]
    public BannerView SmallBanner_2nd_Medium_Ecpm;

    public delegate void SmallBanner2ndMediumEcpm();

    public static event SmallBanner2ndMediumEcpm Second_Small_banner_Medium_Ecpm;


    public delegate void SmallBannr2ndLowEcmp();
    public static event SmallBannr2ndLowEcmp Second_Small_banner_Low_Ecpm;


    public static bool/* SecondBanner_High_Ecpm = false,*/ SecondBanner_Medium_Ecpm = true, SecondBanner_Low_Ecpm = false;
    #endregion

    #region MediumBanner_Var
    [HideInInspector]
    public BannerView MediumBannerHighEcpm;
    [HideInInspector]
    public BannerView MediumBannerMediumEcpm;
    [HideInInspector]
    public BannerView MediumBannerLowEcpm;


    public delegate void MediumBannerMediumECPM();
    public static event MediumBannerMediumECPM MediumbannerMediumEcpm;

    public delegate void MediumBannerLowECPM();
    public static event MediumBannerLowECPM MediumbannerLowEcpm;

    public static bool /*MediumBanner_High_Ecpm = false,*/ MediumBanner_Medium_Ecpm = true, MediumBanner_Low_Ecpm = false;


    #endregion

    #region RewardedVideo_Var

    private static RewardUserDelegate NotifyReward;

    [HideInInspector]
    public RewardedAd rewardBasedVideoHighEcpm;
    [HideInInspector]
    public RewardedAd rewardBasedVideoMediumEcpm;
    [HideInInspector]
    public RewardedAd rewardBasedVideoLowEcpm;

    public delegate void RewardVideoMediumEcpm();
    public static event RewardVideoMediumEcpm RewardVideoMediumECPM;

    public delegate void RewardVideoLowEcpm();
    public static event RewardVideoLowEcpm RewardVideoLowECPM;
    public delegate void RewardVideoUnity();
    public static event RewardVideoUnity RewardVideo_Unity;

    public static bool RewardVideo_High_Ecpm = true, RewardVideo_Medium_Ecpm = false, RewardVideo_Low_Ecpm = false, UnityRewarded = false;

    #endregion

    #region RewardedInterstitialAds

    [HideInInspector]
    public RewardedInterstitialAd rewardedInterstitialAdHighECPM, rewardedInterstitialAdMediumECPM, rewardedInterstitialAdLowECPM;

    public delegate void RewardInterstitialMediumEcpm();
    public static event RewardInterstitialMediumEcpm RewardInterstitialMediumECPM;

    public delegate void RewardInterstitialLowEcpm();
    public static event RewardInterstitialLowEcpm RewardInterstitialLowECPM;

    public static bool RewardInterstitial_High_ECPM = true, RewardInterstitial_Medium_Ecpm = false, RewardInterstitial_Low_Ecpm = false;

    [HideInInspector]
    public bool rewardedInterstitialHighECPMLoaded, rewardedInterstitialMediumECPMLoaded, rewardedInterstitialLowECPMLoaded;

    #endregion
    
    private static AdmobAdsManager _instance;
  
    public static AdmobAdsManager Instance
    {
        get
        {
            if (_instance!=null)
            {
                return _instance;
            }
            else
            {
                return FindObjectOfType<AdmobAdsManager>();
            }
        }
    }
    private void Awake()
    {
        _instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        SystemRaM = SystemInfo.systemMemorySize;
        DontDestroyOnLoad(this.gameObject);
        //InitializeFirebase();
        if (EnableTestModeAds)
        {
            ADMOB_ID = TestAdmob_ID;
        }
        else
        {
#if UNITY_ANDROID
            ADMOB_ID = AndroidAdmob_ID;
#elif UNITY_IOS
        ADMOB_ID = IosAndroid_ID;
          RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
       .SetSameAppKeyEnabled(false)
       .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
#endif
        }


    }

    private void Start()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        InitAdmob();
        InitializeAds();
    }

    public void InitializeAds()
    {
        //ADMOB_ID.UnityId = (Application.platform == RuntimePlatform.IPhonePlayer)
        //    ? AndroidAdmob_ID.UnityId
        //    : AndroidAdmob_ID.UnityId;
        Advertisement.Initialize(ADMOB_ID.UnityId, EnableTestModeAds, this);
    }

    public void OnInitializationComplete()
    {
        Admob_LogHelper.LogGAEvent("Unity_Ads_initialization_complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    private void InitAdmob()
    {
        Admob_LogHelper.LogSender(AdmobEvents.Initializing);

        MobileAds.Initialize((initStatus) =>
        {



            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:

                        Logging.Log("GR >> Adapter: " + status.Description + " not ready.Name=" + className);

                        break;
                    case AdapterState.Ready:

                        Admob_LogHelper.LogSender(AdmobEvents.Initialized);


                        MediationAdapterConsent(className);



                        break;
                }
            }


        });
#if UNITY_IOS
        MobileAds.SetiOSAppPauseOnBackground(true);    
        
#endif
    }


    void MediationAdapterConsent(string AdapterClassname)
    {
        if (AdapterClassname.Contains("ExampleClass"))
        {
            isAdmobInitialized = true;
            CreateAdsObjects();
            LoadFirstSmallBanner();
            LoadSecondSmallBanner();
            LoadMediumBanner();
            LoadRewardedInterstitial();
            LoadRewardedVideo();

        }
        if (AdapterClassname.Contains("MobileAds"))
        {
            isAdmobInitialized = true;
            CreateAdsObjects();
             LoadFirstSmallBanner();
            LoadSecondSmallBanner();
            LoadMediumBanner();
            LoadRewardedInterstitial();
            LoadRewardedVideo();

        }



    }

    private void CreateAdsObjects()
    {




        this.rewardBasedVideoHighEcpm = new RewardedAd(ADMOB_ID.RewardedVideo_High_Ecpm);
        this.rewardBasedVideoMediumEcpm = new RewardedAd(ADMOB_ID.RewardedVideo_Medium_Ecpm);
        this.rewardBasedVideoLowEcpm = new RewardedAd(ADMOB_ID.RewardedVideo_Low_Ecpm);
    }

    #region IntersititialCodeBlock


    private void BindIntersititialHighEcpmEvents()
    {
        this.Interstitial_High_Ecpm.OnAdLoaded += HandleOnAdLoaded_High_Ecpm;

        this.Interstitial_High_Ecpm.OnAdFailedToLoad += HandleOnAdFailedToLoad_High_Ecpm;

        this.Interstitial_High_Ecpm.OnAdOpening += HandleOnAdOpened_High_Ecpm;

        this.Interstitial_High_Ecpm.OnAdClosed += HandleOnAdClosed_High_Ecpm;
    }
    private void BindIntersititialMediumEcpmEvents()
    {
        this.Interstitial_Medium_Ecpm.OnAdLoaded += HandleOnAdLoaded_Medium_Ecpm;

        this.Interstitial_Medium_Ecpm.OnAdFailedToLoad += HandleOnAdFailedToLoad_Medium_Ecpm;

        this.Interstitial_Medium_Ecpm.OnAdOpening += HandleOnAdOpened_Medium_Ecpm;

        this.Interstitial_Medium_Ecpm.OnAdClosed += HandleOnAdClosed_Medium_Ecpm;
    }
    private void BindIntersititialLowEcpmEvents()
    {
        this.Intersitial_Low_Ecpm.OnAdLoaded += HandleOnAdLoaded_Low_Ecpm;

        this.Intersitial_Low_Ecpm.OnAdFailedToLoad += HandleOnAdFailedToLoad_Low_Ecpm;

        this.Intersitial_Low_Ecpm.OnAdOpening += HandleOnAdOpened_Low_Ecpm;

        this.Intersitial_Low_Ecpm.OnAdClosed += HandleOnAdClosed_Low_Ecpm;
    }

    public override bool IsInterstitialAdReady()
    {
        if (Interstitial_HighEcpm)
        {
            if (this.Interstitial_High_Ecpm != null)
                return this.Interstitial_High_Ecpm.IsLoaded();
            else
                return false;
        }
        else if (Interstitial_MediumEcpm)
        {

            if (this.Interstitial_Medium_Ecpm != null)
                return this.Interstitial_Medium_Ecpm.IsLoaded();
            else
                return false;
        }
        else
        {
            if (this.Intersitial_Low_Ecpm != null)
                return this.Intersitial_Low_Ecpm.IsLoaded();
            else
                return false;
        }

    }


    public override void ShowInterstitialAd()
    {
        if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized || !ServerAds)
        {
            return;
        }

        if (Interstitial_HighEcpm)
        {
            if (this.Interstitial_High_Ecpm != null)
            {
                if (this.Interstitial_High_Ecpm.IsLoaded())
                {
                    Admob_LogHelper.LogSender(AdmobEvents.Interstitial_WillDisplay_High_Ecpm);
                    this.Interstitial_High_Ecpm.Show();


                }
            }
        }
        else if (Interstitial_MediumEcpm)
        {
            if (this.Interstitial_Medium_Ecpm != null)
            {
                if (this.Interstitial_Medium_Ecpm.IsLoaded())
                {
                    Admob_LogHelper.LogSender(AdmobEvents.Interstitial_WillDisplay_Medium_Ecpm);
                    this.Interstitial_Medium_Ecpm.Show();

                }
            }
        }
        else if (Interstitial_LowEcpm)
        {
            if (this.Intersitial_Low_Ecpm != null)
            {
                if (this.Intersitial_Low_Ecpm.IsLoaded())
                {
                    Admob_LogHelper.LogSender(AdmobEvents.Interstitial_WillDisplay_Low_Ecpm);
                    this.Intersitial_Low_Ecpm.Show();


                }
            }
        }
        else if (UnityAds)
        {
            Admob_LogHelper.LogGAEvent("Load_Unity_Int");
            Advertisement.Show(ADMOB_ID.Unity_Interstitial_ID, this);
        }
    }

    public override void LoadInterstitialAd()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        if (!isAdmobInitialized || IsInterstitialAdReady() || iAdStatus == AdsLoadingStatus.Loading || !PreferenceManager.GetAdsStatus())
        {
            return;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            if (Interstitial_HighEcpm)
            {
                Int_Medium_Ecpm += LoadInterstitialAd;
                Admob_LogHelper.LogSender(AdmobEvents.LoadInterstitial_High_Ecpm);
                this.Interstitial_High_Ecpm = new InterstitialAd(ADMOB_ID.Intersitial_High_Ecpm);
                BindIntersititialHighEcpmEvents();
                AdRequest request = new AdRequest.Builder().Build();
                // Load the interstitial with the request.
                this.Interstitial_High_Ecpm.LoadAd(request);
                iAdStatus = AdsLoadingStatus.Loading;
            }
            else
            if (Interstitial_MediumEcpm)
            {
                Int_Low_Ecpm += LoadInterstitialAd;
                Admob_LogHelper.LogSender(AdmobEvents.LoadInterstitial_Medium_Ecpm);
                this.Interstitial_Medium_Ecpm = new InterstitialAd(ADMOB_ID.Intersitial_Medium_Ecpm);
                BindIntersititialMediumEcpmEvents();
                AdRequest request = new AdRequest.Builder().Build();
                // Load the interstitial with the request.
                this.Interstitial_Medium_Ecpm.LoadAd(request);
                iAdStatus = AdsLoadingStatus.Loading;

            }
            else
            if (Interstitial_LowEcpm)
            {
                Int_Unity += LoadInterstitialAd;
                Admob_LogHelper.LogSender(AdmobEvents.LoadIntersitiatial_Low_Ecpm);
                this.Intersitial_Low_Ecpm = new InterstitialAd(ADMOB_ID.Intersitial_Low_Ecpm);
                BindIntersititialLowEcpmEvents();
                AdRequest request = new AdRequest.Builder().Build();
                // Load the interstitial with the request.
                this.Intersitial_Low_Ecpm.LoadAd(request);
                iAdStatus = AdsLoadingStatus.Loading;
            }
            else if (UnityAds)
            {
                Admob_LogHelper.LogGAEvent("Load_Unity_Int");
                Advertisement.Load(ADMOB_ID.Unity_Interstitial_ID, this);

            }
        }
    }

    #endregion

    #region IntersititialEventCallBacks
    //HighEcpmEvents
    public void HandleOnAdLoaded_High_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (Interstitial_HighEcpm)
            {
                iAdStatus = AdsLoadingStatus.Loaded;

                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Loaded_High_Ecpm);
                Int_Medium_Ecpm -= LoadInterstitialAd;
                Interstitial_HighEcpm = true;
                Interstitial_MediumEcpm = false;
                Interstitial_LowEcpm = false;
                UnityAds = false;
            }

        });
    }

    public void HandleOnAdFailedToLoad_High_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (Interstitial_HighEcpm)
            {

                iAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:iad:NoInventory_H_Ecpm :: " + args.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_NoInventory_High_Ecpm);
                Interstitial_HighEcpm = false;
                Interstitial_MediumEcpm = true;
                UnityAds = false;
                if (Int_Medium_Ecpm != null)
                    Int_Medium_Ecpm();


            }

        });
    }

    public void HandleOnAdOpened_High_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            iAdStatus = AdsLoadingStatus.NotLoaded;



            if (Interstitial_HighEcpm)
            {
                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Displayed_High_Ecpm);

                Int_Medium_Ecpm -= LoadInterstitialAd;

            }


        });
    }

    public void HandleOnAdClosed_High_Ecpm(object sender, EventArgs args)
    {


        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Closed_High_Ecpm);

            iAdStatus = AdsLoadingStatus.NotLoaded;

            if (Interstitial_HighEcpm)
            {
                this.Interstitial_High_Ecpm.Destroy();
                this.Interstitial_High_Ecpm = new InterstitialAd(ADMOB_ID.Intersitial_High_Ecpm);

                Int_Medium_Ecpm -= LoadInterstitialAd;

                Interstitial_HighEcpm = true;
                Interstitial_MediumEcpm = false;
                Interstitial_LowEcpm = false;
                UnityAds = false;


            }



        });
    }

    //MediumEcpmEvents

    public void HandleOnAdLoaded_Medium_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (Interstitial_MediumEcpm)
            {
                iAdStatus = AdsLoadingStatus.Loaded;

                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Loaded_Medium_Ecpm);
                Int_Low_Ecpm -= LoadInterstitialAd;
                Interstitial_HighEcpm = false;
                UnityAds = false;
                Interstitial_MediumEcpm = true;
                Interstitial_LowEcpm = false;
            }

        });
    }

    public void HandleOnAdFailedToLoad_Medium_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (Interstitial_MediumEcpm)
            {

                iAdStatus = AdsLoadingStatus.NoInventory;
                Admob_LogHelper.LogSender(AdmobEvents.Interstilial_NoInventory_Medium_Ecpm);
                Interstitial_HighEcpm = false;
                Interstitial_MediumEcpm = false;
                UnityAds = false;
                Interstitial_LowEcpm = true;
                if (Int_Low_Ecpm != null)
                    Int_Low_Ecpm();


            }

        });
    }

    public void HandleOnAdOpened_Medium_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            iAdStatus = AdsLoadingStatus.NotLoaded;



            if (Interstitial_MediumEcpm)
            {
                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Displayed_High_Ecpm);

                Int_Low_Ecpm -= LoadInterstitialAd;

            }


        });
    }

    public void HandleOnAdClosed_Medium_Ecpm(object sender, EventArgs args)
    {


        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Closed_Medium_Ecpm);

            iAdStatus = AdsLoadingStatus.NotLoaded;

            if (Interstitial_MediumEcpm)
            {
                this.Interstitial_Medium_Ecpm.Destroy();
                this.Interstitial_Medium_Ecpm = new InterstitialAd(ADMOB_ID.Intersitial_Medium_Ecpm);

                Int_Low_Ecpm -= LoadInterstitialAd;

                Interstitial_HighEcpm = true;
                Interstitial_MediumEcpm = false;
                Interstitial_LowEcpm = false;
                UnityAds = false;


            }



        });
    }

    //LowEcpmEvents
    public void HandleOnAdLoaded_Low_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (Interstitial_LowEcpm)
            {
                iAdStatus = AdsLoadingStatus.Loaded;

                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Loaded_Low_Ecpm);
                Interstitial_HighEcpm = false;
                Interstitial_MediumEcpm = false;
                UnityAds = false;
                Interstitial_LowEcpm = true;
            }

        });
    }

    public void HandleOnAdFailedToLoad_Low_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (Interstitial_LowEcpm)
            {

                iAdStatus = AdsLoadingStatus.NoInventory;
                Admob_LogHelper.LogSender(AdmobEvents.Interstitial_NoInventory_Low_Ecpm);
                Interstitial_HighEcpm = false;
                Interstitial_MediumEcpm = false;
                Interstitial_LowEcpm = false;
                UnityAds = true;
                if (Int_Unity != null)
                {
                    Int_Unity();
                }


            }

        });
    }

    public void HandleOnAdOpened_Low_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            iAdStatus = AdsLoadingStatus.NotLoaded;



            if (Interstitial_LowEcpm)
            {
                Admob_LogHelper.LogSender(AdmobEvents.Interstital_Displayed_Low_Ecpm);

            }


        });
    }

    public void HandleOnAdClosed_Low_Ecpm(object sender, EventArgs args)
    {


        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            Admob_LogHelper.LogSender(AdmobEvents.Interstitial_Closed_Low_Ecpm);

            iAdStatus = AdsLoadingStatus.NotLoaded;

            if (Interstitial_LowEcpm)
            {
                this.Intersitial_Low_Ecpm.Destroy();
                this.Intersitial_Low_Ecpm = new InterstitialAd(ADMOB_ID.Intersitial_Low_Ecpm);
                Interstitial_HighEcpm = true;
                Interstitial_MediumEcpm = false;
                Interstitial_LowEcpm = false;
                UnityAds = false;



            }



        });
    }

    #endregion

    #region BannerCodeBlock

    public override bool IsSmallFirstBannerReady()
    {
        return isSmallBannerLoadedFirst;
    }
    public override void LoadFirstSmallBanner()
    {
        if (!PreferenceManager.GetAdsStatus() || IsSmallFirstBannerReady() || smallBannerStatus == AdsLoadingStatus.Loading)
        {
            return;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            //if (FirstBanner_High_Ecpm)
            //{
            //    this.SmallBanner_First_High_Ecpm = new BannerView(ADMOB_ID.smallBanner_First_High_Ecpm, AdSize.Banner, AdPosition.TopLeft);
            //    First_Small_Banner_Low_Ecpm -= LoadFirstSmallBanner;
            //    First_Small_Banner_Medium_Ecpm += LoadFirstSmallBanner;
            //    Logging.Log("FirstSmallBanner_H_Ecpm");
            //    BindSmallBannerFirstHighEcpm();
            //    // Create an empty ad request.
            //    AdRequest request = new AdRequest.Builder().Build();
            //    // Load the banner with the request.
            //    this.SmallBanner_First_High_Ecpm.LoadAd(request);
            //    this.SmallBanner_First_High_Ecpm.Hide();
            //}
            //else 
            if (FirstBanner_Medium_Ecpm)
            {
                this.SmallBanner_First_Medium_Ecpm = new BannerView(ADMOB_ID.SmallBanner_First_Medium_Ecpm, AdSize.Banner, AdPosition.TopLeft);
                First_Small_Banner_Medium_Ecpm -= LoadFirstSmallBanner;
                First_Small_Banner_Low_Ecpm += LoadFirstSmallBanner;
                Logging.Log("FirstSmallBanner_M_Ecpm");
                BindSmallBannerFirstMediumEcpm();
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();

                // Load the banner with the request.
                this.SmallBanner_First_Medium_Ecpm.LoadAd(request);
                this.SmallBanner_First_Medium_Ecpm.Hide();
            }
            else
            if (FirstBanner_Low_Ecpm)
            {
                this.SmallBanner_First_ECPM = new BannerView(ADMOB_ID.Small_banners_First_Ecpm, AdSize.Banner, AdPosition.TopLeft);

                Logging.Log("FirstSmallBanner_L_Ecpm");
                BindSmallBannerFirst();
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();

                // Load the banner with the request.
                this.SmallBanner_First_ECPM.LoadAd(request);
                this.SmallBanner_First_ECPM.Hide();

            }



        }
    }
    public override void HideFirstSmallBannerEvent()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        //if (this.SmallBanner_First_High_Ecpm != null)
        //{
        //    this.SmallBanner_First_High_Ecpm.Hide();
        //   Logging.Log("GR >> Admob:smallBanner:Hide_H_Ecpm");
        //}
        //else
        if (this.SmallBanner_First_Medium_Ecpm != null)
        {
            this.SmallBanner_First_Medium_Ecpm.Hide();
            Logging.Log("GR >> Admob:smallBanner:Hide_M_Ecpm");
        }
        else
            if (this.SmallBanner_First_ECPM != null)
        {
            Logging.Log("GR >> Admob:smallBanner:Hide_L_Ecpm");
            this.SmallBanner_First_ECPM.Hide();
        }

    }

    public void ShowBanner()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        ShowSmallFirstBanner(GoogleMobileAds.Api.AdPosition.TopLeft);
    }
    public override void ShowSmallFirstBanner(AdPosition pos)
    {
        // HideFirstSmallBannerEvent();

        try
        {
            if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized || !ServerAds)
            {
                return;
            }

            //if (FirstBanner_High_Ecpm)
            //{

            //        if (SmallBanner_First_High_Ecpm != null)
            //        {

            //            this.SmallBanner_First_High_Ecpm.Hide();

            //            this.SmallBanner_First_High_Ecpm.Show();
            //            this.SmallBanner_First_High_Ecpm.SetPosition(pos);
            //        }

            //}
            //else 
            if (FirstBanner_Medium_Ecpm)
            {
                if (SmallBanner_First_Medium_Ecpm != null)
                {
                    Logging.Log("GR >> FirstBanner_Medium_Ecpm_Show");
                    this.SmallBanner_First_Medium_Ecpm.Hide();

                    this.SmallBanner_First_Medium_Ecpm.Show();
                    this.SmallBanner_First_Medium_Ecpm.SetPosition(pos);
                }
            }
            else if (FirstBanner_Low_Ecpm)
            {
                if (SmallBanner_First_ECPM != null)
                {

                    this.SmallBanner_First_ECPM.Hide();
                    Logging.Log("GR >> SmallBanner_First_ECPM_Show");
                    this.SmallBanner_First_ECPM.Show();
                    this.SmallBanner_First_ECPM.SetPosition(pos);
                }
            }
            else
            {
                LoadFirstSmallBanner();
            }



        }
        catch (Exception error)
        {
            Logging.Log("Small Banner Error: " + error);
        }
    }


    private void BindSmallBannerFirst()
    {
        this.SmallBanner_First_ECPM.OnAdLoaded += SmallBanner_HandleOnAdLoaded_First;

        this.SmallBanner_First_ECPM.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad_First;


    }
    //private void BindSmallBannerFirstHighEcpm()
    //{
    //    //this.SmallBanner_First_High_Ecpm.OnAdLoaded += SmallBanner_HandleOnAdLoaded_First_High_Ecpm;

    //    //this.SmallBanner_First_High_Ecpm.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad_First_High_Ecpm;
    //}

    private void BindSmallBannerFirstMediumEcpm()
    {
        this.SmallBanner_First_Medium_Ecpm.OnAdLoaded += SmallBanner_HandleOnAdLoaded_First_Medium_Ecpm;

        this.SmallBanner_First_Medium_Ecpm.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad_First_Medium_Ecpm;
    }
    /// <summary>
    /// 2nd BannerCode
    /// </summary>
    private void BindSmallBannerSecondEcpm()
    {
        this.SmallBanner_2nd_ECPM.OnAdLoaded += SmallBanner_HandleOnAdLoaded_Second;

        this.SmallBanner_2nd_ECPM.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad_Second;



    }
    //private void BindSmallBannerSecondHighEcpm()
    //{


    //    //this.SmallBanner_2nd_High_Ecpm.OnAdLoaded += SmallBanner_HandleOnAdLoaded_Second_High_Ecpm;

    //    //this.SmallBanner_2nd_High_Ecpm.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad_Second_High_Ecpm;
    //}

    private void BindSmallBannerSecondMediumEcpm()
    {
        this.SmallBanner_2nd_Medium_Ecpm.OnAdLoaded += SmallBanner_HandleOnAdLoaded_Second_Medium_Ecpm;

        this.SmallBanner_2nd_Medium_Ecpm.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad_Second_Medium_Ecpm;
    }
    public override bool IsSecondBannerReady()
    {
        return isSmallBannerLoadedSecond;
    }
    public override void LoadSecondSmallBanner()
    {
        if (!PreferenceManager.GetAdsStatus() || IsSecondBannerReady() || small2ndBannerStatus == AdsLoadingStatus.Loading)
        {
            return;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {

            //if (FirstBanner_High_Ecpm)
            //    //{
            //    //    this.SmallBanner_First_High_Ecpm = new BannerView(ADMOB_ID.smallBanner_First_High_Ecpm, AdSize.Banner, AdPosition.TopLeft);
            //    //    First_Small_Banner_Low_Ecpm -= LoadFirstSmallBanner;
            //    //    First_Small_Banner_Medium_Ecpm += LoadFirstSmallBanner;
            //    //    Logging.Log("FirstSmallBanner_H_Ecpm");
            //    //    BindSmallBannerFirstHighEcpm();
            //    //    // Create an empty ad request.
            //    //    AdRequest request = new AdRequest.Builder().Build();
            //    //    // Load the banner with the request.
            //    //    this.SmallBanner_First_High_Ecpm.LoadAd(request);
            //    //    this.SmallBanner_First_High_Ecpm.Hide();
            //    //}
            //    //else 
            if (SecondBanner_Medium_Ecpm)
            {
                this.SmallBanner_2nd_Medium_Ecpm = new BannerView(ADMOB_ID.SmallBanner_Second_Medium_Ecpm, AdSize.Banner, AdPosition.TopRight);

                Second_Small_banner_Low_Ecpm += LoadSecondSmallBanner;
                Logging.Log("SecondSmallBanner_M_Ecpm");
                BindSmallBannerSecondMediumEcpm();
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();

                // Load the banner with the request.
                this.SmallBanner_2nd_Medium_Ecpm.LoadAd(request);
                this.SmallBanner_2nd_Medium_Ecpm.Hide();
            }
            else
            if (SecondBanner_Low_Ecpm)
            {
                this.SmallBanner_2nd_ECPM = new BannerView(ADMOB_ID.Small_banners_Second_Ecpm, AdSize.Banner, AdPosition.TopRight);

                Logging.Log("SecondSmallBanner_L_Ecpm");
                BindSmallBannerSecondEcpm();

                AdRequest request = new AdRequest.Builder().Build();

                this.SmallBanner_2nd_ECPM.LoadAd(request);
                this.SmallBanner_2nd_ECPM.Hide();

            }




        }
    }
    public override void HideSecondSmallBannerEvent()
    {
        if (SystemRaM <=1024)
        {
            return;
        }

        if (this.SmallBanner_2nd_ECPM != null)
        {

            this.SmallBanner_2nd_ECPM.Hide();
        }


    }

    public void ShowBanner2()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        ShowSecondBanner(GoogleMobileAds.Api.AdPosition.TopRight);
    }
    public override void ShowSecondBanner(AdPosition pos)
    {
        //HideSecondSmallBannerEvent();

        try
        {
            if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
            {
                return;
            }


            //if (FirstBanner_High_Ecpm)
            //{

            //        if (SmallBanner_First_High_Ecpm != null)
            //        {

            //            this.SmallBanner_First_High_Ecpm.Hide();

            //            this.SmallBanner_First_High_Ecpm.Show();
            //            this.SmallBanner_First_High_Ecpm.SetPosition(pos);
            //        }

            //}
            //else 
            if (SecondBanner_Medium_Ecpm)
            {
                if (SmallBanner_2nd_Medium_Ecpm != null)
                {

                    this.SmallBanner_2nd_Medium_Ecpm.Hide();

                    this.SmallBanner_2nd_Medium_Ecpm.Show();
                    this.SmallBanner_2nd_Medium_Ecpm.SetPosition(pos);
                    Logging.Log("GR >> SecondBanner_Medium__Ecpm_Show");
                }
            }
            else if (SecondBanner_Low_Ecpm)
            {
                if (SmallBanner_2nd_ECPM != null)
                {

                    this.SmallBanner_2nd_ECPM.Hide();

                    this.SmallBanner_2nd_ECPM.Show();
                    this.SmallBanner_2nd_ECPM.SetPosition(pos);
                    Logging.Log("GR >> SecondBanner_Low_Ecpm_Show");
                }
            }
            else
            {
                LoadSecondSmallBanner();
            }



        }
        catch (Exception error)
        {
            Logging.Log("Small Banner Error: " + error);
        }
    }

    #endregion

    #region SmallBannerEvents

    //public void SmallBanner_HandleOnAdLoaded_First_High_Ecpm(object sender, EventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (FirstBanner_High_Ecpm)
    //        {
    //            smallBannerStatus = AdsLoadingStatus.Loaded;


    //            First_Small_Banner_Medium_Ecpm -= LoadFirstSmallBanner;

    //            isSmallBannerLoadedFirst = true;
    //            FirstBanner_High_Ecpm = true;
    //            FirstBanner_Medium_Ecpm = false;
    //            FirstBanner_Low_Ecpm = false;

    //        }

    //    });
    //}

    //public void SmallBanner_HandleOnAdFailedToLoad_First_High_Ecpm(object sender, AdFailedToLoadEventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (FirstBanner_High_Ecpm)
    //        {
    //            smallBannerStatus = AdsLoadingStatus.NoInventory;


    //            isSmallBannerLoadedFirst = false;
    //            FirstBanner_High_Ecpm = false;
    //            FirstBanner_Medium_Ecpm = true;
    //            FirstBanner_Low_Ecpm = false;

    //            if (First_Small_Banner_Medium_Ecpm != null)
    //                First_Small_Banner_Medium_Ecpm();

    //        }
    //    });
    //}


    public void SmallBanner_HandleOnAdLoaded_First_Medium_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            if (FirstBanner_Medium_Ecpm)
            {
                smallBannerStatus = AdsLoadingStatus.Loaded;


                First_Small_Banner_Low_Ecpm -= LoadFirstSmallBanner;
                Logging.Log("GR >> FirstSmallBanner_M_Loaded_Ecpm");
                isSmallBannerLoadedFirst = true;
              //  FirstBanner_High_Ecpm = false;
                FirstBanner_Medium_Ecpm = true;
                FirstBanner_Low_Ecpm = false;

            }

        });
    }

    public void SmallBanner_HandleOnAdFailedToLoad_First_Medium_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {


            if (FirstBanner_Medium_Ecpm)
            {
                smallBannerStatus = AdsLoadingStatus.NoInventory;

                Logging.Log("GR >> FirstSmallBanner_M_Fail_Ecpm");
                isSmallBannerLoadedFirst = false;
               // FirstBanner_High_Ecpm = false;
                FirstBanner_Medium_Ecpm = false;
                FirstBanner_Low_Ecpm = true;

                if (First_Small_Banner_Low_Ecpm != null)
                    First_Small_Banner_Low_Ecpm();

            }
        });
    }




    public void SmallBanner_HandleOnAdLoaded_First(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            if (FirstBanner_Low_Ecpm)
            {
                smallBannerStatus = AdsLoadingStatus.Loaded;

                isSmallBannerLoadedFirst = true;
               // FirstBanner_High_Ecpm = false;
                FirstBanner_Medium_Ecpm = false;
                FirstBanner_Low_Ecpm = true;
                Logging.Log("GR >> FirstSmallBanner_l_Loaded_Ecpm");

            }

        });
    }

    public void SmallBanner_HandleOnAdFailedToLoad_First(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            {
                smallBannerStatus = AdsLoadingStatus.NoInventory;

                Logging.Log("GR >> FirstSmallBanner_l_Fail_Ecpm");

                isSmallBannerLoadedFirst = false;

            }
        });
    }







    //******Small Banner2 Ad Handlers**********//


    /// <summary>
    /// 2nd Banner High Ecm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    //public void SmallBanner_HandleOnAdLoaded_Second_High_Ecpm(object sender, EventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (SecondBanner_High_Ecpm)
    //        {
    //            small2ndBannerStatus = AdsLoadingStatus.Loaded;


    //            Second_Small_banner_Medium_Ecpm -= LoadSecondSmallBanner;

    //            isSmallBannerLoadedSecond = true;
    //            SecondBanner_High_Ecpm = true;
    //            SecondBanner_Medium_Ecpm = false;
    //            SecondBanner_Low_Ecpm = false;

    //        }

    //    });
    //}

    //public void SmallBanner_HandleOnAdFailedToLoad_Second_High_Ecpm(object sender, AdFailedToLoadEventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (SecondBanner_High_Ecpm)
    //        {
    //            small2ndBannerStatus = AdsLoadingStatus.NoInventory;


    //            isSmallBannerLoadedSecond = false;
    //            SecondBanner_High_Ecpm = false;
    //            SecondBanner_Medium_Ecpm = true;
    //            SecondBanner_Low_Ecpm = false;

    //            if (Second_Small_banner_Medium_Ecpm != null)
    //                Second_Small_banner_Medium_Ecpm();

    //        }
    //    });
    //}


    /// <summary>
    /// 2nd banner Medium
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>

    public void SmallBanner_HandleOnAdLoaded_Second_Medium_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {

            if (SecondBanner_Medium_Ecpm)
            {
                small2ndBannerStatus = AdsLoadingStatus.Loaded;

                Logging.Log("GR >> 2ndSmallBanner_M_Loaded_Ecpm");
                First_Small_Banner_Low_Ecpm -= LoadSecondSmallBanner;

                isSmallBannerLoadedSecond = true;
              //  SecondBanner_High_Ecpm = false;
                SecondBanner_Medium_Ecpm = true;
                SecondBanner_Low_Ecpm = false;

            }

        });
    }

    public void SmallBanner_HandleOnAdFailedToLoad_Second_Medium_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {


            if (SecondBanner_Medium_Ecpm)
            {
                small2ndBannerStatus = AdsLoadingStatus.NoInventory;


                isSmallBannerLoadedSecond = false;
               // SecondBanner_High_Ecpm = false;
                SecondBanner_Medium_Ecpm = false;
                SecondBanner_Low_Ecpm = true;
                Logging.Log("GR >> 2ndSmallBanner_M_Failed_Ecpm");
                if (Second_Small_banner_Low_Ecpm != null)
                    Second_Small_banner_Low_Ecpm();

            }
        });
    }


    /// <summary>
    /// 2nd Banner Low Ecpm Events
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public void SmallBanner_HandleOnAdLoaded_Second(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (SecondBanner_Low_Ecpm)
            {

                small2ndBannerStatus = AdsLoadingStatus.Loaded;
                Second_Small_banner_Low_Ecpm -= LoadSecondSmallBanner;

                isSmallBannerLoadedSecond = true;
              //  SecondBanner_High_Ecpm = false;
                SecondBanner_Medium_Ecpm = false;
                SecondBanner_Low_Ecpm = true;
                Logging.Log("GR >> 2ndSmallBanner_l_Loaded_Ecpm");
            }

        });
    }

    public void SmallBanner_HandleOnAdFailedToLoad_Second(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (SecondBanner_Low_Ecpm)
            {
                small2ndBannerStatus = AdsLoadingStatus.NoInventory;

                Logging.Log("GR >> 2ndSmallBanner_l_Fail_Ecpm");
                isSmallBannerLoadedSecond = false;

            }
        });
    }




    #endregion

    #region MediumBannerCodeBlocks
    public override bool IsMediumBannerReady()
    {
        return isMediumBannerLoaded;
    }

    public override void LoadMediumBanner()
    {
        if (!PreferenceManager.GetAdsStatus() || IsMediumBannerReady() || mediumBannerStatus == AdsLoadingStatus.Loading)
        {
            return;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            //if (MediumBanner_High_Ecpm)
            //{
            //    MediumbannerMediumEcpm += LoadMediumBanner;
            //    Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Load_HighEcpm);


            //    this.MediumBannerHighEcpm = new BannerView(ADMOB_ID.MediumBanner_High_Ecpm, AdSize.MediumRectangle, AdPosition.BottomLeft);

            //    BindMediumBannerEvents_H_Ecpm();
            //    // Create an empty ad request.
            //    AdRequest request = new AdRequest.Builder().Build();

            //    // Load the banner with the request.
            //    this.MediumBannerHighEcpm.LoadAd(request);
            //    this.MediumBannerHighEcpm.Hide();

            //}
            //else
            if (MediumBanner_Medium_Ecpm)
            {
                MediumbannerLowEcpm += LoadMediumBanner;
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Load_MediumEcpm);


                this.MediumBannerMediumEcpm = new BannerView(ADMOB_ID.MediumBanner_Medium_Ecpm, AdSize.MediumRectangle, AdPosition.BottomLeft);

                BindMediumBannerEvents_M_Ecpm();
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();

                // Load the banner with the request.
                this.MediumBannerMediumEcpm.LoadAd(request);
                this.MediumBannerMediumEcpm.Hide();
            }
            else if (MediumBanner_Low_Ecpm)
            {

                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Load_LowEcpm);


                this.MediumBannerLowEcpm = new BannerView(ADMOB_ID.MediumBanner_Ecpm, AdSize.MediumRectangle, AdPosition.BottomLeft);

                BindMediumBannerEvents_L_Ecpm();
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();

                // Load the banner with the request.
                this.MediumBannerLowEcpm.LoadAd(request);
                this.MediumBannerLowEcpm.Hide();
            }
        }

    }

    public void ShowMBanner()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
    }
    public override void ShowMediumBanner(AdPosition pos)
    {
        try
        {
            if (!PreferenceManager.GetAdsStatus() || !isAdmobInitialized)
            {
                return;
            }
            //if (MediumBanner_High_Ecpm)
            //{

            //    Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Show_HighEcpm);
            //    if (MediumBannerHighEcpm != null)
            //    {

            //        this.MediumBannerHighEcpm.Hide();
            //        this.MediumBannerHighEcpm.Show();
            //        this.MediumBannerHighEcpm.SetPosition(pos);
            //    }
            //}
            //else
            if (MediumBanner_Medium_Ecpm)
            {
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Show_MediumEcpm);
                if (MediumBannerMediumEcpm != null)
                {

                    this.MediumBannerMediumEcpm.Hide();
                    this.MediumBannerMediumEcpm.Show();
                    this.MediumBannerMediumEcpm.SetPosition(pos);
                }
            }
            else if (MediumBanner_Low_Ecpm)
            {
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Show_LowEcpm);
                if (MediumBannerLowEcpm != null)
                {

                    this.MediumBannerLowEcpm.Hide();
                    this.MediumBannerLowEcpm.Show();
                    this.MediumBannerLowEcpm.SetPosition(pos);
                }
            }
        }
        catch (Exception e)
        {

            Logging.Log("Medium Banner Error: " + e);
        }
    }
    public override void HideMediumBannerEvent()
    {
        if (SystemRaM <=1024)
        {
            return;
        }
        //if (MediumBanner_High_Ecpm)
        //{
        //if (this.MediumBannerHighEcpm != null)
        //{
        //    Logging.Log("GR >> Admob:mediumBanner:Hide_H_Ecpm");
        //    this.MediumBannerHighEcpm.Hide();
        //}
        //  }
        //  else if (MediumBanner_Medium_Ecpm)
        //   {
        if (this.MediumBannerMediumEcpm != null)
        {
            Logging.Log("GR >> Admob:mediumBanner:Hide_M_Ecpm");
            this.MediumBannerMediumEcpm.Hide();
        }
        //   }
        //  else if (MediumBanner_Low_Ecpm)
        //  {
        if (this.MediumBannerLowEcpm != null)
        {
            Logging.Log("GR >> Admob:mediumBanner:Hide_M_Ecpm");
            this.MediumBannerLowEcpm.Hide();
        }
        //  }
    }

    //private void BindMediumBannerEvents_H_Ecpm()
    //{



    //    this.MediumBannerHighEcpm.OnAdLoaded += MediumBanner_HandleOnAdLoaded_H_Ecpm;

    //    this.MediumBannerHighEcpm.OnAdFailedToLoad += MediumBanner_HandleOnAdFailedToLoad_H_Ecpm;

    //    this.MediumBannerHighEcpm.OnAdOpening += MediumBanner_HandleOnAdOpened_H_Ecpm;

    //    this.MediumBannerHighEcpm.OnAdClosed += MediumBanner_HandleOnAdClosed_H_Ecpm;

    //}

    private void BindMediumBannerEvents_M_Ecpm()
    {


        this.MediumBannerMediumEcpm.OnAdLoaded += MediumBanner_HandleOnAdLoaded_M_Ecpm;

        this.MediumBannerMediumEcpm.OnAdFailedToLoad += MediumBanner_HandleOnAdFailedToLoad_M_Ecpm;

        this.MediumBannerMediumEcpm.OnAdOpening += MediumBanner_HandleOnAdOpened_M_Ecpm;

        this.MediumBannerMediumEcpm.OnAdClosed += MediumBanner_HandleOnAdClosed_M_Ecpm;

    }
    private void BindMediumBannerEvents_L_Ecpm()
    {



        this.MediumBannerLowEcpm.OnAdLoaded += MediumBanner_HandleOnAdLoaded_L_Ecpm;

        this.MediumBannerLowEcpm.OnAdFailedToLoad += MediumBanner_HandleOnAdFailedToLoad_L_Ecpm;

        this.MediumBannerLowEcpm.OnAdOpening += MediumBanner_HandleOnAdOpened_L_Ecpm;

        this.MediumBannerLowEcpm.OnAdClosed += MediumBanner_HandleOnAdClosed_L_Ecpm;

    }
    #endregion

    #region MediumBannerCallBack Handlers
    //public void MediumBanner_HandleOnAdLoaded_H_Ecpm(object sender, EventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (MediumBanner_High_Ecpm)
    //        {
    //            mediumBannerStatus = AdsLoadingStatus.Loaded;


    //            Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Loaded_HighEcpm);
    //            MediumbannerMediumEcpm -= LoadMediumBanner;
    //            isMediumBannerLoaded = true;
    //        }

    //    });
    //}

    //public void MediumBanner_HandleOnAdFailedToLoad_H_Ecpm(object sender, AdFailedToLoadEventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (MediumBanner_High_Ecpm)
    //        {
    //            mediumBannerStatus = AdsLoadingStatus.NotLoaded;
    //            Logging.Log("GR >> Admob:mediumBanner:NoInventory_H_Ecpm :: " + args.ToString());
    //            //callBackText.text = "There is no inventory of medium Banner for the given size";
    //            Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_NoInventory_HighEcpm);
    //            MediumBanner_High_Ecpm = false;
    //            MediumBanner_Medium_Ecpm = true;
    //            isMediumBannerLoaded = false;
    //            if (MediumbannerMediumEcpm != null)
    //            {
    //                MediumbannerMediumEcpm();
    //            }
    //        }

    //    });

    //}

    //public void MediumBanner_HandleOnAdOpened_H_Ecpm(object sender, EventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (MediumBanner_High_Ecpm)
    //        {

    //            Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Displayed_HighEcpm);
    //            MediumbannerMediumEcpm -= LoadMediumBanner;
    //        }
    //    });
    //}

    //public void MediumBanner_HandleOnAdClosed_H_Ecpm(object sender, EventArgs args)
    //{
    //    MobileAdsEventExecutor.ExecuteInUpdate(() =>
    //    {
    //        if (MediumBanner_High_Ecpm)
    //        {


    //            MediumbannerMediumEcpm -= LoadMediumBanner;
    //        }
    //    });
    //}



    //MediumBanner2
    public void MediumBanner_HandleOnAdLoaded_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Medium_Ecpm)
            {
                mediumBannerStatus = AdsLoadingStatus.Loaded;

                //callBackText.text = "Medium Banner Loaded of Size: " + BannersHandler.instance.width.text + "*" + BannersHandler.instance.height.text;
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Loaded_MediumEcpm);
                MediumbannerLowEcpm -= LoadMediumBanner;
                isMediumBannerLoaded = true;
            }

        });
    }

    public void MediumBanner_HandleOnAdFailedToLoad_M_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Medium_Ecpm)
            {
                mediumBannerStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:mediumBanner:NoInventory_M_Ecpm :: " + args.ToString());
                //callBackText.text = "There is no inventory of medium Banner for the given size";
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_NoInventory_MediumEcpm);
             //   MediumBanner_High_Ecpm = false;
                MediumBanner_Medium_Ecpm = false;
                MediumBanner_Low_Ecpm = true;
                isMediumBannerLoaded = false;
                if (MediumbannerLowEcpm != null)
                {
                    MediumbannerLowEcpm();
                }
            }

        });

    }

    public void MediumBanner_HandleOnAdOpened_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Medium_Ecpm)
            {

                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Displayed_MediumEcpm);
                MediumbannerLowEcpm -= LoadMediumBanner;
            }
        });
    }

    public void MediumBanner_HandleOnAdClosed_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {



            MediumbannerLowEcpm -= LoadMediumBanner;

        });
    }



    //MediumBanner3

    public void MediumBanner_HandleOnAdLoaded_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Low_Ecpm)
            {
                mediumBannerStatus = AdsLoadingStatus.Loaded;

                //callBackText.text = "Medium Banner Loaded of Size: " + BannersHandler.instance.width.text + "*" + BannersHandler.instance.height.text;
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Loaded_LowEcpm);

                isMediumBannerLoaded = true;
            }

        });
    }

    public void MediumBanner_HandleOnAdFailedToLoad_L_Ecpm(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Low_Ecpm)
            {
                mediumBannerStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:mediumBanner:NoInventory_L_Ecpm :: " + args.ToString());
                //callBackText.text = "There is no inventory of medium Banner for the given size";
                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_NoInventory_LowEcpm);

                isMediumBannerLoaded = false;

            }

        });

    }

    public void MediumBanner_HandleOnAdOpened_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Low_Ecpm)
            {

                Admob_LogHelper.LogSender(AdmobEvents.MediumBanner_Displayed_LowEcpm);


            }
        });
    }

    public void MediumBanner_HandleOnAdClosed_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (MediumBanner_Low_Ecpm)
            {
                Logging.Log("GR >> Admob:mediumBanner:Closed");


            }
        });
    }


    #endregion

    #region RewardedVideoCodeBlock
    public override void LoadRewardedVideo()
    {
        if (!isAdmobInitialized || IsRewardedAdReady() || rAdStatus == AdsLoadingStatus.Loading)
        {
            return;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            if (RewardVideo_High_Ecpm)
            {
                RewardVideoMediumECPM += LoadRewardedVideo;
                RewardVideoLowECPM -= LoadRewardedVideo;

                Admob_LogHelper.LogSender(AdmobEvents.LoadRewardedVideo_High_Ecpm);
                BindRewardedEvents_H_Ecpm();
                // Create an empty ad request. 
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded video ad with the request.
                this.rewardBasedVideoHighEcpm.LoadAd(request);
                rAdStatus = AdsLoadingStatus.Loading;
            }
            else if (RewardVideo_Medium_Ecpm)
            {
                RewardVideoMediumECPM -= LoadRewardedVideo;
                RewardVideoLowECPM += LoadRewardedVideo;

                Admob_LogHelper.LogSender(AdmobEvents.LoadRewardedVideo_Medium_Ecpm);
                BindRewardedEvents_M_Ecpm();
                // Create an empty ad request. 
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded video ad with the request.
                this.rewardBasedVideoMediumEcpm.LoadAd(request);
                rAdStatus = AdsLoadingStatus.Loading;
            }
            else if (RewardVideo_Low_Ecpm)
            {
                RewardVideoMediumECPM -= LoadRewardedVideo;
                RewardVideoLowECPM -= LoadRewardedVideo;
                RewardVideo_Unity += LoadRewardedVideo;

                Admob_LogHelper.LogSender(AdmobEvents.LoadRewardedVideo_Low_Ecpm);
                BindRewardedEvents_L_Ecpm();
                // Create an empty ad request. 
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded video ad with the request.
                this.rewardBasedVideoLowEcpm.LoadAd(request);
                rAdStatus = AdsLoadingStatus.Loading;
            }
            else if (UnityRewarded)
            {
                Advertisement.Load(ADMOB_ID.Unity_RewardedVideo);
            }


        }

    }
    public override bool IsRewardedAdReady()
    {
        if (RewardVideo_High_Ecpm)
        {
            if (this.rewardBasedVideoHighEcpm != null)
                return this.rewardBasedVideoHighEcpm.IsLoaded();
            else
                return false;
        }
        else if (RewardVideo_Low_Ecpm)
        {
            if (this.rewardBasedVideoMediumEcpm != null)
                return this.rewardBasedVideoMediumEcpm.IsLoaded();
            else
                return false;
        }
        else
        {
            if (this.rewardBasedVideoLowEcpm != null)
                return this.rewardBasedVideoLowEcpm.IsLoaded();
            else
                return false;
        }
    }
    public override void ShowRewardedVideo(RewardUserDelegate _delegate)
    {
        if (RewardVideo_High_Ecpm)
        {

            NotifyReward = _delegate;
            Admob_LogHelper.LogSender(AdmobEvents.ShowRewardedVideo_High_Ecpm);

            if (this.rewardBasedVideoHighEcpm.IsLoaded())
            {


                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_WillDisplay_High_Ecpm);
                this.rewardBasedVideoHighEcpm.Show();

            }
        }
        else if (RewardVideo_Medium_Ecpm)
        {
            NotifyReward = _delegate;
            Admob_LogHelper.LogSender(AdmobEvents.ShowRewardedVideo_Medium_Ecpm);

            if (this.rewardBasedVideoMediumEcpm.IsLoaded())
            {

                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_WillDisplay_Medium_Ecpm);
                this.rewardBasedVideoMediumEcpm.Show();

            }
        }
        else if (RewardVideo_Medium_Ecpm)
        {
            NotifyReward = _delegate;
            Admob_LogHelper.LogSender(AdmobEvents.ShowRewardedVideo_Low_Ecpm);

            if (this.rewardBasedVideoLowEcpm.IsLoaded())
            {


                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_WillDisplay_Low_Ecpm);
                this.rewardBasedVideoLowEcpm.Show();

            }
        }
        else if (UnityRewarded)
        {
            NotifyReward = _delegate;
            Advertisement.Show(ADMOB_ID.Unity_RewardedVideo, this);
        }
    }

    private void BindRewardedEvents_H_Ecpm()
    {

        rewardBasedVideoHighEcpm.OnAdLoaded += HandleRewardBasedVideoLoaded_H_Ecpm;

        rewardBasedVideoHighEcpm.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad_H_Ecpm;

        rewardBasedVideoHighEcpm.OnAdOpening += HandleRewardBasedVideoOpened_H_Ecpm;


        rewardBasedVideoHighEcpm.OnAdFailedToShow += HandleRewardedAdFailedToShow_H_Ecpm;

        rewardBasedVideoHighEcpm.OnUserEarnedReward += HandleRewardBasedVideoRewarded_H_Ecpm;

        rewardBasedVideoHighEcpm.OnAdClosed += HandleRewardBasedVideoClosed_H_Ecpm;

    }
    private void BindRewardedEvents_M_Ecpm()
    {

        rewardBasedVideoMediumEcpm.OnAdLoaded += HandleRewardBasedVideoLoaded_M_Ecpm;
        // Called when an ad request failed to load.
        rewardBasedVideoMediumEcpm.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad_M_Ecpm;
        // Called when an ad is shown.
        rewardBasedVideoMediumEcpm.OnAdOpening += HandleRewardBasedVideoOpened_M_Ecpm;


        rewardBasedVideoMediumEcpm.OnAdFailedToShow += HandleRewardedAdFailedToShow_M_Ecpm;

        rewardBasedVideoMediumEcpm.OnUserEarnedReward += HandleRewardBasedVideoRewarded_M_Ecpm;

        rewardBasedVideoMediumEcpm.OnAdClosed += HandleRewardBasedVideoClosed_M_Ecpm;

    }
    private void BindRewardedEvents_L_Ecpm()
    {

        rewardBasedVideoLowEcpm.OnAdLoaded += HandleRewardBasedVideoLoaded_L_Ecpm;

        rewardBasedVideoLowEcpm.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad_L_Ecpm;

        rewardBasedVideoLowEcpm.OnAdOpening += HandleRewardBasedVideoOpened_L_Ecpm;

        rewardBasedVideoLowEcpm.OnAdFailedToShow += HandleRewardedAdFailedToShow_L_Ecpm;

        rewardBasedVideoLowEcpm.OnUserEarnedReward += HandleRewardBasedVideoRewarded_L_Ecpm;

        rewardBasedVideoLowEcpm.OnAdClosed += HandleRewardBasedVideoClosed_L_Ecpm;

    }
    #endregion

    #region RewardedVideoEvents
    //***** Rewarded Events *****//
    public void HandleRewardBasedVideoLoaded_H_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_High_Ecpm)
            {
                RewardVideoMediumECPM -= LoadRewardedVideo;
                rAdStatus = AdsLoadingStatus.Loaded;
                UnityRewarded = false;
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Loaded_High_Ecpm);

            }
        });
    }

    public void HandleRewardBasedVideoFailedToLoad_H_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_High_Ecpm)
            {
                rAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:rad:NoInventory_H_Ecpm :: " + args.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_NoInventory_High_Ecpm);
                RewardVideo_High_Ecpm = false;
                RewardVideo_Medium_Ecpm = true;
                RewardVideo_Low_Ecpm = false;
                UnityRewarded = false;
                if (RewardVideoMediumECPM != null)
                {
                    RewardVideoMediumECPM();

                }
            }
        });

    }
    public void HandleRewardedAdFailedToShow_H_Ecpm(object sender, AdErrorEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_High_Ecpm)
            {

                rAdStatus = AdsLoadingStatus.NotLoaded;

            }
        });
    }
    public void HandleRewardBasedVideoOpened_H_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_High_Ecpm)
            {
                RewardVideoMediumECPM -= LoadRewardedVideo;
                rAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Displayed_High_Ecpm);

            }
        });

    }




    public void HandleRewardBasedVideoClosed_H_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_High_Ecpm)
            {
                RewardVideoMediumECPM -= LoadRewardedVideo;
                rAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:rad:Closed_H_Ecpm");
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Closed_High_Ecpm);

                this.rewardBasedVideoHighEcpm = new RewardedAd(ADMOB_ID.RewardedVideo_High_Ecpm);

                LoadRewardedVideo();
            }

        });


    }

    public void HandleRewardBasedVideoRewarded_H_Ecpm(object sender, Reward args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_High_Ecpm)
            {
                Logging.Log("GR >> give reward to user after watching rAd_H_Ecpm");
                if (NotifyReward != null)
                    NotifyReward();
                Admob_LogHelper.LogSender(AdmobEvents.RewardedAdReward_H_ECPM);
                RewardVideoMediumECPM -= LoadRewardedVideo;
            }

        });
    }

    //***** Rewarded Events2 *****//
    public void HandleRewardBasedVideoLoaded_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Medium_Ecpm)
            {
                RewardVideoLowECPM -= LoadRewardedVideo;
                rAdStatus = AdsLoadingStatus.Loaded;
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Loaded_High_Ecpm);
                RewardVideo_High_Ecpm = false;
                RewardVideo_Medium_Ecpm = true;
                RewardVideo_Low_Ecpm = false;
                UnityRewarded = false;

            }

        });
    }

    public void HandleRewardBasedVideoFailedToLoad_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Medium_Ecpm)
            {
                rAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:rad:NoInventory_M_Ecpm :: " + args.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_NoInventory_Medium_Ecpm);

                RewardVideo_Medium_Ecpm = false;
                RewardVideo_Low_Ecpm = true;
                UnityRewarded = false;
                if (RewardVideoLowECPM != null)
                    RewardVideoLowECPM();
            }
        });

    }
    public void HandleRewardedAdFailedToShow_M_Ecpm(object sender, AdErrorEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Medium_Ecpm)
            {
                rAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:rad:FailedToShow");

            }
        });
    }
    public void HandleRewardBasedVideoOpened_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Medium_Ecpm)
            {

                RewardVideoLowECPM -= LoadRewardedVideo;
                rAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Displayed_Medium_Ecpm);
            }
        });

    }



    public void HandleRewardBasedVideoClosed_M_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Medium_Ecpm)
            {
                RewardVideoLowECPM -= LoadRewardedVideo;
                rAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:rad:Closed");
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Closed_Medium_Ecpm);

                this.rewardBasedVideoMediumEcpm = new RewardedAd(ADMOB_ID.RewardedVideo_Medium_Ecpm);

                LoadRewardedVideo();
            }
        });


    }

    public void HandleRewardBasedVideoRewarded_M_Ecpm(object sender, Reward args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Medium_Ecpm)
            {
                RewardVideoLowECPM -= LoadRewardedVideo;
                Logging.Log("GR >> give reward to user after watching rAd_M_Ecpm");
                if (NotifyReward != null)
                    NotifyReward();
                Admob_LogHelper.LogSender(AdmobEvents.RewardedAdReward_M_ECPM);
            }
        });
    }
    //***** Rewarded Events3 *****//
    public void HandleRewardBasedVideoLoaded_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Low_Ecpm)
            {
                rAdStatus = AdsLoadingStatus.Loaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Loaded_Low_Ecpm);
                RewardVideo_High_Ecpm = false;
                RewardVideo_Medium_Ecpm = false;
                RewardVideo_Low_Ecpm = true;
                UnityRewarded = false;
            }
        });
    }

    public void HandleRewardBasedVideoFailedToLoad_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Low_Ecpm)
            {

                rAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:rad:NoInventory_L_Ecpm :: " + args.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_NoInventory_Low_Ecpm);
                RewardVideo_High_Ecpm = false;
                RewardVideo_Medium_Ecpm = false;
                RewardVideo_Low_Ecpm = false;
                UnityRewarded = true;
                if (RewardVideo_Unity != null)
                {
                    RewardVideo_Unity();
                }


            }
        });

    }
    public void HandleRewardedAdFailedToShow_L_Ecpm(object sender, AdErrorEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Low_Ecpm)
            {

                rAdStatus = AdsLoadingStatus.NotLoaded;

            }
        });
    }
    public void HandleRewardBasedVideoOpened_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Low_Ecpm)
            {

                rAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Displayed_Low_Ecpm);
            }

        });

    }




    public void HandleRewardBasedVideoClosed_L_Ecpm(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Low_Ecpm)
            {
                rAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedVideo_Closed_Low_Ecpm);

                this.rewardBasedVideoLowEcpm = new RewardedAd(ADMOB_ID.RewardedVideo_Low_Ecpm);

                LoadRewardedVideo();
            }
        });


    }

    public void HandleRewardBasedVideoRewarded_L_Ecpm(object sender, Reward args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardVideo_Low_Ecpm)
            {
                Logging.Log("GR >> give reward to user after watching rAd_L_Ecpm");
                if (NotifyReward != null)
                    NotifyReward();
                Admob_LogHelper.LogSender(AdmobEvents.RewardedAdReward_L_ECPM);
            }
        });
    }

    #endregion

    #region RewardedInterstial
    public override void LoadRewardedInterstitial()
    {
        if (!isAdmobInitialized || IsRewardedInterstitialAdReady() || riAdStatus == AdsLoadingStatus.Loading)
        {
            return;
        }

        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            if (RewardInterstitial_High_ECPM)
            {
                RewardInterstitialMediumECPM += LoadRewardedInterstitial;
                RewardInterstitialLowECPM -= LoadRewardedInterstitial;

                Admob_LogHelper.LogSender(AdmobEvents.LoadRewardedInterstitialAd_H_ECPM);
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded ad with the request.
                RewardedInterstitialAd.LoadAd(ADMOB_ID.RewardedInt_High_Ecpm, request, adLoadCallbackHighECPM);
                riAdStatus = AdsLoadingStatus.Loading;
            }
            if (RewardInterstitial_Medium_Ecpm)
            {
                RewardInterstitialMediumECPM -= LoadRewardedInterstitial;
                RewardInterstitialLowECPM += LoadRewardedInterstitial;

                Admob_LogHelper.LogSender(AdmobEvents.LoadRewardedInterstitialAd_M_ECPM);
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded ad with the request.
                RewardedInterstitialAd.LoadAd(ADMOB_ID.RewardedInt_Medium_Ecpm, request, adLoadCallbackMediumECPM);
                riAdStatus = AdsLoadingStatus.Loading;
            }
            if (RewardInterstitial_Low_Ecpm)
            {

                Admob_LogHelper.LogSender(AdmobEvents.LoadRewardedInterstitialAd_L_ECPM);
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the rewarded ad with the request.
                RewardedInterstitialAd.LoadAd(ADMOB_ID.RewardedInt_Low_Ecpm, request, adLoadCallbackLowECPM);
                riAdStatus = AdsLoadingStatus.Loading;
            }

        }

    }
    public override void ShowRewardedInterstitialAd(RewardUserDelegate _delegate)
    {
        if (RewardInterstitial_High_ECPM)
        {

            NotifyReward = _delegate;
            Admob_LogHelper.LogSender(AdmobEvents.ShowRewardedInterstitialAd_H_ECPM);

            if (this.rewardedInterstitialAdHighECPM != null)
            {
                if (rewardedInterstitialHighECPMLoaded)
                {


                    //  AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedInterstitialAdWillDisplay_H_ECPM);
                    this.rewardedInterstitialAdHighECPM.Show(userEarnedRewardCallback);
                }
            }

        }
        else if (RewardInterstitial_Medium_Ecpm)
        {

            NotifyReward = _delegate;
            Admob_LogHelper.LogSender(AdmobEvents.ShowRewardedInterstitialAd_M_ECPM);

            if (this.rewardedInterstitialAdMediumECPM != null)
            {
                if (rewardedInterstitialMediumECPMLoaded)
                {


                    //   AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedInterstitialAdWillDisplay_M_ECPM);
                    this.rewardedInterstitialAdMediumECPM.Show(userEarnedRewardCallback);
                }
            }
        }
        else if (RewardInterstitial_Low_Ecpm)
        {

            NotifyReward = _delegate;
            Admob_LogHelper.LogSender(AdmobEvents.ShowRewardedInterstitialAd_L_ECPM);

            if (this.rewardedInterstitialAdLowECPM != null)
            {
                if (rewardedInterstitialLowECPMLoaded)
                {


                    //  AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedInterstitialAdWillDisplay_L_ECPM);
                    this.rewardedInterstitialAdLowECPM.Show(userEarnedRewardCallback);
                }
            }
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
        //Logging.Log("User Rewarded");
    }
    public override bool IsRewardedInterstitialAdReady()
    {
        if (this.rewardedInterstitialAdHighECPM != null)
        {
            if (rewardedInterstitialHighECPMLoaded)
            {
                return true;
            }
        }
        if (this.rewardedInterstitialAdMediumECPM != null)
        {
            if (rewardedInterstitialMediumECPMLoaded)
            {
                return true;
            }
        }
        if (this.rewardedInterstitialAdLowECPM != null)
        {
            if (rewardedInterstitialLowECPMLoaded)
            {
                return true;
            }
        }
        return false;


    }

    private void adLoadCallbackHighECPM(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            rewardedInterstitialAdHighECPM = ad;

            rewardedInterstitialAdHighECPM.OnAdFailedToPresentFullScreenContent += RewardedInterstitialHandleAdFailedToPresentHighECPM;
            rewardedInterstitialAdHighECPM.OnAdDidPresentFullScreenContent += RewardedInterstitialHandleAdDidPresentHighECPM;
            rewardedInterstitialAdHighECPM.OnAdDidDismissFullScreenContent += RewardedInterstitialHandleAdDidDismissHighECPM;
            rewardedInterstitialAdHighECPM.OnPaidEvent += RewardedInterstitialHandlePaidEventHighECPM;

            rewardedInterstitialHighECPMLoaded = true;
        }
        else
        {
            // Handle the error.
            Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                riAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:riad:NoInventory_H_Ecpm :: " + error.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialNoInventory_H_ECPM);
                RewardInterstitial_High_ECPM = false;
                RewardInterstitial_Medium_Ecpm = true;
                RewardInterstitial_Low_Ecpm = false;
                if (RewardInterstitialMediumECPM != null)
                    RewardInterstitialMediumECPM();
            });
            return;
        }

    }

    private void adLoadCallbackMediumECPM(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            rewardedInterstitialAdMediumECPM = ad;

            rewardedInterstitialAdMediumECPM.OnAdFailedToPresentFullScreenContent += RewardedInterstitialHandleAdFailedToPresentMediumECPM;
            rewardedInterstitialAdMediumECPM.OnAdDidPresentFullScreenContent += RewardedInterstitialHandleAdDidPresentMediumECPM;
            rewardedInterstitialAdMediumECPM.OnAdDidDismissFullScreenContent += RewardedInterstitialHandleAdDidDismissMediumECPM;
            rewardedInterstitialAdMediumECPM.OnPaidEvent += RewardedInterstitialHandlePaidEventMediumECPM;
            Logging.Log("GR >> Admob:riad:Loaded_M_ECPM");
            rewardedInterstitialMediumECPMLoaded = true;
        }
        else
        {
            // Handle the error.
            Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                riAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:riad:NoInventory_M_Ecpm :: " + error.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialNoInventory_M_ECPM);
                RewardInterstitial_High_ECPM = false;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = true;
                if (RewardInterstitialLowECPM != null)
                    RewardInterstitialLowECPM();
            });
            return;
        }

    }

    private void adLoadCallbackLowECPM(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            rewardedInterstitialAdLowECPM = ad;

            rewardedInterstitialAdLowECPM.OnAdFailedToPresentFullScreenContent += RewardedInterstitialHandleAdFailedToPresentLowECPM;
            rewardedInterstitialAdLowECPM.OnAdDidPresentFullScreenContent += RewardedInterstitialHandleAdDidPresentLowECPM;
            rewardedInterstitialAdLowECPM.OnAdDidDismissFullScreenContent += RewardedInterstitialHandleAdDidDismissLowECPM;
            rewardedInterstitialAdLowECPM.OnPaidEvent += RewardedInterstitialHandlePaidEventLowECPM;
            Logging.Log("GR >> Admob:riad:Loaded_L_ECPM");
            rewardedInterstitialLowECPMLoaded = true;
        }
        else
        {
            // Handle the error.
            Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                riAdStatus = AdsLoadingStatus.NoInventory;
                Logging.Log("GR >> Admob:riad:NoInventory_L_Ecpm :: " + error.ToString());
                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialNoInventory_L_ECPM);
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
            });
            return;
        }

    }
    #endregion

    #region RewardedInterstitialCallbackHandler

    ///////// Rewarded Interstitial High ECPM Callbacks //////////
    private void RewardedInterstitialHandleAdFailedToPresentHighECPM(object sender, AdErrorEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_High_ECPM)
            {
                riAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:riad:FailedToShow:HCPM");
            }
        });
    }

    private void RewardedInterstitialHandleAdDidPresentHighECPM(object sender, EventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has presented.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_High_ECPM)
            {
                RewardInterstitialMediumECPM -= LoadRewardedInterstitial;
                riAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdDisplayed_H_ECPM);

            }
        });
    }

    private void RewardedInterstitialHandleAdDidDismissHighECPM(object sender, EventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has dismissed presentation.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_High_ECPM)
            {
                rewardedInterstitialHighECPMLoaded = false;

                RewardInterstitialMediumECPM -= LoadRewardedInterstitial;
                riAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdClosed_H_ECPM);
                NotifyReward();
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
                LoadRewardedInterstitial();
            }

        });
    }

    private void RewardedInterstitialHandlePaidEventHighECPM(object sender, AdValueEventArgs args)
    {
        MonoBehaviour.print(
            "Rewarded interstitial ad has received a paid event.");

        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_High_ECPM)
            {
                RewardInterstitialMediumECPM -= LoadRewardedInterstitial;
                Logging.Log("GG >> give reward to user after watching riAd_H_Ecpm");
                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdReward_H_ECPM);
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
            }

        });


    }


    ///////// Rewarded Interstitial Medium ECPM Callbacks //////////
    private void RewardedInterstitialHandleAdFailedToPresentMediumECPM(object sender, AdErrorEventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has failed to present.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Medium_Ecpm)
            {
                riAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GG >> Admob:riad:FailedToShow:MECPM");
            }
        });
    }

    private void RewardedInterstitialHandleAdDidPresentMediumECPM(object sender, EventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has presented.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Medium_Ecpm)
            {
                RewardInterstitialLowECPM -= LoadRewardedInterstitial;
                riAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GG >> Admob:riad:Displayed_M_Ecpm");
                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdDisplayed_M_ECPM);

            }
        });
    }

    private void RewardedInterstitialHandleAdDidDismissMediumECPM(object sender, EventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has dismissed presentation.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Medium_Ecpm)
            {
                rewardedInterstitialMediumECPMLoaded = false;

                RewardInterstitialLowECPM -= LoadRewardedInterstitial;
                riAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GG >> Admob:riad:Closed_M_Ecpm");
                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdClosed_M_ECPM);
                NotifyReward();
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
                LoadRewardedInterstitial();
            }

        });
    }

    private void RewardedInterstitialHandlePaidEventMediumECPM(object sender, AdValueEventArgs args)
    {
        MonoBehaviour.print(
            "Rewarded interstitial ad has received a paid event.");

        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Medium_Ecpm)
            {
                RewardInterstitialLowECPM -= LoadRewardedInterstitial;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdReward_M_ECPM);
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
            }

        });
    }


    ///////// Rewarded Interstitial Low ECPM Callbacks ///////////
    private void RewardedInterstitialHandleAdFailedToPresentLowECPM(object sender, AdErrorEventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has failed to present.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Low_Ecpm)
            {
                riAdStatus = AdsLoadingStatus.NotLoaded;
                Logging.Log("GR >> Admob:riad:FailedToShow:LECPM");
            }
        });
    }

    private void RewardedInterstitialHandleAdDidPresentLowECPM(object sender, EventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has presented.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Low_Ecpm)
            {
                riAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdDisplayed_L_ECPM);

            }
        });
    }

    private void RewardedInterstitialHandleAdDidDismissLowECPM(object sender, EventArgs args)
    {
        Logging.Log("Rewarded interstitial ad has dismissed presentation.");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Low_Ecpm)
            {
                rewardedInterstitialLowECPMLoaded = false;

                riAdStatus = AdsLoadingStatus.NotLoaded;

                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdClosed_L_ECPM);
                NotifyReward();
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
                LoadRewardedInterstitial();
            }

        });
    }

    private void RewardedInterstitialHandlePaidEventLowECPM(object sender, AdValueEventArgs args)
    {
        MonoBehaviour.print(
            "Rewarded interstitial ad has received a paid event.");

        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            if (RewardInterstitial_Low_Ecpm)
            {

                Admob_LogHelper.LogSender(AdmobEvents.RewardedInterstitialAdReward_L_ECPM);
                RewardInterstitial_High_ECPM = true;
                RewardInterstitial_Medium_Ecpm = false;
                RewardInterstitial_Low_Ecpm = false;
            }

        });
    }


    #endregion


    #region UnityCallBack
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Enter1");
        // Optionally execute code if the Ad Unit successfully loads content.
        if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
        {
            iAdStatus = AdsLoadingStatus.Loaded;
            UnityAds = true;
            Interstitial_HighEcpm = false;
            Interstitial_MediumEcpm = false;
            Interstitial_LowEcpm = false;
            Debug.Log("Enter22");
        }
        else if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
        {
            rAdStatus = AdsLoadingStatus.Loaded;
            RewardVideo_High_Ecpm = false;
            RewardVideo_Medium_Ecpm = false;
            RewardVideo_Low_Ecpm = false;
            UnityRewarded = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
        {
            iAdStatus = AdsLoadingStatus.Loaded;
            UnityAds = true;
            Interstitial_HighEcpm = false;
            Interstitial_MediumEcpm = false;
            Interstitial_LowEcpm = false;
            Debug.Log("Ad_Failed");
        }
        else if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
        {
            rAdStatus = AdsLoadingStatus.Loaded;
            RewardVideo_High_Ecpm = false;
            RewardVideo_Medium_Ecpm = false;
            RewardVideo_Low_Ecpm = false;
            UnityRewarded = true;
            Debug.Log("Ad_Failed");
        }
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
        {
            iAdStatus = AdsLoadingStatus.Loaded;
            UnityAds = false;
            Interstitial_HighEcpm = true;
            Interstitial_MediumEcpm = false;
            Interstitial_LowEcpm = false;
            Debug.Log("Ad_Failed");
        }
        else if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
        {
            rAdStatus = AdsLoadingStatus.Loaded;
            RewardVideo_High_Ecpm = true;
            RewardVideo_Medium_Ecpm = false;
            RewardVideo_Low_Ecpm = false;
            UnityRewarded = false;
            Debug.Log("Ad_Failed");
        }
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {


        //  ADmobInterstial = true;
        if (adUnitId == ADMOB_ID.Unity_Interstitial_ID)
        {
            iAdStatus = AdsLoadingStatus.NotLoaded;
            Interstitial_HighEcpm = true;
            Interstitial_MediumEcpm = false;
            Interstitial_LowEcpm = false;
            UnityAds = false;
            Debug.Log("Ad_completed");
        }
        else

        if (adUnitId.Equals(ADMOB_ID.Unity_RewardedVideo) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            if (adUnitId == ADMOB_ID.Unity_RewardedVideo)
            {
                RewardVideo_High_Ecpm = true;
                RewardVideo_Medium_Ecpm = false;
                RewardVideo_Low_Ecpm = false;
                UnityRewarded = false;
                NotifyReward();
                Debug.Log("Ad_completed");
            }
            // Load another ad:
            Advertisement.Load(ADMOB_ID.Unity_RewardedVideo, this);
        }



    }

    #endregion


    //#region fireBaseCode 
    //public static bool isFirebaseInitialized = false;

    //void InitializeFirebase()
    //{
    //    // [START set_defaults]
    //    System.Collections.Generic.Dictionary<string, object> defaults =
    //      new System.Collections.Generic.Dictionary<string, object>();

    //    // These are the values that are used if we haven't fetched data from the
    //    // server
    //    // yet, or if we ask for values that the server doesn't have:


    //    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
    //      .ContinueWithOnMainThread(task =>
    //      {
    //          // [END set_defaults]
    //          Logging.Log("RemoteConfig configured and ready!");
    //          isFirebaseInitialized = true;
    //          FetchDataAsync();
    //      });
    //}

    //public void DisplayData()
    //{
    //    Logging.Log("Current Data:");

    //    //Logging.Log("FireBaseADID: " +
    //    //         Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
    //    //         .GetValue(FireBaseADID).LongValue);
    //    //if (Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
    //    //         .GetValue(FireBaseADID).LongValue <= 3 && Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
    //    //         .GetValue(FireBaseADID).LongValue >= 1)
    //    //{

    //    //    ADMOB_ID.Intersitial_High_Ecpm = Convert.ToInt32(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance
    //    //             .GetValue(FireBaseADID).LongValue);
    //    //}
    //    //else
    //    //{
    //    //    ServerADValue = 3;
    //    //}
    //    if (!EnableTestModeAds)
    //    {

    //        if (Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseIntersitialHighEcpm).StringValue != null)
    //        {

    //            ADMOB_ID.Intersitial_High_Ecpm = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseIntersitialHighEcpm).StringValue;
    //            Interstitial_HighEcpm = true;
    //            Interstitial_MediumEcpm = false;
    //            Interstitial_LowEcpm = false;
    //        }
    //        if (Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseIntersitialMediumEcpm).StringValue != null)
    //            ADMOB_ID.Intersitial_Medium_Ecpm = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseIntersitialMediumEcpm).StringValue;


    //        Debug.Log("GR >>" + ADMOB_ID.Intersitial_High_Ecpm);

    //        Debug.Log("GR >>" + ADMOB_ID.Intersitial_Medium_Ecpm);
    //        if (Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseFirstSmallBannerMediumEcpm).StringValue != null)
    //        {

    //            ADMOB_ID.SmallBanner_First_Medium_Ecpm = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseFirstSmallBannerMediumEcpm).StringValue;
    //            FirstBanner_Medium_Ecpm = true;
    //            FirstBanner_Low_Ecpm = false;

    //        }

    //        if (Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseSecondSmallBannerMediumEcpm).StringValue != null)
    //        {

    //            ADMOB_ID.SmallBanner_Second_Medium_Ecpm = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseSecondSmallBannerMediumEcpm).StringValue;
    //            SecondBanner_Medium_Ecpm = true;
    //            SecondBanner_Low_Ecpm = false;

    //        }
    //        if (Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseMediumBannerMediumEcpm).StringValue != null)
    //        {

    //            ADMOB_ID.MediumBanner_Medium_Ecpm = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseMediumBannerMediumEcpm).StringValue;
    //            MediumBanner_Medium_Ecpm = true;
    //            MediumBanner_Low_Ecpm = false;



    //            #if UNITY_IOS
    //            ServerAds = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(ADSSwitch).BooleanValue;

    //            #endif
    //        }
    //    }
    //        else
    //        {
    //            ADMOB_ID.Intersitial_High_Ecpm = TestAdmob_ID.Intersitial_Low_Ecpm;
    //            //Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseIntersitialHighEcpm).StringValue;
    //            ADMOB_ID.Intersitial_Medium_Ecpm = TestAdmob_ID.Intersitial_Low_Ecpm;
    //            Debug.Log("GR >>" + ADMOB_ID.Intersitial_High_Ecpm);

    //            Debug.Log("GR >>" + ADMOB_ID.Intersitial_Medium_Ecpm);
    //            //Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(FireBaseIntersitialMediumEcpm).StringValue;
    //            Interstitial_HighEcpm = true;
    //            Interstitial_MediumEcpm = false;
    //            Interstitial_LowEcpm = false;
    //            //   ADMOB_ID.smallBanner_First_High_Ecpm = TestAdmob_ID.smallBanner_First_High_Ecpm;
    //            ADMOB_ID.SmallBanner_First_Medium_Ecpm = TestAdmob_ID.Small_banners_First_Ecpm;
    //            //  FirstBanner_High_Ecpm = true;
    //            FirstBanner_Medium_Ecpm = true;
    //            FirstBanner_Low_Ecpm = false;

    //            ADMOB_ID.SmallBanner_Second_Medium_Ecpm = TestAdmob_ID.Small_banners_First_Ecpm;
    //            SecondBanner_Medium_Ecpm = true;
    //            SecondBanner_Low_Ecpm = false;

    //            ADMOB_ID.MediumBanner_Medium_Ecpm = TestAdmob_ID.MediumBanner_Ecpm;
    //            MediumBanner_Medium_Ecpm = true;
    //            MediumBanner_Low_Ecpm = false;

    //        }

    //        InitAdmob();
    //    }

    //    public Task FetchDataAsync()
    //    {
    //        Logging.Log("Fetching data...");
    //        System.Threading.Tasks.Task fetchTask =
    //        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
    //            TimeSpan.Zero);
    //        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    //    }

    //    void FetchComplete(Task fetchTask)
    //    {
    //        if (fetchTask.IsCanceled)
    //        {
    //            Logging.Log("Fetch canceled.");
    //        }
    //        else if (fetchTask.IsFaulted)
    //        {
    //            Logging.Log("Fetch encountered an error.");
    //        }
    //        else if (fetchTask.IsCompleted)
    //        {
    //            Logging.Log("Fetch completed successfully!");
    //        }

    //        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
    //        switch (info.LastFetchStatus)
    //        {
    //            case Firebase.RemoteConfig.LastFetchStatus.Success:
    //                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
    //                .ContinueWithOnMainThread(task =>
    //                {
    //                    Logging.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
    //                                   info.FetchTime));
    //                    DisplayData();
    //                });

    //                break;
    //            case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //                InitAdmob();
    //                switch (info.LastFetchFailureReason)
    //                {

    //                    case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                        Logging.Log("Fetch failed for unknown reason");

    //                        break;
    //                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                        Logging.Log("Fetch throttled until " + info.ThrottledEndTime);
    //                        break;
    //                }
    //                break;
    //            case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //                Logging.Log("Latest Fetch call still pending.");
    //                InitAdmob();
    //                break;
    //        }
    //    }
    //    #endregion
    }
