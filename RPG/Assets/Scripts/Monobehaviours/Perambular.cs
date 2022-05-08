using System.Collections;
using UnityEngine;

/// <summary>
/// Comportamento IA de perâmbulo e perseguição dos inimigos.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Perambular : MonoBehaviour
{
    // Velocidade do Inimigo na área de Spot.
    public float velocidadePerseguicao;

    // Velocidade do Inimigo passeando.
    public float velocidadePerambular;

    // Velocidade do Inimigo atribuída.
    float velocidadeCorrente;

    // Tempo para alterar a direcao.
    public float intervalorMudancaDirecao;

    // Indicador de perseguidor ou nao.
    public bool perseguePlayer;

    // Máxima distância que o inimigo pode perambular.
    public float maxDistancia;

    // Coroutine de movimentação.
    Coroutine moveCoroutine;

    // Armazena o componente RigidBody2D.
    Rigidbody2D rb2D;

    // Armazena o componente Animator.
    Animator animator;

    // Armazena o componente Transform do alvo.
    Transform alvoTransform;

    // Posicao final de movimentação.
    Vector3 posicaoFinal;

    // Angulo atribuido.
    float anguloAtual = 0;

    // Armazena o componente Spot.
    CircleCollider2D circleCollider;

    /// <summary>
    /// Chamado antes da atualização do primeiro frame.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePerambular;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(rotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Para visualizar os componentes na tela (principalmente de colisão).
    /// </summary>
    private void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    /// <summary>
    /// Coroutine para perambular o inimigo, uma espécie de IA.
    /// </summary>
    /// <returns></returns>
    public IEnumerator rotinaPerambular()
    {
        while (true)
        {
            EscolheNovoPontoFinal();
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
            yield return new WaitForSeconds(intervalorMudancaDirecao);
        }
    }

    /// <summary>
    /// Randomiza o novo ponto final de movimentação de perâmbulo.
    /// </summary>
    void EscolheNovoPontoFinal()
    {
        anguloAtual += Random.Range(0, 360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal = Vector3ParaAngulo(anguloAtual);
    }

    /// <summary>
    /// Obtém posição a partir do ângulo.
    /// </summary>
    /// <param name="anguloEntradaGraus">Ângulo para obter a posição.</param>
    /// <returns>Vetor posição.</returns>
    Vector3 Vector3ParaAngulo(float anguloEntradaGraus)
    {
        float anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        float distancia = Random.Range(1, maxDistancia + 1);
        return transform.position + (new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0)) * distancia;
    }

    /// <summary>
    /// Realiza a movimentação do inimigo.
    /// </summary>
    /// <param name="rbParaMover">Corpo rígido para aplicar física.</param>
    /// <param name="velocidade">Velocidade de movimentação.</param>
    /// <returns></returns>
    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
        while (distanciaFaltante > float.Epsilon)
        {
            if (alvoTransform != null)
            {
                posicaoFinal = alvoTransform.position;
            }
            if (rbParaMover != null)
            {
                animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                rb2D.MovePosition(novaPosicao);
                var translacao = transform.position - posicaoFinal;
                distanciaFaltante = translacao.sqrMagnitude;

                // Atualiza as variáveis de animação.
                if (Mathf.Abs(translacao.x) > Mathf.Abs(translacao.y))
                {
                    animator.SetFloat("dirX", translacao.x < 0 ? 1 : -1);
                    animator.SetFloat("dirY", 0);
                }
                else
                {
                    animator.SetFloat("dirX", 0);
                    animator.SetFloat("dirY", translacao.y < 0 ? 1 : -1);
                }
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);
        print("caminhando");
    }

    /// <summary>
    /// Trigger da colisão de spot.
    /// </summary>
    /// <param name="collision">Objeto colidido.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && perseguePlayer)
        {
            velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.gameObject.transform;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
        }
    }

    /// <summary>
    /// Finalização da trigger da colisão de spot.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Caminhando", false);
            velocidadeCorrente = velocidadePerambular;
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            alvoTransform = null;
        }
    }

    /// <summary>
    /// Atualização a cada frame.
    /// </summary>
    void Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red);
    }

}
