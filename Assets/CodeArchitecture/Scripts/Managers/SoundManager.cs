﻿using UnityEngine;
using System.Collections;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    int adsCounter = 0;
    public GameObject resource;
    public AudioClip BgSound, menuSound;
    public AudioClip click, one, two, three, go, LevelComplete, levelFail, timer,
        phoneRingClip, typingClip, coinPickClip,sprayClip;

    public bool isDeletePrefs;
    // Use this for initialization
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //PlayAudio (menuSound);
        if (isDeletePrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        //PlayerPrefs.DeleteAll ();
        //PrefsManager.SetCoinsValue (200000);
    }

    public void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();

    }
    public void PlayAudio2(AudioClip clip)
    {
        resource.GetComponent<AudioSource>().clip = clip;
        resource.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Stop();
    }

    public void PlayOneShotSounds(AudioClip clip)
    {
        resource.GetComponent<AudioSource>().clip = clip;
        resource.GetComponent<AudioSource>().Play();
        
    }

    public void PlayOneShotSoundsFail(AudioClip clip)
    {
        resource.GetComponent<AudioSource>().clip = clip;
        resource.GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().Stop();
    }

    public void PlayTimmerSound()
    {
        resource.GetComponent<AudioSource>().clip = timer;
        resource.GetComponent<AudioSource>().Play();
        resource.GetComponent<AudioSource>().volume = 0.5f;
        resource.GetComponent<AudioSource>().loop = true;

        GetComponent<AudioSource>().Stop();
    }

    public void StopOneShotClip() {

    }



    public void OffPlayTimmerSound()
	{
		
		resource.GetComponent<AudioSource>().loop = false;
        resource.GetComponent<AudioSource>().volume = 1f;
        resource.GetComponent<AudioSource>().Stop();
	}
	//	public void ShowAds ()
	//	{
	//		if (adsCounter == 0) {
	//			Debug.Log ("Ads Counter Value = " + adsCounter);
	//			if (Advertisement.IsReady ()) {
	//				Advertisement.Show ();
	//			} else {
	//				AdmobAds.instance.ShowInterstitial ();
	//			}
	//			adsCounter = 1;
	//		} else if (adsCounter == 1) {
	//			Debug.Log ("Ads Counter Value = " + adsCounter);
	//			AdmobAds.instance.ShowInterstitial ();
	//			adsCounter = 2;
	//		} else if (adsCounter == 2) {
	//			Debug.Log ("Ads Counter Value = " + adsCounter);
	//			if (Chartboost.hasInterstitial (CBLocation.Default)) {
	//				Chartboost.showInterstitial (CBLocation.Default);
	//			} else {
	//				Chartboost.cacheInterstitial (CBLocation.Default);
	//			}
	//			adsCounter = 0;
	//		}
	//	}


}
