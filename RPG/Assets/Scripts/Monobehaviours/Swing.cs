using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public IEnumerator swing(float anguloInicial, float duracao)
    {
        var posicaoInicial = transform.position;
        var percentualCompleto = 0.0f;

        while (percentualCompleto < 90.0f)
        {
            percentualCompleto += Time.deltaTime / duracao;
            transform.RotateAround(transform.position, Vector3.forward, percentualCompleto);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
