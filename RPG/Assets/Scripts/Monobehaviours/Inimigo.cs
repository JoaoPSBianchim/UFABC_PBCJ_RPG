using System.Collections;
using UnityEngine;

/// <summary>
/// Gerencia o inimigo.
/// </summary>
public class Inimigo : Caractere
{
    // Saude do inimigo
    float pontosVida;

    // Poder de dano
    public float forcaDano;

    // Coroutine de dano.
    Coroutine danoCoroutine;

    /**
     * Função ao behaviour ser habilitado.
     */
    private void OnEnable()
    {
        ResetCaractere();
    }

    /**
     * Trata da colisão entre o inimigo e o jogador.
     * collision é o objeto colidido.
     */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (danoCoroutine == null)
            {
                danoCoroutine = StartCoroutine(player.DanoCaractere(forcaDano, 1.0f));
            }
        }
    }

    /**
     * Trata do fim da colisão entre o inimigo e o jogador.
     * collision é o objeto colidido.
     */
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }
    }

    /**
     * Coroutine para tratar danos recebidos.
     * dano é o dano recebido.
     * intervalo é o intervalo para esperar entre cada dano.
     */
    public override IEnumerator DanoCaractere(float dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosVida = pontosVida - dano;
            if (pontosVida <= float.Epsilon)
            {
                KillCaractere();
                break;
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }

    /**
     * Redefine os atributos do inimigo.
     */
    public override void ResetCaractere()
    {
        pontosVida = inicioPontosDano;
    }
}
