using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Abstract Significa que a classe não pode ser instanciada, só herdada
public abstract class Caractere : MonoBehaviour
{
    public float inicioPontosDano;  // valor minimo inicial de saude do Player
    public float MaxPontoDano;      // valor maximo permitido de saude do Player

    public abstract void ResetCaractere();

    public virtual IEnumerator FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

    public virtual void KillCaractere()
    {
        Destroy(gameObject);
    }
}
