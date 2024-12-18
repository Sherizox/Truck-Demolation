using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaysControl : MonoBehaviour
{

    public enum WayMode { Side = 1, Center = 2 }

    public int ways = 1;
    public bool TCActive;
	public bool isAutoMove; 
    public int TrafficNumber = 1;
    public int[] trafficNumbers = new int[3];

    public float TrafficTime;
    public int TrafficWays = 2;

    public Transform way1, way2, way3, way4;
    public int way1Mode, way2Mode, way3Mode, way4Mode = 1;

    public string[] trafficMode = new string[] { "Way1", "Way2", "Way3", "Way4" };

    public float TrafficTimer;
    public float TrafficWaitTimer = 0f;

    public float stopDistance = 4.0f;

    private Node[] waysConnected = new Node[4];

    void Awake()
    {
        if (way1 && ways > 0) way1.GetComponent<Node>().mode = (way1Mode == 1) ? "TwoWay" : "OnWay";
        if (way2 && ways > 1) way2.GetComponent<Node>().mode = (way2Mode == 1) ? "TwoWay" : "OnWay";
        if (way3 && ways > 2) way3.GetComponent<Node>().mode = (way3Mode == 1) ? "TwoWay" : "OnWay";
        if (way4 && ways > 3) way4.GetComponent<Node>().mode = (way4Mode == 1) ? "TwoWay" : "OnWay";
    }

    void Start()
    {
        if (way1) waysConnected[0] = way1.GetComponent<Node>();
        if (way2) waysConnected[1] = way2.GetComponent<Node>();
        if (way3) waysConnected[2] = way3.GetComponent<Node>();
        if (way4) waysConnected[3] = way4.GetComponent<Node>();

        TrafficWaitTimer =0f;

        if (!TCActive) return;
        TrafficTimer = TrafficTime;
        TrafficController();
    }


    void Update()
    {
		if (isAutoMove == false) {

			if (!TCActive || Vector3.Distance (AIContoller.Instance.player.position, transform.position) > 300)
				return;
		}
        TrafficTimer = Mathf.MoveTowards(TrafficTimer, 0, Time.deltaTime);

        if (TrafficTimer == 0)
        {

            TrafficWaitTimer = Mathf.MoveTowards(TrafficWaitTimer, 0, Time.deltaTime);

            if (TrafficWaitTimer == 0)
            {
                if (TrafficNumber >= TrafficWays) TrafficNumber = 0;
                TrafficNumber++;

                TrafficTimer = TrafficTime;
                TrafficWaitTimer = 0f;
            }
        }

        TrafficController();
    }

    void TrafficController()
    {

        for (int n = 0; n < trafficNumbers.Length; n++)
        {
            for (int i = 0; i < trafficMode.Length; i++)
            {
                int layer = 1 << i;
                if ((trafficNumbers[n] & layer) != 0)
                {

                    if (waysConnected[i] != null)
                    {

                        if (TrafficWaitTimer == 0)
                            waysConnected[i].trafficMode = (TrafficNumber - 1 == n) ? TrafficMode.Go : TrafficMode.Stop;
                        else
                            waysConnected[i].trafficMode = TrafficMode.Wait;

                        waysConnected[i].trafficNode = true;

                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 2.0f);
    }


    public bool DrawAutoNote = false;
    public LayerMask layerMask;
    public List<Transform> AllRangeNode = new List<Transform>();
    //public void AutoFindPaths(bool DrawAutoNote) {


    //    if (DrawAutoNote)
    //    {

    //        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stopDistance * 2f, layerMask);
    //        AllRangeNode.Clear();
    //        layerMask = LayerMask.GetMask("Nodes");
    //        Gizmos.DrawWireSphere(base.transform.position, stopDistance * 2f);
    //        foreach (var hitCollider in hitColliders)
    //        {
    //            AllRangeNode.Add(hitCollider.transform);
    //        }
    //        if (hitColliders.Length > 0)
    //            way1 = hitColliders[0].transform;
    //        if (hitColliders.Length > 1)
    //            way2 = hitColliders[1].transform;
    //        if (hitColliders.Length > 2)
    //            way3 = hitColliders[2].transform;
    //        if (hitColliders.Length > 3)
    //            way4 = hitColliders[3].transform;


    //        if (hitColliders.Length > 0 && hitColliders.Length < 5)
    //            ways = hitColliders.Length;

    //    }

    //}


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
        Gizmos.color = Color.blue;

        if (way1 && ways > 0) { Gizmos.DrawCube(way1.position, Vector3.one * 2); Gizmos.DrawIcon(way1.TransformPoint(Vector3.up), (way1Mode == 0) ? "OneWay" : "TwoWay", false); }
        if (way2 && ways > 1) { Gizmos.DrawCube(way2.position, Vector3.one * 2); Gizmos.DrawIcon(way2.TransformPoint(Vector3.up), (way2Mode == 0) ? "OneWay" : "TwoWay", false); }
        if (way3 && ways > 2) { Gizmos.DrawCube(way3.position, Vector3.one * 2); Gizmos.DrawIcon(way3.TransformPoint(Vector3.up), (way3Mode == 0) ? "OneWay" : "TwoWay", false); }
        if (way4 && ways > 3) { Gizmos.DrawCube(way4.position, Vector3.one * 2); Gizmos.DrawIcon(way4.TransformPoint(Vector3.up), (way4Mode == 0) ? "OneWay" : "TwoWay", false); }


        if (DrawAutoNote)
        {

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, stopDistance * 2f, layerMask);
            AllRangeNode.Clear();
            layerMask = LayerMask.GetMask("Nodes");
            Gizmos.DrawWireSphere(base.transform.position, stopDistance * 2f);
            foreach (var hitCollider in hitColliders)
            {
                AllRangeNode.Add(hitCollider.transform);
            }
            if (hitColliders.Length > 0)
                way1 = hitColliders[0].transform;
            if (hitColliders.Length > 1)
                way2 = hitColliders[1].transform;
            if (hitColliders.Length > 2)
                way3 = hitColliders[2].transform;
            if (hitColliders.Length > 3)
                way4 = hitColliders[3].transform;


            if (hitColliders.Length > 0 && hitColliders.Length < 5)
                ways = hitColliders.Length;

            DrawAutoNote = false;
        }


    }

}
