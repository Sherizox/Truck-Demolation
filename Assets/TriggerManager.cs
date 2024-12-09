using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    //public LevelProperties CurrentLevelProperties;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    string AiCarTag = "AiCar";
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == AiCarTag)
        {
            // Debug.Log("Called");
            if (other.GetComponentInParent<DamagManager>())
            {
                other.GetComponentInParent<DamagManager>().Damage(20f);

            }
        }

        //if (other.gameObject.tag=="ReachedPoint")
        //{
        //    gameObject.SetActive(false);
        //    //Destroy(gameObject);
        //    Destroy(other.gameObject);
        //    GetComponent<Rigidbody>().isKinematic = true;
        //    GetComponent<RCC_CarControllerV3>().enabled = false;
        //    LevelManager.instace.FpsMan.transform.position = transform.position;
        //    LevelManager.instace.FpsMan.transform.rotation = transform.rotation;
        //    LevelManager.instace.FpsMan.SetActive(true);
        //    LevelManager.instace.rCC_Camera.gameObject.SetActive(false);
        //    UiManagetObjective.instance.controls.SetActive(false);
        //    UiManagetObjective.instance.FpsControls.SetActive(true);
        //    UiManagetObjective.instance.EnemyCount.SetActive(true);
        //}
    }



    public void OnTriggerStay(Collider collider) {

        if (collider.gameObject.tag == AiCarTag)
        {
           // Debug.Log("Called");
            if (collider.GetComponentInParent<DamagManager>()) {
                collider.GetComponentInParent<DamagManager>().Damage(Time.timeScale/5f);

            }
        }

    }
    public void OnCollisionStay(Collision collision) {

        if (collision.gameObject.tag == AiCarTag)
        {
            Debug.Log("Called 1");
            if (collision.gameObject.GetComponentInParent<DamagManager>())
            {
                collision.gameObject.GetComponentInParent<DamagManager>().Damage(Time.timeScale/5f );

            }
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == AiCarTag)
        {
            Debug.Log("Called 1");
            if (collision.gameObject.GetComponentInParent<DamagManager>())
            {
                if(GetComponent<DamagManagerPlayer>().isShield)
                    collision.gameObject.GetComponentInParent<DamagManager>().Damage(collision.relativeVelocity.magnitude*2.5f);
                else
                    collision.gameObject.GetComponentInParent<DamagManager>().Damage(collision.relativeVelocity.magnitude*1.3f);

            }
        }
    }
}
