using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoAdsRemove : MonoBehaviour {
    private int tempCountForPromotion = 0;
    private int showCount =3;
    public GameObject promotionalImage;
    public bool isRemoveAds;
    public bool isUnlockAllVehicles;
    public bool isUnlockAllLevels;
    void OnEnable () 
    {

        if (PlayerPrefs.GetInt("removeads", 0) == 0  && isRemoveAds)
        {
           
            if (tempCountForPromotion == 0)
            {
                tempCountForPromotion = showCount;
                promotionalImage.SetActive(true);
            }
            tempCountForPromotion--;
        }
        else if (PlayerPrefs.GetFloat("UnlockAllVehicles", 0)==0 && isUnlockAllVehicles)
        {
            if (tempCountForPromotion == 0)
            {
                tempCountForPromotion = showCount;
                promotionalImage.SetActive(true);
            }
            tempCountForPromotion--;
        }
        else if (PlayerPrefs.GetFloat("UnlockAllLevels", 0) == 0 && isUnlockAllLevels)
        {
            if (tempCountForPromotion == 0)
            {
                tempCountForPromotion = showCount;
                promotionalImage.SetActive(true);
            }
            tempCountForPromotion--;
        }


        Data.OnUnlockAllMission += ClosePromo;
        Data.OnUnlockAllPlayers += ClosePromo;

        Data.OnRemoveAds += ClosePromo;
    }
    public void ClosePromo() {
        promotionalImage.SetActive(false);
    }



    public void OnDisable()
    {


        Data.OnUnlockAllMission -= ClosePromo;
        Data.OnUnlockAllPlayers -= ClosePromo;

        Data.OnRemoveAds -= ClosePromo;
    }

}
