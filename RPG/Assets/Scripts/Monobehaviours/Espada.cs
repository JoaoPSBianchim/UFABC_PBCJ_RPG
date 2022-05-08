using UnityEngine;

/// <summary>
/// Gerencia a espada do jogador.
/// </summary>
public class Espada : MonoBehaviour
{
    // Poder da espada
    public int dano;

    /// <summary>
    /// Executa dano ao inimigo quando colisão é detectada.
    /// </summary>
    /// <param name="collision">Objeto sendo colidido</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Inimigo inimigo = collision.gameObject.GetComponent<Inimigo>();
            print($"acertei inimigo, dano: {dano}");
            StartCoroutine(inimigo.DanoCaractere(dano, 0.0f));
        }
    }
}
