using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait : MonoBehaviour {

    private float T{set; get;}
    Text t;
    public Image ParentNode;

    void Start()
    {
        t = GetComponent<Text>();
    }

	 void Update() {
         if (PlayG.sh == 1 || MainScore.TotalCount == 0 || ParentNode.enabled)
         {
             t.gameObject.SetActive(false);
             t.text = " ";
         }
         if (PlayG.m == 0)
         {
             T += Time.deltaTime;
             if (T >= 0.3)
             {
                 t.text = " ";
                 T = 0;
                 t.gameObject.SetActive(false);
             }

         }

    }
}
