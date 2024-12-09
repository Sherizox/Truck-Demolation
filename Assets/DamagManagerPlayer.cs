using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagManagerPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentHp, TotalHp;
    public Image healthBar;
    public GameObject InstialteCar,powerUps, powerpartical;
    public bool isShield = false;
    public void OnEnable() {

        currentHp = TotalHp;
        ShowProgressBar(currentHp);
    }


    public void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 15f)
        {
            Damage(5f);
        }
        else {
            Damage(2f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health")
        {

            other.gameObject.SetActive(false);
            currentHp = TotalHp;
            powerUps.SetActive(true);
            ShowProgressBar(currentHp);
            Invoke("OffPowerUps",2.5f);

        }
       else if(other.gameObject.tag == "power")
        {
            other.gameObject.SetActive(false);
            powerpartical.SetActive(true);
            isShield = true;
            Invoke("OffPowerUps", 10f);
        }
    }
    
    public void OffPowerUps() {

        isShield = false;
        powerUps.SetActive(false);
        powerpartical.SetActive(false);
    }


    public void Damage(float currentDamage)
    {
        if (currentHp > 0 && !isDeath) { 
        currentHp -= currentDamage;
        ShowProgressBar(currentDamage);
        if (currentHp < 0)
        {
            Death();
        }
    }
    }

    bool isDeath = false;
    public void Death() {
        isDeath = true;
        GetComponent<RCC_CarControllerV3>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        
       

        Invoke("ExplodeNow",1f);

    }

    public void ExplodeNow() {

        if (InstialteCar)
        {

            Instantiate(InstialteCar, transform.position, transform.rotation);
            Destroy(this.gameObject,0.2f);

        }
        UiManagerObject.instance.ShowFail();
        //  levelompletepanel.SetActive(true);
    }
    public void ShowProgressBar(float currenthp) {

        healthBar.fillAmount = (currentHp / TotalHp);
       
 
    }

}
