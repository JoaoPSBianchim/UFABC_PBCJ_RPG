using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
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


    private void FixedUpdate()
    {
        MoveCaractere();
    }

    private void MoveCaractere()
    {
        Movimento.x = Input.GetAxisRaw("Horizontal");
        Movimento.y = Input.GetAxisRaw("Vertical");
        Movimento.Normalize();
        rb2D.velocity = Movimento * velocidadeMovimento;
    }

    void UpdateEstado()
    {
        if (Mathf.Approximately(Movimento.x, 0) && (Mathf.Approximately(Movimento.y, 0)))
        {
            animator.SetBool("Caminhando", false);
        }
        else
        {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("dirX", Movimento.x);
        animator.SetFloat("dirY", Movimento.y);
    }
}
