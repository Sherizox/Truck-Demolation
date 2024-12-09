using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffIfPurchase : MonoBehaviour {
    public bool isRemoveAd = false;
    public bool isUnlockall = false;
    public bool isUnlockAllLevel = false;
    public bool IsEconomyPackage = false;

    // Use this for initialization
    public   void OnEnable () {
        if (isRemoveAd && PlayerPrefs.GetInt("removeads", 0) != 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("off remove ads");
        }
        if (isUnlockall && PlayerPrefs.GetFloat("UnlockAllVehicles", 0) != 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("off Unlock All");
        }
        if (isUnlockAllLevel && PlayerPrefs.GetFloat("UnlockAllLevels", 0) != 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("off Unlock All mission");

        }
        if (IsEconomyPackage && PlayerPrefs.GetFloat("unlockEconomyPackage", 0) != 0)
        {
            this.gameObject.SetActive(false);
            Debug.Log("off Unlock All mission");

        }

        Data.OnUnlockAllMission += ClosePromo;
        Data.OnUnlockAllPlayers += ClosePromo;

        Data.OnRemoveAds += ClosePromo;
    }
	
	// Update is called once per frame
	public void ClosePromo() {
        this.gameObject.SetActive(false);
    }



    public void OnDisable()
    {


        Data.OnUnlockAllMission -= ClosePromo;
        Data.OnUnlockAllPlayers -= ClosePromo;

        Data.OnRemoveAds -= ClosePromo;
    }
}
