using System.Collections;
using UnityEngine;

/// <summary>
/// Define a trajetória de um arco (para as munições principalmente).
/// </summary>
public class Arco : MonoBehaviour
{
    /// <summary>
    /// Coroutine que executa um trajeto de arco.
    /// </summary>
    /// <param name="destino">Destino do arco.</param>
    /// <param name="duracao">Em quanto tempo o arco deve ser executado.</param>
    /// <returns></returns>
    public IEnumerator arcoTrajetoria(Vector3 destino, float duracao)
    {
        var posicaoInicial = transform.position;
        var percentualCompleto = 0.0f;

        while (percentualCompleto < 1.0f)
        {
            percentualCompleto += Time.deltaTime / duracao;
            transform.position = Vector3.Lerp(posicaoInicial, destino, percentualCompleto);
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
