using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int vida = 3;
    private GameObject protagonista;
    [SerializeField]
    private Animator animator;
    public bool viuProtagonista = false;
    public bool podeAndar = true;

    [SerializeField]
    private float basicAttackRange = 5f;
    [SerializeField]
    private int basicAttackDamage = 30;



    //Start is called before the first frame update
    void Start()
    {
        protagonista = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Comportamento();
    }
    public virtual void Comportamento() 
    {
        if (viuProtagonista) 
        {
            MoveToPlayer();
        }

        if (Vector3.Distance(protagonista.transform.position, gameObject.transform.position) < basicAttackRange)
        {
            this.Attack();
        }
    }

    public virtual void MoveToPlayer() 
    {
        if (podeAndar)
        {
            animator.SetBool("running", true);
            //move em direção ao player
            this.GetComponent<NavMeshAgent>().SetDestination(protagonista.transform.position);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    public virtual void Attack() 
    {
        animator.Play("Attack");

        if (Vector3.Distance(protagonista.transform.position, gameObject.transform.position) < basicAttackRange) 
        {
            protagonista.GetComponent<Protagonista>().RemoveVida(basicAttackDamage);
        }
    }

    public void RemoveVida(int valor)
    {
        vida -= valor;
        if (vida <= 0) 
        {
            //morre
            animator.Play("Dead");
        }
    }
}
