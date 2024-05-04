using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagonista : MonoBehaviour
{
    private int vida = 100;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void RemoveVida(int valor) 
    {
        //tem q mostrar a direção do dano!!!

        vida -= valor;
        if (vida <= 0)
        {
            //morre
            //animator.Play("Dead");
        }
    }
}
