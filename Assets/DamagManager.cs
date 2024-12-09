using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentHp, TotalHp;
    public Image healthBar;
    public GameObject InstialteCar;

    public void OnEnable() {

        currentHp = TotalHp;
        ShowProgressBar(currentHp);
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
        ExplodeNow();
        LevelManager.instace.EnemyKilled();
      //  Invoke("ExplodeNow",1f);

    }

    public void ExplodeNow() {

        if (InstialteCar)
        {

            Instantiate(InstialteCar, transform.position, transform.rotation);
            Destroy(this.gameObject,0.2f);

        }
        LevelManager.instace.TaskCompleted();
        //  levelompletepanel.SetActive(true);
    }
    public void ShowProgressBar(float currenthp) {

        healthBar.fillAmount = (currentHp / TotalHp);
       
 
    }

}
