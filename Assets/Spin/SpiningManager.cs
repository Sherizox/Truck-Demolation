using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiningManager : MonoBehaviour {

	int randVal;
	private float timeInterval;
	private bool isCoroutine;
	private int finalAngle;
	public GameObject RewardPannel;
	public Text winText;
	public Image winimage;

	public int section;
	float totalAngle;
	public int[] PrizeName;
	public Sprite[] spriteName;

	


	// Use this for initialization
	private void Start () {
		isCoroutine = true;
		totalAngle = 360 / section;
	}

	// Update is called once per frame
	private void Update () {

		// if (Input.GetMouseButton (0) && isCoroutine) {
		// 	StartCoroutine (Spin ());
		// }
		
	}


	public void SpinNow()
	{
		
		StartCoroutine (Spin ());
	}

	
	private IEnumerator Spin(){
	 // this whole loop is updating the time and the rotation of wheel
		isCoroutine = false;
		randVal = Random.Range (200, 300);

		timeInterval = 0.0001f*Time.deltaTime*1;

		for (int i = 0; i < randVal; i++) {

			transform.Rotate (0, 0, (totalAngle/2)); //Start Rotate 


			//To slow Down Wheel
			if (i > Mathf.RoundToInt (randVal * 0.2f))
				timeInterval = 0.5f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.3f))
				timeInterval = .85f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.5f))
				timeInterval = 1f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.6f))
				timeInterval = 1.68f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.7f))
				timeInterval = 1.78f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.8f))
				timeInterval = 1.88f*Time.deltaTime;

			yield return new WaitForSeconds (timeInterval);

		}

		if (Mathf.RoundToInt (transform.eulerAngles.z) % totalAngle != 0) //when the indicator stop between 2 numbers,it will add aditional step 
			transform.Rotate (0, 0, totalAngle/2);
		
		finalAngle = Mathf.RoundToInt (transform.eulerAngles.z);//round off euler angle of wheel value

		print (finalAngle);

		//Prize check
		for (int i = 0; i < section; i++) {

			if (finalAngle == i * totalAngle)
			{
				
				RewardPannel.SetActive(true);
				// this line is updating panel time 
				Invoke("OffNow",4f);

				winimage.sprite = spriteName[i];
				//this line is showing the the exact value for execution
				winText.text = PrizeName [i]+" Coins";
				
				//this line is updating the coins
				PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue()+PrizeName [i]);
					

			}

			}
			
			
		

	
		isCoroutine = true;
	}

	public void OffNow()
	{
		RewardPannel.SetActive(false);
	}
}
