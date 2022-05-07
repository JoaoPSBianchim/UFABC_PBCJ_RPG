using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoInimigo : MonoBehaviour
{
    public float velocidadeMovimento = 3.0f;    //Equivale ao momento(impulso) a ser dado ao Player
    Vector2 Movimento = new Vector2();          //Detectar movimento pelo teclado

    Animator animator;                          //Guarda a componente do Controlador de Animacao
    Rigidbody2D rb2D;                           //Guarda a componente Corpo Rigido do Player


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEstado();
    }

    void UpdateEstado()
    {
        Vector2 novaPosicao = GetComponent<Rigidbody2D>().transform.position;
        if (Mathf.Approximately(novaPosicao.x, 0) && (Mathf.Approximately(novaPosicao.y, 0)))
        {
            animator.SetBool("Caminhando", false);
        }
        else
        {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("dirX", novaPosicao.x);
        animator.SetFloat("dirY", novaPosicao.y);
    }
}
