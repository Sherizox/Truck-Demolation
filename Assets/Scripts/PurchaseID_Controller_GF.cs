using UnityEngine;
using System.Collections;
using System;

public enum  ItemType {
	Gold,Cash,UnlockAll,UnlockAllMission,RemoveAds,Garnade,HealthKit,Skins, coins15Kcars, coins30KCars, coins50kCarsRemoveAd, unlockAll40Percent
}
[System.Serializable]
public class PurchaseID_Controller_GP
{
	//public UnityEngine.Purchasing.ProductType purchaseableType= UnityEngine.Purchasing.ProductType.Consumable;
	public string purchaseID="";
	public ItemType itemType;


}
