using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class DetectaFala : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Cachorro", Up);
        actions.Add("Subway", Down);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += ReconizedSpeech;
        keywordRecognizer.Start();
    }

    private void ReconizedSpeech(PhraseRecognizedEventArgs speech) 
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();

    }

    private void Up()
    {
        transform.Translate(0, 1, 0);
    }
    private void Down()
    {
        transform.Translate(0, -1, 0);
    }

}
