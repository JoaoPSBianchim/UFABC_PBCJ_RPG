using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            print("acertei Inimigo");
            StartCoroutine(inimigo.DanoCaractere(dano, 0.0f));
        }
    }
}
