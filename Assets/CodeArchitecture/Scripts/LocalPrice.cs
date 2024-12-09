using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalPrice : MonoBehaviour {
    public Text price;
    public bool isRemoveAd;
    public bool isUnlockAllVehicles;
    public bool isUnlockAllMission;
    public bool is15KCoinsCars;

    public bool is30KCoinsCars;

    public bool isEconomyPakage;

    public bool is40PercentOffcars;


    // public bool isUnlockAllLevels;
    // Use this for initialization
    void Start()
    {
        if (isRemoveAd == true)
        {
           // price.text = MyIAPManager.removeadspricetext;
        }
        else if (isUnlockAllVehicles == true)
        {
           // price.text = MyIAPManager.unlockallvehiclespricetext;
        }
        else if (isUnlockAllMission == true)
        {
          //  price.text = MyIAPManager.unlockalllevelpricetext;
        }
        else if (is15KCoinsCars == true)
        {
          //  price.text = MyIAPManager.coin15KCar;
        }
        else if (is30KCoinsCars == true)
        {
           // price.text = MyIAPManager.coin30Kcar;
        }
        else if (isEconomyPakage == true)
        {
           // price.text = MyIAPManager.economyPakage;
        }
        else if (is40PercentOffcars == true)
        {
           // price.text = MyIAPManager.unlockAllCars40Percent;
        }
       
    }

}
