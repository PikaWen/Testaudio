using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class ScrollV : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    private ScrollRect scroll;
    private float[] pagevArray = new float[] { 1, 0.75f, 0.50f, 0.25f, 0 };
    private float targetVerticalposition = 1;
    public int smoothing = 4;
    private bool isDrag = false;
    public Sprite [] photo;
    public Image Pho;
    public GameObject [] Music;
  
    // Use this for initialization


    void Start()
    {
        scroll = GetComponent<ScrollRect>();
     
        for (int i = 0; i < Music.Length; i++) { Music[i].SetActive(false); }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrag)
        {
            scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetVerticalposition, Time.deltaTime * smoothing);
        }
        if (scroll.verticalNormalizedPosition > 0.9)
        {
            Pho.sprite = photo[0];
            Music[0].SetActive(true);

        }
        else
        {
            Music[0].SetActive(false);
        }

        if (scroll.verticalNormalizedPosition > 0.7 && scroll.verticalNormalizedPosition < 0.8)
        {
            Pho.sprite = photo[1];
            Music[1].SetActive(true);

        }
        else
        {
        
            Music[1].SetActive(false);
        }

        if (scroll.verticalNormalizedPosition > 0.4 && scroll.verticalNormalizedPosition < 0.65)
        {
            Pho.sprite = photo[2];
            Music[2].SetActive(true);

        }
        else
        {
           
            Music[2].SetActive(false);
        }

        if (scroll.verticalNormalizedPosition > 0.1 && scroll.verticalNormalizedPosition < 0.35)
        {
            Pho.sprite = photo[3];
            Music[3].SetActive(true);

        }
        else
        {

            Music[3].SetActive(false);
        }

        if (scroll.verticalNormalizedPosition < 0.09)
        {
            Pho.sprite = photo[4];
            Music[4].SetActive(true);

        }
        else
        {
     
            Music[4].SetActive(false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        float posY = scroll.verticalNormalizedPosition;
        int index = 0;
        float offset = Math.Abs(pagevArray[index] - posY);

        for (int i = 1; i < pagevArray.Length; i++)
        {
            float offsetTemp = Math.Abs(pagevArray[i] - posY);
            if (offsetTemp < offset)
            {
                index = i;
                offset = offsetTemp;
            }
        }

        targetVerticalposition = pagevArray[index];

    }

}
