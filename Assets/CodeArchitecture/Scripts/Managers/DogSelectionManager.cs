using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class Specification
{
    public int[] Values;


}

public class DogSelectionManager : MonoBehaviour
{

    public Specification[] SpecificationValue;
    public GameObject[] sp_bar;
    public int[] Prices;
    int selectedDogValue = 0;
    public GameObject[] selectedDogArray;
    public GameObject nextBtn, backBtn,nextDirt,beckDirt, dogSelectionCanvas, menuCanvas, 
        levelSelectionCanvas, lockSprite, LOADING, Play, notCash, successPannel, unlockPlayerButton,TestDriveButton
        /*,DirtNextBack*/,MainNextBack;
    public Text coinText,coinText2, ls_cointext, PriceText;
    bool isReadyForPurchase;
    int ActiveDog = 1;
    int coinValue;
    public GameObject Env, playerParent;
    private GameObject CurrentEnv;
    public static DogSelectionManager instance;
    public int[] DirtModeVehicle;

  //  public SmoothFollow MainCamera;
    // Use this for initialization

    void Start()
    {
        instance = this;
        coinText.text = "" + PrefsManager.GetCoinsValue();
        coinText2.text = "" + PrefsManager.GetCoinsValue();

        Time.timeScale = 1;
        CurrentEnv = Env;CurrentEnv.SetActive(true);
      //  CurrentEnv = Instantiate(Env, playerParent.transform);
        //if (PrefsManager.GetComeForSelection() == 1)
        //{


        //    dogSelectionCanvas.SetActive(true);
        //    menuCanvas.SetActive(false);
        //    if (CurrentPlayer != null)
        //        //CurrentPlayer.transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
        //    //  selectedDogArray[PrefsManager.GetLastJeepUnlock()]
        //    ShowDog(PrefsManager.GetLastJeepUnlock());
        //    selectedDogValue = PrefsManager.GetLastJeepUnlock();
        //    PrefsManager.SetComeForSelection(0);

        //}
    }



    private void Update()
    {
        coinText.text = "" + PrefsManager.GetCoinsValue();
        coinText2.text = "" + PrefsManager.GetCoinsValue();
        ls_cointext.text = "" + PrefsManager.GetCoinsValue();
    }


