using UnityEngine;

/// <summary>
/// Gerencia ataques de armas (espada do jogador, faca do jogador, ataques dos inimigos, etc).
/// </summary>
public class Weapon : MonoBehaviour
{
    // Poder da arma
    public float dano;

    // Alvo do ataque ("Inimigo" se a arma deveria atacar um inimigo, "Player" se deveria atacar o player, etc).
    public string tagAlvo;

    /**
     * Executa dano ao caractere quando colisão é detectada.
     * collision é o objeto sendo colidido.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag(tagAlvo))
        {
            var caractere = collision.gameObject.GetComponent<Caractere>();
            print($"acertei caractere, dano: {dano}");
            StartCoroutine(caractere.DanoCaractere(dano, 0.0f));
        }
    }
}
