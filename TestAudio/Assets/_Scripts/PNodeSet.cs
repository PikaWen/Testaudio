using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PNodeSet : MonoBehaviour {

    public GameObject[] Go; // node
    public Image[] Go1;     // node


	// Use this for initialization
	void Start () {
        Song1();
	}
	
	// Update is called once per frame
	void Song1 () {
        StartCoroutine(WaitFor(0.6f, 14));
        StartCoroutine(WaitFor(0.6f, 5));
        StartCoroutine(WaitFor(0.4f, 4));
        StartCoroutine(WaitFor(0.6f, 8));
        StartCoroutine(WaitFor(0.6f, 10));
        StartCoroutine(WaitFor(0.5f, 0));
        StartCoroutine(WaitFor(0.4f, 16)); 
        StartCoroutine(WaitFor(0.5f, 7));
        StartCoroutine(WaitFor(0.5f, 9));
        StartCoroutine(WaitFor(0.5f, 17));
        StartCoroutine(WaitFor(0.7f, 2));
        StartCoroutine(WaitFor(0.6f, 15));
        StartCoroutine(WaitFor(0.5f, 3));
        StartCoroutine(WaitFor(0.4f, 14));
        StartCoroutine(WaitFor(0.4f, 6));
        StartCoroutine(WaitFor(0.4f, 8));
        StartCoroutine(WaitFor(0.5f, 1));
        StartCoroutine(WaitFor(0.3f, 3));
        StartCoroutine(WaitFor(0.3f, 12));
        StartCoroutine(WaitFor(0.3f, 11));
        StartCoroutine(WaitFor(0.5f, 0));

        Song2();
	}

    void Song2()
    {

    }




    IEnumerator WaitFor(float waitTime , int i)
    {
        yield return new WaitForSeconds(waitTime);
        Go[i].SetActive(true);
        Go1[i].enabled = true;
    }
   
}
