using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManagerObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ObjectivePannel, Pause, Fail, Complete, controls, Loading,OutOfFuel,error;
    public Text ObjectiveText;
    public static UiManagerObject instance;
    public int TotalLevels;
    public Image fuelBar;
    public GameObject FadeImage;
    public GameObject FadeImage2;
    public GameObject CutScene;
    public GameObject MiniMap;
    public Image NosFiller;
    public GameObject NosButton;
    public GameObject Player;
    public Text EnemyCountShow;
    void Awake()
    {

        instance = this;
        SoundManager.Instance.PlayAudio(SoundManager.Instance.BgSound);
        Time.timeScale = 1f;
        
        
    }
    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());


     if (PrefsManager.GetLevelMode() == 1)
        {
           
            //MiniMap.GetComponent<CanvasGroup>().alpha = 0;

        }
     if(PrefsManager.GetGameMode()== "free") 
        {
            //MiniMap.GetComponent<CanvasGroup>().alpha = 0;
            Admob_LogHelper.MissionOrLevelStartedEventLog("Free",0);

        }
     else
     {
         Admob_LogHelper.MissionOrLevelStartedEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
     }

     AdmobAdsManager.Instance.ShowBanner();
     AdmobAdsManager.Instance.ShowBanner2();
     AdmobAdsManager.Instance.HideMediumBannerEvent();
     AdmobAdsManager.Instance.LoadInterstitialAd();
    }

    public void ShowObjective(string statment)
    {
        ObjectiveText.text = statment;
        //if (LevelManager.instace.FreeMode)
        if (PrefsManager.GetGameMode() == "free")
        {
           
            ObjectivePannel.SetActive(false);
            SetTimeScale(1);

           
        }
         else if(PrefsManager.GetLevelMode() == 0){
        ObjectivePannel.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
        }
        else if(PrefsManager.GetLevelMode() ==1){
        ObjectivePannel.SetActive(false);
            //MiniMap.GetComponent<CanvasGroup>().alpha = 0;

        }
        
        
    }


    public void FillFuelbar(float fillAmount) {
        fuelBar.fillAmount = fillAmount;
        if (fillAmount <= 0) {
            OutOfFuel.SetActive(true);
        }
    }

    public void FillFuelTank() {
        if (PrefsManager.GetCoinsValue() > 1000)
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - 1000);
        else { 
            error.SetActive(true);
            Invoke("OffError",4f);
        }
     //   LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);
    }


    public void OffError() {
        error.SetActive(false);

    }

   public void FullTankWithVideo() { 
        //LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);

    }

    public void HideGamePlay()
    {
        controls.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void ShowGamePlay()
    {
        controls.GetComponent<CanvasGroup>().alpha = 1;

    }
    public void HideObjectivePannel()
    {
        AdmobAdsManager.Instance.LoadInterstitialAd();
        ObjectivePannel.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
       
    }

    public void ShowPause()
    {
        
       
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        AdmobAdsManager.Instance.ShowInt(ShowPauseNow,true);
    }

    public void ShowPauseNow()
    {
        Pause.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
    }

    public void Resume()
    {
        Pause.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        AdmobAdsManager.Instance.LoadInterstitialAd();
    }

    public void Restart()
    {
        Loading.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);


    }

    public void Home()
    {
        Loading.SetActive(true);
        SceneManager.LoadSceneAsync(1);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);

    }

    public void LevelCompleteNow()
    {
        Complete.SetActive(true);
        // SetTimeScale(0);
        HideGamePlay();
        if (PrefsManager.GetGameMode() != "free")
        {

            if (PrefsManager.GetLevelMode() == 0)
            {
                Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
                if (PrefsManager.GetCurrentLevel() >= PrefsManager.GetLevelLocking())
                {
                    PrefsManager.SetLevelLocking(PrefsManager.GetLevelLocking() + 1);

                }
                Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
            }
            else if (PrefsManager.GetLevelMode() == 1)
            {
                Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
                if ((PrefsManager.GetCurrentLevel()) >= PrefsManager.GetSnowLevelLocking())
                {
                    PrefsManager.SetSnowLevelLocking(PrefsManager.GetSnowLevelLocking() + 1);

                }
                Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
            }
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
            //  Data.SendCompleteEvent(PrefsManager.GetCurrentLevel());
            Admob_LogHelper.MissionOrLevelCompletedEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());

        }
        else { 
            //Data.SendCompleteEvent(20);

            Admob_LogHelper.MissionOrLevelCompletedEventLog("Free",0);

        }

    }

    public async void ShowComplete()
    {
        //Debug.Log("Called ");
        await Task.Delay(2000);
      AdmobAdsManager.Instance.ShowInt(LevelCompleteNow,false);
        SoundManager.Instance?.PlayAudio2(SoundManager.Instance.LevelComplete);

    }

    public  void ShowFail()
    {
       
        SoundManager.Instance.PlayAudio2(SoundManager.Instance.levelFail);
      //  Data.SendFailEvent(PrefsManager.GetCurrentLevel());
          AdmobAdsManager.Instance.ShowInt(ShowLevelFailNow,true);

    }

    public void ShowLevelFailNow()
    {
        
        Fail.SetActive(true);
        SetTimeScale(0);
        HideGamePlay();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
       Admob_LogHelper.MissionOrLevelFailEventLog(PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
    }

    public void Next()
    {
     
        PrefsManager.SetCurrentLevel(PrefsManager.GetCurrentLevel()+1);
        Loading.SetActive(true);
        if (PrefsManager.GetCurrentLevel() >= TotalLevels)
        { 
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex-1);

        }
        else
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void SetTimeScale(float timescale)
    {

        Time.timeScale = timescale;
    }

    public void ResetCarOnButtonPressed()
    {
        Player.GetComponent<RCC_CarControllerV3>().ResetCarBtn();  

    }
}
