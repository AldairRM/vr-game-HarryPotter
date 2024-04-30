using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.XR.Interaction.Toolkit;

public class CastMagic : MonoBehaviour
{
    public GameObject lumos;
    public Transform spawnPoint;
    public Material materialCasting;
    public Material materialCastingOff;
    public GameObject ChangeMaterialObj;
    public bool casting = false;

    //reconhecer voz
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Lúmus", Lumos);
        actions.Add("Nóx", Nox);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += ReconizedSpeech;
    }


    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void ReconizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void CastingOn()
    {

        keywordRecognizer.Start();
        casting = true;



        var meshRenderer = ChangeMaterialObj.GetComponent<MeshRenderer>();
        var materialsCopy = meshRenderer.materials;
        materialsCopy[0] = materialCasting;
        meshRenderer.materials = materialsCopy;
    }

    public void CastingOff()
    {

        keywordRecognizer.Stop();
        casting = false;



        var meshRenderer = ChangeMaterialObj.GetComponent<MeshRenderer>();
        var materialsCopy = meshRenderer.materials;
        materialsCopy[0] = materialCastingOff;
        meshRenderer.materials = materialsCopy;
    }

    public void Lumos() 
    {
        lumos.SetActive(true);
    }


    public void Nox()
    {
        lumos.SetActive(false);
    }
}
