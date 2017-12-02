using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstantiateUI : MonoBehaviour {

    public Image _samplePrefab;
    public Material[] mat;

    Image[] _sampleCube = new Image[110];
    public float pox;
    public float _maxScale;
    // Use this for initialization
    void Start()
    {
        pox = this.transform.position.x;
        for (int i = 0; i < 100; i++)
        {
            Image _instanceCube = (Image)Instantiate(_samplePrefab);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.SetParent(this.transform);
            pox += 1.5f;
            _instanceCube.name = "SampleCube" + i;
           // this.transform.position = new Vector3();
      //      _instanceCube.transform.position = Vector3.up * 30;
            _instanceCube.transform.position = new Vector3(pox, _instanceCube.transform.position.y, _instanceCube.transform.position.z-1);
            _sampleCube[i] = _instanceCube;
            _sampleCube[i].fillAmount = 0;
 
             _sampleCube[i].material = mat[0];

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayG.m == 0 || PlayG.sh != 0 || PlayG.star != 0)
        {
            for (int i = 0; i < 100; i++)
            {

                if (_sampleCube != null)
                {
                    float mm;

                    mm = (AudioPeer._samples[i] * _maxScale) + 2;
                    if (mm > 10) { mm = 10; }
                    else if (mm == 0) mm = 1;

                    _sampleCube[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    _sampleCube[i].fillAmount = mm * 0.1f;

                }

            }
        }
        else
        {
            for (int i = 0; i < 100; i++)
            {

                if (_sampleCube != null)
                {
                    float mm = 1;

                    _sampleCube[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    _sampleCube[i].fillAmount = mm * 0.1f;

                }

            }
        }

    }
}
