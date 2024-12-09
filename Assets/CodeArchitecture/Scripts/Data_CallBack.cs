using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_CallBack : MonoBehaviour
{
    public static int AdType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void RewardedAdWatched() {

        if (AdType == 0) {

            Debug.Log("Watched Ad 0");
        }

    }
}
