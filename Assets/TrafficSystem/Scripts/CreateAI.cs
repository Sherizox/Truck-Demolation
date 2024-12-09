using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateAI : MonoBehaviour
{
    public LayerMask nodeMask = -1;
    public float InstantiateTime = 2.0f;
    private float vehicleTimer;
    public bool createVehicles = true;
    [Space]
    public float vehicleInstantiateRaduis = 70f;
    public float vehInstMinRadius;
    public float OverlapCheckRadius;
    [Space]
    public List<GameObject> vehiclesList = new List<GameObject>();

    [HideInInspector] public Collider[] selectedCollider;
    [HideInInspector] public GameObject AiVehicleCreated;
    [HideInInspector] public GameObject AIVehicle;
    int randomWay;
	Quaternion tempRotate;
	Vector3 relitivePos;

    void Start()
    {
        
       if (SystemInfo.systemMemorySize > 2500)
        {
            AIContoller.Instance.maxVehicles= 8;
        }
        else if (SystemInfo.systemMemorySize > 1200)
        {
            AIContoller.Instance.maxVehicles= 4;
        }
        else
        {
            AIContoller.Instance.maxVehicles= 1;
        }
        
        
        for (int i = 0; i < AIContoller.Instance.maxVehicles; i++)
        {
            GameObject tempObject =  Instantiate(AIContoller.Instance.vehiclesPrefabs[i]) as GameObject;
            vehiclesList.Add(tempObject);
            vehiclesList[i].SetActive(false);
            vehiclesList[i].name = "AIVehicle";
        }

        StartCoroutine(CreateAiNow());
    }
    
    public void OffVehicles()
    {
        for (int i = 0; i < vehiclesList.Count; i++)
        {
            vehiclesList[i].SetActive(false);
        }
    }
    
    public GameObject GetpooledVehicle()
    {
        for (int i = 0; i < vehiclesList.Count; i++)
        {
            if (!vehiclesList[i].activeInHierarchy)
            {
                return vehiclesList[i];
            }
        }
        return null;
    }

    public void InstantiateVehicle(Node CurrentNode)
    {
        Collider[] vehicles = Physics.OverlapSphere(CurrentNode.transform.position, OverlapCheckRadius);

        bool CanCreateVehicle = true;

        foreach (Collider vehicle in vehicles)
        {
            if (vehicle.CompareTag("Vehicle"))
                CanCreateVehicle = false;
        }
        
        AIVehicle = vehiclesList[Random.Range(0, vehiclesList.Count)];

        if (AIVehicle)
        {
            if (CanCreateVehicle && AIContoller.Instance.currentVehicles < AIContoller.Instance.maxVehicles)
            {
                RaycastHit hit;
                if (Physics.Raycast(CurrentNode.transform.position, -Vector3.up, out hit))
                {
                    AIContoller.Instance.currentVehicles++;

                    AiVehicleCreated = GetpooledVehicle();
                    if (AiVehicleCreated != null)
                    {
                        AiVehicleCreated.transform.position = hit.point + (Vector3.up / 2.0f);
                        AiVehicleCreated.transform.rotation = Quaternion.identity;
                        AiVehicleCreated.SetActive(true);
                        AiVehicleCreated.name = "AIVehicle";
                        if (AiVehicleCreated.GetComponent<AIVehicle>())
                        {
                            AIVehicle AIVehicleScript = AiVehicleCreated.GetComponent<AIVehicle>();
                            if (AIVehicleScript.vehicleStatus != VehicleStatus.ChasePlayer)
                            {
                                if (CurrentNode.mode == "TwoWay")
                                {
                                    randomWay = Random.Range(1, 3);

                                    if (randomWay == 1)
                                    {
                                        AIVehicleScript.wayMove = WayMove.Left;
                                        AIVehicleScript.myStatue = "NextPoint";
                                        relitivePos = CurrentNode.previousNode.position - AiVehicleCreated.transform.position;
                                        relitivePos.y = 0;
                                        tempRotate = Quaternion.LookRotation(relitivePos);
                                        AiVehicleCreated.transform.rotation = tempRotate;

                                        AIVehicleScript.currentNode = CurrentNode.transform;
                                        AIVehicleScript.nextNode = CurrentNode.nextNode;
                                        AiVehicleCreated.transform.position = AiVehicleCreated.transform.TransformPoint(CurrentNode.widthDistance, 0, 0);
                                    }
                                    else
                                    {
                                        AIVehicleScript.wayMove = WayMove.Right;
                                        AIVehicleScript.myStatue = "PreviousPoint";
                                        relitivePos = CurrentNode.nextNode.position - AiVehicleCreated.transform.position;
                                        relitivePos.y = 0;
                                        tempRotate = Quaternion.LookRotation(relitivePos);
                                        AiVehicleCreated.transform.rotation = tempRotate;
                                        AIVehicleScript.currentNode = CurrentNode.transform;
                                        AIVehicleScript.nextNode = CurrentNode.previousNode;

                                        AiVehicleCreated.transform.position = AiVehicleCreated.transform.TransformPoint(CurrentNode.widthDistance, 0, 0);
                                    }
                                }
                                else
                                {
                                    AIVehicleScript.wayMove = WayMove.Right;
                                    AIVehicleScript.myStatue = "PreviousPoint";
                                    relitivePos = CurrentNode.nextNode.position - AiVehicleCreated.transform.position;
                                    relitivePos.y = 0;
                                    tempRotate = Quaternion.LookRotation(relitivePos);
                                    AiVehicleCreated.transform.rotation = tempRotate;
                                    AIVehicleScript.currentNode = CurrentNode.transform;
                                    AIVehicleScript.nextNode = CurrentNode.nextNode;

                                    AiVehicleCreated.transform.position = AiVehicleCreated.transform.TransformPoint(Random.Range(-CurrentNode.widthDistance, CurrentNode.widthDistance) / 2.0f, 0, 0);
                                }
                            }
                            else
                            {
                                relitivePos = CurrentNode.nextNode.position - AiVehicleCreated.transform.position;
                                relitivePos.y = 0;
                                tempRotate = Quaternion.LookRotation(relitivePos);
                                AiVehicleCreated.transform.rotation = tempRotate;
                                AIVehicleScript.currentNode = CurrentNode.transform;
                                AIVehicleScript.nextNode = this.transform;

                                AiVehicleCreated.transform.position = AiVehicleCreated.transform.TransformPoint(Random.Range(-CurrentNode.widthDistance, CurrentNode.widthDistance) / 2.0f, 0, 0);
                            }
                        }
                    }
                    else
                    {
                    }
                }
            }
        }
    }
    
    public IEnumerator CreateAiNow()
    {
        while (true)
        { 
            if (createVehicles)
            {
                if (vehicleTimer == 0)
                {
                    Collider[] nodes = Physics.OverlapSphere(transform.position, vehicleInstantiateRaduis, nodeMask);
                    selectedCollider = nodes;

                    foreach (Collider node in nodes)
                    {
                        float Dist = Vector3.Distance(transform.position, node.transform.position);

                        if (Dist > vehInstMinRadius)
                        {
                            if (node.GetComponent<Node>() && AIContoller.Instance.vehiclesPrefabs.Length > 0)
                            {
                                {
                                    if (node != null)
                                    InstantiateVehicle(node.GetComponent<Node>());
                                    vehicleTimer = InstantiateTime;
                                }
                            }
                        }
                    }
                }
                else
                {
                    vehicleTimer = Mathf.MoveTowards(vehicleTimer, 0.0f, Time.deltaTime);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}