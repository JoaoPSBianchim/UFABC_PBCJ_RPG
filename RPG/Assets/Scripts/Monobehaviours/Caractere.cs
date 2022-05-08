using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Define propriedades comuns de um character do jogo.
/// Abstract significa que a classe não pode ser instanciada, só herdada.
/// </summary>
public abstract class Caractere : MonoBehaviour
{
    // Valor minimo inicial de saude do Player
    public float inicioPontosDano;

    // Valor maximo permitido de saude do Player
    public float MaxPontoDano;

    /// <summary>
    /// Função abstrata para redefinir os atributos do character.
    /// </summary>
    public abstract void ResetCaractere();

    /// <summary>
    /// Animação para quando recebe um golpe.
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    /// <summary>
    /// Trata o dano recebido por golpes externos.
    /// </summary>
    /// <param name="dano">Dano recebido.</param>
    /// <param name="intervalo">Intervalor entre os danos recebidos.</param>
    /// <returns></returns>
    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

    /// <summary>
    /// Destrói o character.
    /// </summary>
    public virtual void KillCaractere()
    {
        Destroy(gameObject);
    }
}
