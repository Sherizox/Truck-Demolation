using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class VehicleProperties : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject effect, confettiEffect, CheckPointEffect;
    public Rigidbody Rb;


    void Start()
    {
        Rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool fail = false;
    bool isSingleCall = false;
    string FinishTag = "Finish";
    string FailTag = "Fail";


    public void OffCoinsEffect()
    {
        effect.SetActive(false);
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            effect.SetActive(true);
            Invoke("OffCoinsEffect", 1f);
            if (PrefsManager.GetGameMode() == "free")
            {
                PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 10);
                Destroy(other.gameObject);
                LevelManager.instace.coinBar.GetComponentInChildren<Text>().text = "" + PrefsManager.GetCoinsValue();
                // StartCoroutine(ConfettiEffect());
            }
            else {
                LevelManager.instace.LevelCoin += 10;
                Destroy(other.gameObject);
            }
        }


        if (other.gameObject.CompareTag(FinishTag) && !isSingleCall)
        {

            Rb.isKinematic = true;
            if (Rb.velocity.magnitude > 0)
            {
                Rb.velocity = Vector3.zero;
                Rb.angularVelocity = Vector3.zero;
            }
            UiManagerObject.instance.HideGamePlay();

            UiManagerObject.instance.FadeImage.SetActive(true);
            //RCC_Camera.Instance.TPSDistance += 5f;
            //  RCC_Camera.Instance.TPSHeight += 3f;
            isSingleCall = true;
            //  RCC_Camera.Instance.orbitX = 160f;
            await System.Threading.Tasks.Task.Delay(500);
            UiManagerObject.instance.CutScene.SetActive(true);

            await System.Threading.Tasks.Task.Delay(500);
            UiManagerObject.instance.FadeImage.SetActive(false);

            confettiEffect.SetActive(true);



            await System.Threading.Tasks.Task.Delay(3000);
            UiManagerObject.instance.CutScene.SetActive(false);
            UiManagerObject.instance.ShowComplete();
            other.gameObject.SetActive(false);
            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(other);
            return;

        }


        // IEnumerator ConfettiEffect()
        // {
        //      UiManagerObject.instance.CutScene.SetActive(true);
        // }

        //Debug.Log(other.gameObject.tag);
        if (PrefsManager.GetGameMode() != "Free" && !isSingleCall)
        {
            if (other.gameObject.CompareTag(FailTag) && !fail)
            {
                Debug.Log("Failed With" + other.gameObject.name + " " + other.gameObject.tag);
                isSingleCall = true;
                fail = true;
                await System.Threading.Tasks.Task.Delay(500);
                UiManagerObject.instance.ShowFail();
                return;
                //  Destroy(other);
            }
        }
    }



    public void PlayEffect()
    {
        CheckPointEffect.SetActive(true);
        Invoke("OffCheckPointEffect", 2f);
    }

    public void OffCheckPointEffect()
    {
        CheckPointEffect.SetActive(false);
    }

    private async void OnCollisionEnter(Collision other)
    {
        if (PrefsManager.GetGameMode() != "Free" && !isSingleCall)
        {
            if (other.gameObject.CompareTag(FailTag) && !fail)
            {
                Debug.Log("Failed With" + other.gameObject.name + " " + other.gameObject.tag);
                isSingleCall = true;
                fail = true;
                await System.Threading.Tasks.Task.Delay(500);
                UiManagerObject.instance.ShowFail();
                return;
                //  Destroy(other);
            }
        }
    }
}

