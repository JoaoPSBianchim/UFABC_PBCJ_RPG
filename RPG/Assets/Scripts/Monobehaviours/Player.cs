using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Caractere
{
    public Inventario inventarioPrefab;     //Referencia ao objeto prefab criado do Inventario
    Inventario inventario;
    public HealthBar healthBarPrefab;       //Referencia ao Objeto prefab criado da HealthBar
    HealthBar healthBar;

    public PontosDano pontosDano;   //tem o valor da saude do objeto script

    private void Start()
    {
        inventario = Instantiate(inventarioPrefab);
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
    }

    public override IEnumerator DanoCaractere(int dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if(pontosDano.valor <= float.Epsilon)
            {
                KillCaractere();
            }
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }
            else
            {
                break;
            }
        }
    }

    public override void ResetCaractere()
    {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }

    public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            Item DanoObjeto = collision.gameObject.GetComponent<Consumable>().item;
            print(DanoObjeto.tipoItem);
            if(DanoObjeto != null)
            {
                bool deveDesaparecer = false;
                switch (DanoObjeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.HEALTH:
                        deveDesaparecer = AjustePontosDano(DanoObjeto.quantidade);
                        break;

                    case Item.TipoItem.DIAMANTE:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    case Item.TipoItem.ESCUDO:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;

                    default:
                        break;
                }

                if (deveDesaparecer)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }

    }

    public bool AjustePontosDano(int quantidade)
    {
        if (pontosDano.valor < MaxPontoDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            print("Ajustando PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        }
        else return false;
        
    }
}
