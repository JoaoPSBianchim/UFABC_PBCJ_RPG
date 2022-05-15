using System.Collections;
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

    /**
     * Função abstrata para redefinir os atributos do character.
     */
    public abstract void ResetCaractere();

    /**
     * Animação para quando recebe um golpe.
     */
    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    /**
     * Trata o dano recebido por golpes externos.
     * dano é o dano recebido.
     * intevalo é o intervalo entre os danos recebidos.
     */
    public abstract IEnumerator DanoCaractere(float dano, float intervalo);

    /**
     * Destrói o character.
     */
    public virtual void KillCaractere()
    {
        Destroy(gameObject);
    }
}
