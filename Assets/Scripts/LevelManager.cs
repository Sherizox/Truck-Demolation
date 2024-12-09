using SickscoreGames.HUDNavigationSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels,ParkingLevel;
    public GameObject[] Players,AllParkingPlayer;
    public int[] Reward; 
    public LevelProperties CurrentLevelProperties, FreeModeLevelProperties;
    public ParkingLevelProperties ParkingLevelProperties;
   // public DrawMapPath mapPath;
    // Start is called before the first frame update
   public GameObject SelectedPlayer,FreeMode,FreeModePosition,coinBar,City, enemybar;
    public static LevelManager instace;
    public LineRenderer Line;
    // public AIContoller aIContoller;
    // public RCC_Camera rCC_Camera;
    // public MapCanvasController canvasController;
    public HUDNavigationSystem system;
   public int CurrentLevel, coinsCounter;
    public int killedenemy=0;
    public int LevelCoin = 0;
   
    void Awake()
    {
        instace = this;
       
        if (PrefsManager.GetGameMode() != "free")
        {
            if (PrefsManager.GetLevelMode() == 0)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                CurrentLevelProperties = Levels[CurrentLevel].GetComponent<LevelProperties>();
                //CurrentLevelProperties.gameObject.SetActive(true);
                SelectedPlayer.transform.position = CurrentLevelProperties.PlayerPosition.position;
                SelectedPlayer.transform.rotation = CurrentLevelProperties.PlayerPosition.rotation;
                //mapPath = SelectedPlayer.GetComponent<VehicleProperties>().mapPath;
                CurrentLevelProperties.gameObject.SetActive(true);
                //enemybar.SetActive(false);
                // rCC_Camera.TPSPitchAngle = 18f;

            }
            else if (PrefsManager.GetLevelMode()==1)
            {
                //enemybar.SetActive(false);
                Time.timeScale = 1;
                SelectedPlayer = AllParkingPlayer[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                ParkingLevelProperties = ParkingLevel[CurrentLevel].GetComponent<ParkingLevelProperties>();
                ParkingLevelProperties.gameObject.SetActive(true);
                SelectedPlayer.transform.position = ParkingLevelProperties.Position.position;
                SelectedPlayer.transform.rotation = ParkingLevelProperties.Position.rotation;
                City.SetActive(false);
              //  aIContoller.gameObject.SetActive(false);
              //  rCC_Camera.GetComponentInChildren<CreateAI>().enabled = false;
               // rCC_Camera.GetComponentInChildren<Camera>().farClipPlane = 1000f;
               // rCC_Camera.TPSPitchAngle = 22f;
                UiManagerObject.instance.NosButton.SetActive(false);
            }
           
          //  FreeMode.SetActive(true);
//            coinBar.SetActive(true);
//            coinBar.GetComponentInChildren<Text>().text = "" + PrefsManager.GetCoin();
        //    SelectedPlayer.transform.position = FreeModePosition.transform.position;
          //  SelectedPlayer.transform.rotation = FreeModePosition.transform.rotation;

        }
        else
        {

            Time.timeScale = 1; 
            FreeMode.SetActive(true);
            coinBar.GetComponentInChildren<Text>().text = "" + PrefsManager.GetCoinsValue();
            coinBar.SetActive(true);
            enemybar.SetActive(false);

            CurrentLevelProperties = FreeModeLevelProperties;
            SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
            Debug.Log("CurrentLevelProperties" + CurrentLevelProperties.PlayerPosition.position);
            SelectedPlayer.transform.position = CurrentLevelProperties.PlayerPosition.position;
            SelectedPlayer.transform.rotation = CurrentLevelProperties.PlayerPosition.rotation;
        //    mapPath = SelectedPlayer.GetComponent<VehicleProperties>().mapPath;
        //     mapPath = SelectedPlayer.GetComponent<VehicleProperties>().mapPath;
            CurrentLevelProperties.gameObject.SetActive(true);
            
        }
        SelectedPlayer.SetActive(true);

       // aIContoller.player = SelectedPlayer.transform;
     //  RCC_Camera.player = SelectedPlayer.GetComponent<RCC_CarControllerV3>();
       // //canvasController.playerTransform = SelectedPlayer.transform;
        system.PlayerController = SelectedPlayer.transform;
        

    }

public IEnumerator Start(){
    if (PrefsManager.GetLevelMode()!=1)
    {
        yield return new WaitForSeconds(0.5f);
        UiManagerObject.instance.ShowObjective(CurrentLevelProperties.LevelStatment);
    }
  
}



    public void EnemyKilled() {

        killedenemy++;
        UiManagerObject.instance.EnemyCountShow.text = killedenemy + " / " + CurrentLevelProperties.TotalEnemy;


    }



    public void TaskCompleted()
    {

        CurrentLevelProperties.Nextobjective();

    }

}
