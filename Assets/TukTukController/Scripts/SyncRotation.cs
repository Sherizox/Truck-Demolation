using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    public Transform tyremesh;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = tyremesh.rotation; 
    }
}
