using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia o inimigo.
/// </summary>
public class Inimigo : Caractere
{
    float pontosVida;               // Saude do inimigo
    public int forcaDano;           // Poder de dano

    // Coroutine de dano.
    Coroutine danoCoroutine;

    /// <summary>
    /// Função ao ser habilitado.
    /// </summary>
    private void OnEnable()
    {
        ResetCaractere();
    }

    /// <summary>
    /// Trata da colisão entre o inimigo e o jogador.
    /// </summary>
    /// <param name="collision">Objeto colidido.</param>
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

    /// <summary>
    /// Trata do fim da colisão entre o inimigo e o jogador.
    /// </summary>
    /// <param name="collision">Objeto colidido.</param>
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

    /// <summary>
    /// Coroutine para tratar danos recebidos.
    /// </summary>
    /// <param name="dano">Dano recebido.</param>
    /// <param name="intervalo">Intervalo entre os danos.</param>
    /// <returns></returns>
    public override IEnumerator DanoCaractere(int dano, float intervalo)
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

    /// <summary>
    /// Redefine os atributos do inimigo.
    /// </summary>
    public override void ResetCaractere()
    {
        pontosVida = inicioPontosDano;
    }
}
