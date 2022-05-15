using System.Collections;
using UnityEngine;

/// <summary>
/// Define a trajetória de um arco (para as munições principalmente).
/// </summary>
public class Arco : MonoBehaviour
{
    /*
    *Coroutine que executa um trajeto de arco.
    *destino é o destino do arco
    *duracao é quanto tempo leva para o arco ser executado
    */
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