    int selectedDirtCar = 0;
    public void OnNextDirt() {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedDirtCar < DirtModeVehicle.Length - 1)
        {
            selectedDirtCar++;
            ShowDog(DirtModeVehicle[selectedDirtCar]);
            if (selectedDirtCar == DirtModeVehicle.Length - 1)
            {
                nextDirt.SetActive(false);
                beckDirt.SetActive(true);
            }
            else
            {
                //	Debug.Log ("Called" + selectedDogValue);
                nextDirt.SetActive(true);
                beckDirt.SetActive(true);
            }
        }
    }
    public void OnPreviousDirt() {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedDirtCar > 0)
        {
            selectedDirtCar--;
            ShowDog(DirtModeVehicle[selectedDirtCar]);
            if (selectedDirtCar == 0)
            {
                // selectedDogArray[0].transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
                nextDirt.SetActive(true);
                beckDirt.SetActive(false);
            }
            else
            {
                //Debug.Log ("Called" + selectedDogValue);
                nextDirt.SetActive(true);
                beckDirt.SetActive(true);
            }
        }
    }




    public void OnNextPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedDogValue < selectedDogArray.Length - 1)
        {
            selectedDogValue++;
            ShowDog(selectedDogValue);
            if (selectedDogValue == selectedDogArray.Length - 1)
            {
                nextBtn.SetActive(false);
                backBtn.SetActive(true);
            }
            else
            {
                //	Debug.Log ("Called" + selectedDogValue);
                nextBtn.SetActive(true);
                backBtn.SetActive(true);
            }
        }

    }

    public void OnBackPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedDogValue > 0)
        {
            selectedDogValue--;
            ShowDog(selectedDogValue);
            if (selectedDogValue == 0)
            {
               // selectedDogArray[0].transform.GetChild(0).GetComponent<AudioSource>().enabled = true;
                nextBtn.SetActive(true);
                backBtn.SetActive(false);
            }
            else
            {
                //Debug.Log ("Called" + selectedDogValue);
                nextBtn.SetActive(true);
                backBtn.SetActive(true);
            }
        }

    }


    GameObject CurrentPlayer = null;
    public void ShowDog(int val)
    {
        //Debug.Log("Value to show is " + val);
        // if (CurrentPlayer != null)
        //    Destroy(CurrentPlayer);
        if (CurrentPlayer)
        {
            //CurrentPlayer.GetComponentInChildren<Animator>().SetBool("out",true);
           // CurrentPlayer.GetComponentInChildren<Animator>().SetBool("in", false);
        }
        ActiveDog = val;
        PriceText.text = Prices[val] + "";
        if (PrefsManager.GetPlayerState(val) == 0 && val != 0)
        {
            PriceText.transform.parent.gameObject.SetActive(true);
            lockSprite.SetActive(true);
            Play.SetActive(false);
            unlockPlayerButton.SetActive(true);
            TestDriveButton.SetActive(true);
        }
        else
        {
            lockSprite.SetActive(false);
            PriceText.transform.parent.gameObject.SetActive(false);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);

        }

        for (int i = 0; i < selectedDogArray.Length; i++)
        {
            selectedDogArray[i].SetActive(false);

        }
        PrefsManager.SetSelectedPlayerValue(val);
        CurrentPlayer = selectedDogArray[val];
        CurrentPlayer.SetActive(true);
      //  CurrentPlayer.GetComponentInChildren<Animator>().SetBool("in", true);
        //CurrentPlayer.GetComponentInChildren<Animator>().SetBool("out", false);
        Invoke("SetTarget",0.3f);
        //Debug.Log("Selected player is " + PrefsManager.GetSelectedPlayerValue());
        //CurrentPlayer = Instantiate(selectedDogArray[val], playerParent.transform);


        for (int i = 0; i < sp_bar.Length; i++)
        {
            sp_bar[i].GetComponent<filled>().SetEndpoint(SpecificationValue[val].Values[i]);
        }


        if (PrefsManager.GetLevelMode() != 2)
        {
            if (ActiveDog == selectedDogArray.Length - 1)
            {
                nextBtn.SetActive(false);
                backBtn.SetActive(true);
            }
            else if (ActiveDog == 0)
            {
                nextBtn.SetActive(true);
                backBtn.SetActive(false);
            }
            else
            {
                //	Debug.Log ("Called" + selectedDogValue);
                nextBtn.SetActive(true);
                backBtn.SetActive(true);
            }
        }
        else {
            PrefsManager.SetDirtSelectedPlayerValue(selectedDirtCar);
        }
        selectedDogValue = ActiveDog;
    }

    public void TestDrive()
    {
        lockSprite.SetActive(false);
        PriceText.transform.parent.gameObject.SetActive(false);
        Play.SetActive(true);
        unlockPlayerButton.SetActive(false);
        TestDriveButton.SetActive(false);
        PrefsManager.SetSelectedPlayerValue(selectedDogValue);
        SelectDogPlay();
     
    }

    public void SetTarget() {
      //  MainCamera.target = selectedDogArray[selectedDogValue].transform.GetChild(0).GetChild(0);
    }

    public void Ps_PlayEvent()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        AdmobAdsManager.Instance.ShowInt(GoToLevelSelect, false);
    }

    public void GoToLevelSelect()
        {
            // CurrentEnv.SetActive(false);
            for (int i = 0; i < selectedDogArray.Length; i++)
            {
                selectedDogArray[i].SetActive(false);

            }

            {
                ls_cointext.text = PrefsManager.GetCoinsValue().ToString();
                SelectDogPlay();
            }
        }
    

    public void SelectDogPlay()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        levelSelectionCanvas.SetActive(true);
        //if (PrefsManager.GetGameMode() == "free")
        //{
        //    LOADING.SetActive(true);
        //    LOADING.GetComponentInChildren<bl_SceneLoader>().LoadLevel("Gameplay");
        //    Debug.Log("Called it");
        //}
    }

    public void ForTutorialPlay()
    {

        levelSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        Debug.Log("Enable Here");
        LOADING.SetActive(true);
        PrefsManager.SetGameMode("challange");
        PrefsManager.SetCurrentLevel(1);
        PrefsManager.SetLevelMode(0);
        Debug.Log("Called it");


        //		LoadLevel (2);
        //  LOADING.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");

    }

    public void LevelSelectionPlay()
    {

    }

    public void RedirectToDogSelection()
    {
        AdmobAdsManager.Instance.LoadInterstitialAd();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        CurrentEnv.SetActive(true);
        dogSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        //   selectedDogArray[PrefsManager.GetLastJeepUnlock()].
        if (PrefsManager.GetLevelMode() == 2)
        {
            ShowDog(DirtModeVehicle[PrefsManager.GetDirtSelectedPlayerValue()]);
            //DirtNextBack.SetActive(true);
            MainNextBack.SetActive(false);
            selectedDirtCar = PrefsManager.GetDirtSelectedPlayerValue();
        }
        else { 
            ShowDog(PrefsManager.GetLastJeepUnlock());
            //  DirtNextBack.SetActive(false);
            MainNextBack.SetActive(true);
            selectedDogValue = PrefsManager.GetLastJeepUnlock();
         
        }
        
       
       
        //	nextBtn.SetActive(true);
        //	backBtn.SetActive(false);
    }
    

    void OnEnable()
    {

        //Data.OnRewardedUser += OffNoCash;
    }

    void OnDisable()
    {

        //Data.OnRewardedUser -= OffNoCash;

    }



    public void ShowVideoAds()
    {
        //AdsManager.instance.ShowOnlyUnityReward (0);
    }

    public void GiveVideoReward()
    {
        PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 500);
        successPannel.SetActive(true);
        successPannel.GetComponentInChildren<Text>().text = "    You are rewarded 500 coins.";
        Invoke("Offsuccess", 3f);
        coinText.text = "" + PrefsManager.GetCoinsValue();
        coinText2.text = "" + PrefsManager.GetCoinsValue();
    }

    public void BackToMainCanvas()
    {
        AdmobAdsManager.Instance.LoadInterstitialAd();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        dogSelectionCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        Debug.Log("Enable Here");
        //for (int i = 0; i < selectedDogArray.Length; i++)
        //{
        //	selectedDogArray[i].SetActive(false);

        //}
       // CurrentEnv.SetActive(false);

    }
    
    public void BackFromLevelScreen()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        CurrentEnv.SetActive(true);
        
        levelSelectionCanvas.SetActive(false);
        dogSelectionCanvas.SetActive(true);
        // ShowDog(PrefsManager.GetLastJeepUnlock())
        {
            ShowDog(PrefsManager.GetSelectedPlayerValue());
            // DirtNextBack.SetActive(false);
            MainNextBack.SetActive(true);
            selectedDogValue = PrefsManager.GetSelectedPlayerValue();

        }


    }

    public void SetLevelValue(int lValue)
    {
        AdmobAdsManager.Instance.ShowInt(LoadGamePlay,false);
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        PrefsManager.SetCurrentLevel(lValue);
       
       // LoadGamePlay();
    }

    public void LoadGamePlay()
    {
        LOADING.SetActive(true);
        //		LoadLevel (2);
        LOADING.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
        Debug.Log("Called it");

        // SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().buildIndex+1);
    }

    public void UnlockSelectedDog()
    {
        UnlockDog(Prices[ActiveDog]);

    }


    public void UnlockDog(int dogVal)
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (PrefsManager.GetCoinsValue() >= dogVal)
        {
            PrefsManager.SetPlayerState(ActiveDog, 1);
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - dogVal);
            Success_purchase();
            PrefsManager.SetLastJeepUnlock(ActiveDog);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);

        }
        else
        {
            //notCash.SetActive(true);
            //	notCash.transform.parent.GetComponent<PromoAdsRemove>().SetCountToMax();
            //Invoke ("OffNoCash",3f);

        }

    }


    public void OffNoCash()
    {
        notCash.SetActive(false);

    }
    public void Offsuccess()
    {
        successPannel.SetActive(false);
    }

    public void Success_purchase()
    {
        isReadyForPurchase = true;
        successPannel.SetActive(true);
        lockSprite.SetActive(false);
        unlockPlayerButton.SetActive(false);
        Play.SetActive(true);
        
        Invoke("Offsuccess", 3f);
       //  successPannel.GetComponentInChildren<Text>().text = "You have successfully Unlocked PowerFul Car.";
        
        
        
        coinText.text = "" + PrefsManager.GetCoinsValue();
        coinText2.text = "" + PrefsManager.GetCoinsValue();
        PriceText.transform.parent.gameObject.SetActive(false);
    }


   



}
