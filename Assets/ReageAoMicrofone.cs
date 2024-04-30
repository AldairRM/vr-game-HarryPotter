using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReageAoMicrofone : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioClip microphoneClip;
    public int sampleWindow = 64;
    void Start()
    {
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip() 
    {
        string MicrophoneName = Microphone.devices[0];
        Debug.Log(MicrophoneName);
        microphoneClip = Microphone.Start(MicrophoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone() 
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip) 
    {
        int startPosition = clipPosition - sampleWindow;
        if (startPosition < 0)
            return 0;

        float[] waveDate = new float[sampleWindow];
        clip.GetData(waveDate, startPosition);

        float totalLoudness = 0f;

        for (int i = 0; i < sampleWindow; i++) 
        {
            totalLoudness += Mathf.Abs(waveDate[i]);
        }

        return totalLoudness/sampleWindow;
    }

    // Update is called once per frame
    void Update()
    {
        float tamanho = GetLoudnessFromMicrophone() * 100;

        if (tamanho < 0.1f) 
        {
            tamanho = 0;
        }

        transform.localScale = Vector3.Lerp(new Vector3(1,1,1), new Vector3(3,3,3), tamanho);
    }
}
