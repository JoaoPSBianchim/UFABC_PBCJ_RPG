using UnityEngine;

/// <summary>
/// Gerencia uma munição atirada.
/// </summary>
public class Municao : MonoBehaviour
{
    public int danoCausado;         //poder da municao

    /// <summary>
    /// Trata da colisão entre a munição e um inimigo.
    /// </summary>
    /// <param name="collision">Objeto colidido.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Inimigo inimigo = collision.gameObject.GetComponent<Inimigo>();
            print("acertei Inimigo");
            StartCoroutine(inimigo.DanoCaractere(danoCausado, 0.0f));
            gameObject.SetActive(false);
        }
    }
}
