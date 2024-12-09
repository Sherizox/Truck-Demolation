using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerManager : MonoBehaviour
{
    private string failtag = "Fail";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(failtag))
        {
            UiManagerObject.instance.ShowFail();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
