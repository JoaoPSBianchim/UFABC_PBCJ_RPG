using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    public IEnumerator swing(Vector3 deslocamento, float angulo, float duracao, GameObject player)
    {
        var anguloObjetivo = new Vector3(0f, 0f, angulo - 90.0f);
        var anguloAtual = new Vector3(0f, 0f, angulo);
        var percentualCompleto = 0.0f;
        var pivot = gameObject.transform.GetChild(0);
        var deslocamentoPivot = transform.position - pivot.transform.position;

        while (percentualCompleto < 1.0f)
        {
            percentualCompleto += Time.deltaTime / duracao;

            var r = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(r.x, r.y, Mathf.LerpAngle(anguloAtual.z, anguloObjetivo.z, percentualCompleto));

            var v = transform.rotation * deslocamentoPivot;
            transform.position = v + player.transform.position + deslocamento;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
