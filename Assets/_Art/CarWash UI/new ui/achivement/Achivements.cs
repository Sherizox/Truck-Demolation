using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achivements : MonoBehaviour {

	public int Amounts;
	public Text TotalAmounts;
	public GameObject[] Claim;
	public GameObject[] Tick;

	bool claim1 = false;
	bool claim2 = false;
	bool claim3 = false;
	bool claim4 = false;
	bool claim5 = false;
	bool claim6 = false;
	bool claim7 = false;
	public GameObject congurlationpanel;

	// Use this for initialization
	void OnEnable () {
		Amounts =PrefsManager.GetCoinsValue();
		TotalAmounts.text = Amounts.ToString();
	   
		if(PrefsManager.GetLevelLocking() >= 2 && !claim1)
		{
			Claim[0].SetActive(true);
			claim1=true;
		}
		if(PrefsManager.GetLevelLocking() >= 3 && !claim2)
		{
			Claim[1].SetActive(true);
			claim2 = true;
		}
		
		if (PrefsManager.GetPlayerState(1)== 1 && !claim3)
		{
			Claim[2].SetActive(true);
			claim3 = true;
		}
		if (PrefsManager.GetLevelLocking() >= 10 && !claim4)
		{
			Claim[3].SetActive(true);
			claim4 = true;
		}
	  
		if (PrefsManager.GetPlayerState(3)  == 1 && !claim5)
		{
			Claim[4].SetActive(true);
			claim5 = true;
		}
		if (PrefsManager.GetLevelLocking() >= 15 && !claim6)
		{
			Claim[5].SetActive(true);
			claim6 = true;
		}
		if (PrefsManager.GetLevelLocking() >= 20 && !claim7)
		{
			Claim[6].SetActive(true);
			claim7 = true;
		}

		
		if (PlayerPrefs.GetInt("claim1", 0) == 1)
		{
			Claim[0].SetActive(false);
			Tick[0].SetActive(true);
		}
		if (PlayerPrefs.GetInt("claim2", 0) == 1)
		{
			Claim[1].SetActive(false);
			Tick[1].SetActive(true);
		}
		if (PlayerPrefs.GetInt("claim3", 0) == 1)
		{
			Claim[2].SetActive(false);
			Tick[2].SetActive(true);
		}
		if (PlayerPrefs.GetInt("claim4", 0) == 1)
		{
			Claim[3].SetActive(false);
			Tick[3].SetActive(true);
		}
		if (PlayerPrefs.GetInt("claim5", 0) == 1)
		{
			Claim[4].SetActive(false);
			Tick[4].SetActive(true);
		}
		if (PlayerPrefs.GetInt("claim6", 0) == 1)
		{
			Claim[5].SetActive(false);
			Tick[5].SetActive(true);
		}
		if (PlayerPrefs.GetInt("claim7", 0) == 1)
		{
			Claim[6].SetActive(false);
			Tick[6].SetActive(true);
		}
	}
	
	public void L1_Claim()
	{
		CongratulationClaim(200);
		PlayerPrefs.SetInt("claim1", 1);
	  
		Claim[0].SetActive(false);
		Tick[0].SetActive(true);

	}
	public void L2_Claim()
	{
		CongratulationClaim(300);

		PlayerPrefs.SetInt("claim2", 1);

		Claim[1].SetActive(false);
		Tick[1].SetActive(true);


	}
	public void L3_Claim()
	{
		CongratulationClaim(500);
		PlayerPrefs.SetInt("claim3", 1);

		Claim[2].SetActive(false);
		Tick[2].SetActive(true);


	}
	public void L4_Claim()
	{
		CongratulationClaim(1000);
		PlayerPrefs.SetInt("claim4", 1);

		Claim[3].SetActive(false);
		Tick[3].SetActive(true);
	

	}
	public void L5_Claim()
	{
		CongratulationClaim(500);
		PlayerPrefs.SetInt("claim5", 1);

		Claim[4].SetActive(false);
		Tick[4].SetActive(true);
	

	}
	public void L6_Claim()
	{
		CongratulationClaim(2000);
		PlayerPrefs.SetInt("claim6", 1);
	
		Claim[5].SetActive(false);
		Tick[5].SetActive(true);
	

	}
	public void L7_Claim()
	{
		CongratulationClaim(2000);
		PlayerPrefs.SetInt("claim7", 1);
	
		Claim[6].SetActive(false);
		Tick[6].SetActive(true);

	}

	public void CongratulationClaim(int score)
	{
		congurlationpanel.GetComponent<MoneyCounterAuto>().UpdateScore(score);

		Amounts = PrefsManager.GetCoinsValue();
		Amounts = Amounts + score;
		PrefsManager.SetCoinsValue(Amounts);
		TotalAmounts.text = Amounts.ToString();
		SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);

		//for(int i=0;i<CongratulationsTextArray.Length;i++)
		//{
		//    CongratulationsTextArray[i].SetActive(false);
		//}
	}
}
