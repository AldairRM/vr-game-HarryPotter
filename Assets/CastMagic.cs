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
    //public GameObject ChangeMaterialObj;
    public GameObject Efeito;
    public GameObject MagiaBasica;
    public bool casting = false;

    //reconhecer voz
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Lúmus", Lumos);
        actions.Add("Lumôs", Lumos);

        actions.Add("Nóx", Nox);
        actions.Add("Nóquis", Nox);

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



        //var meshRenderer = ChangeMaterialObj.GetComponent<MeshRenderer>();
        //var materialsCopy = meshRenderer.materials;
        //materialsCopy[0] = materialCasting;
        //meshRenderer.materials = materialsCopy;
        Efeito.SetActive(true);
    }

    public void soltouOTrigger() 
    {
        if (casting == true)
        {
            var velocidade = Efeito.transform.parent.gameObject.GetComponent<medidorDeVelocidade>().velocidade;
            Debug.Log("Velocidade Final: " + velocidade.magnitude);
            if (velocidade.magnitude > 7f)
            {
                InvokeMagiaBasica();
                return;
            }
        }
        CastingOff();
    }

    public void CastingOff()
    {

        keywordRecognizer.Stop();
        casting = false;



        //var meshRenderer = ChangeMaterialObj.GetComponent<MeshRenderer>();
        //var materialsCopy = meshRenderer.materials;
        //materialsCopy[0] = materialCastingOff;
        //meshRenderer.materials = materialsCopy;
        Efeito.SetActive(false);
    }
    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void InvokeMagiaBasica() 
    {
        //filtrar para pegar apenas inimigos no campo de visão
        var alvos = GameObject.FindGameObjectsWithTag("Alvo");
        if (alvos.Length > 0) 
        {
            var closestEnemy = GetClosestEnemy(alvos);

            var oldEfeito = Efeito;

            //na verdade a nova bala fica na varinha e a antiga vaza;
            GameObject spawnBullet = Instantiate(MagiaBasica);
            spawnBullet.transform.SetParent(oldEfeito.transform.parent);
            spawnBullet.transform.localPosition = oldEfeito.transform.localPosition;

            Efeito = spawnBullet;
            oldEfeito.transform.SetParent(null);

            var componentNovo = oldEfeito.AddComponent<Movimento_MagiaBasica>();
            componentNovo.startPoint = oldEfeito.transform.position;
            componentNovo.endPoint = closestEnemy.transform.position;

            float distanceToWalk = 3f;
            // Obtém a direção atual do objeto
            Vector3 forwardDirection = oldEfeito.transform.forward;
            // Calcula a nova posição após andar a distância desejada
            Vector3 newPosition = oldEfeito.transform.position + forwardDirection * distanceToWalk;
            componentNovo.controlPoint = newPosition;

            //firespeed = 20f;
            //oldEfeito.transform.LookAt(closestEnemy.transform);
            //oldEfeito.GetComponent<Rigidbody>().velocity = oldEfeito.transform.forward * 20f;
            Destroy(oldEfeito, 5);

            Nox();
        }

    }

    public void Lumos() 
    {
        lumos.SetActive(true);
        CastingOff();
    }


    public void Nox()
    {
        lumos.SetActive(false);
        CastingOff();
    }
}
