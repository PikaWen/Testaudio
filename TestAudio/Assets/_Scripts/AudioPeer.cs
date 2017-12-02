using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {

    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBand = new float[8];
    public static float[] _Bandbuffer = new float[8];
    float[] _bufferDecrease = new float[8];

	// Use this for initialization
	void Start () {       
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        _audioSource = GetComponent<AudioSource>();

       GetSpectrumAudioSource();
       MakeFrequencyBands();
       BandBuffer();
        
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0 , FFTWindow.Blackman);
    }

    public void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _Bandbuffer[g])
            {
                _Bandbuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if (_freqBand[g] < _Bandbuffer[g])
            {
                _Bandbuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.5f;
            }


        }
    }

    public void MakeFrequencyBands()
    {
        /*
         * 22050 / 512 = 43 Hz   總共 22050 頻道 512 平均 43 Hz
         * 
         * 20-60
         * 60-250
         * 250-500
         * 500-2000
         * 2000-4000
         * 4000-6000
         * 6000-20000
         * 
         * 
         * 0 - 2 = 86hz
         * 1 - 4 = 172hz = 87 - 258
         * 2 - 8 = 344hz = 259 - 602
         * 3 - 16 = 688hz = 603 - 1290
         * 4 - 32 = 1376hz = 1291 - 2666
         * 5 - 64 = 2752hz = 2667 - 5418
         * 6 - 128 = 5504hz = 5419 - 10922
         * 7 - 256 = 11008hz = 10923 - 21930
         * 加起來 510 頻道
         * 
         */
       
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int _sampleCount = (int)Mathf.Pow(2, i) * 2;

            for (int j = 0; j < _sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;

        }
    }

    
}
