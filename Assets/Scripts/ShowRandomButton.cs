using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRandomButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
        StartCoroutine(OffChild());
    }

    public IEnumerator OffChild() {

        yield return new WaitForSeconds(4.5f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
        StartCoroutine(OffChild());
    }
}
