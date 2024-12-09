using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class ParkVehicle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject ObjectToHide;
    [SerializeField]
    GameObject VehiclePosition;
    //[SerializeField]
  //  Animator DoorAnimator;
    [SerializeField]
   public bool isDrop;
    GameObject busPessangerParent,busPessanger;
    Rigidbody rb;
    public Animator AllPlayerMoves;
    VehicleProperties vehicleProperties;
    public Transform DropPessangerPosition;
    
    
  
    
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {

    }

    string playerTag = "Player";
    public GameObject Player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {

            UiManagerObject.instance.FadeImage.SetActive(true);
            other.transform.root.position = VehiclePosition.transform.position;
            other.transform.root.rotation = VehiclePosition.transform.rotation;
            Player = other.gameObject;
            rb = other.GetComponentInParent<Rigidbody>();
            rb.isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
            ObjectToHide.SetActive(false);
            vehicleProperties = other.GetComponentInParent<VehicleProperties>();
            // DoorAnimator = other.GetComponentInParent<VehicleProperties>().AnimatedDoor;
           // busPessangerParent = other.GetComponentInParent<VehicleProperties>().PessangerParent;
           // busPessanger = other.GetComponentInParent<VehicleProperties>().Pessanger;
            StartCoroutine(DoorAnimatorHandler());

        }


    }


    public IEnumerator DoorAnimatorHandler()
    {
        UiManagerObject.instance.HideGamePlay();
        UiManagerObject.instance.CutScene.SetActive(true);
       // RCC_Camera.Instance.TPSDistance += 5f;
    //    RCC_Camera.Instance.TPSHeight += 3f;
    

        if (isDrop)
        {
            
          
        //  UiManagetObjective
          // busPessanger.SetActive(false);
           // foreach (Animator move in AllPlayerMoves)
           {
               yield return new WaitForSeconds(0.5f);
          //     UiManagerObject.instance.FadeImage2.SetActive(true);
              // AllPlayerMoves =  Player.GetComponentInParent<VehicleProperties>().Pessanger.GetComponent<Animator>() ;
               AllPlayerMoves.transform.SetParent(null);
               AllPlayerMoves.transform.position = DropPessangerPosition.position;
               AllPlayerMoves.transform.rotation = DropPessangerPosition.rotation;
                AllPlayerMoves.gameObject.SetActive(true);
                AllPlayerMoves.SetFloat("Speed",1);
                AllPlayerMoves.SetBool("Sit",false);
                yield return new WaitForSeconds(2f);
              
            }
            
           
        }
        else
        {
            yield return new WaitForSeconds(2f);
            AllPlayerMoves.SetFloat("Speed",1);
            yield return new WaitForSeconds(1.5f);
            UiManagerObject.instance.FadeImage2.SetActive(true); 
            
        }



       
         
      
     
    
      
      //  yield return new WaitForSeconds(5f);
      //  DoorAnimator.SetBool("DoorOpen", false);
    
      
      
       
         
        if (!isDrop)
        {
           
           // foreach (Animator move in AllPlayerMoves)
            {
               // move.gameObject.SetActive(false);
               AllPlayerMoves.SetBool("Sit",true);
               AllPlayerMoves.transform.position = busPessangerParent.transform.GetChild(0).position;
               AllPlayerMoves.transform.rotation = busPessangerParent.transform.GetChild(0).rotation;
               busPessanger = AllPlayerMoves.gameObject;
               AllPlayerMoves.transform.SetParent(busPessangerParent.transform);
              // Player.GetComponentInParent<VehicleProperties>().Pessanger = AllPlayerMoves.gameObject;
            }
           // busPessanger.SetActive(true);
           // vehicleProperties.isPessangerinBus = true;
            
        }
        else {

            
            //vehicleProperties.isPessangerinBus = false;
        }

        if (rb.velocity.magnitude> 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity=Vector3.zero;
        }
        
        rb.isKinematic = false;
        
        yield return new WaitForSeconds(1.5f);
       // RCC_Camera.Instance.orbitX = 0;
      // RCC_Camera.Instance.TPSDistance -= 5f;
      // RCC_Camera.Instance.TPSHeight -= 3f;
      // yield return new WaitForSeconds(1f);
       UiManagerObject.instance.CutScene.SetActive(false);
       UiManagerObject.instance.FadeImage.SetActive(false);
       UiManagerObject.instance.FadeImage2.SetActive(false);
       yield return new WaitForSeconds(1f);
        LevelManager.instace.TaskCompleted();
        UiManagerObject.instance.ShowGamePlay();

    }

}
