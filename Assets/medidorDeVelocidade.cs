using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medidorDeVelocidade : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 velocidade;

    void Start()
    {
        // Define a posição inicial como a posição atual
        previousPosition = transform.position;
    }

    void Update()
    {
        // Calcula a velocidade atual do objeto
        velocidade = (transform.position - previousPosition) / Time.deltaTime;

        // Atualiza a posição anterior para a próxima atualização
        previousPosition = transform.position;

        // Exibe a velocidade nos eixos X, Y e Z
        //Debug.Log("Velocidade X: " + velocidade.x);
        //Debug.Log("Velocidade Y: " + velocidade.y);
        //Debug.Log("Velocidade Z: " + velocidade.z);

    }
}
