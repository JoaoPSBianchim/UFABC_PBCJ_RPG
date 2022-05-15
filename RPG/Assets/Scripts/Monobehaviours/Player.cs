using System.Collections;
using UnityEngine;

/// <summary>
/// Controla ações gerais do jogador.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Player : Caractere
{
    // Referencia ao objeto prefab criado do Inventario.
    public Inventario inventarioPrefab;

    // Referencia ao Objeto prefab criado da HealthBar.
    public HealthBar healthBarPrefab;

    // Audio quando coleta-se um coletável.
    public AudioSource audioColetavel;

    // Animator do player.
    [HideInInspector]
    public Animator animator;

    // Espada do player (para utilizar no power up).
    public Weapon espada;

    // Inventário do jogador.
    Inventario inventario;

    // Barra de vida do jogador.
    HealthBar healthBar;

    // Tem o valor da saude do objeto script.
    public PontosDano pontosDano;

    /*
    Chamado antes da atualização do primeiro frame.
    */
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Super", false);
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;

        if (pontosDano.valor == 0f)
        {
            pontosDano.valor = inicioPontosDano;
        }
    }

    /*
    * Coroutine para atualizar o dano sofrido pelo player.
    * Dano é o dano sofrido pelo Player
    * intervalo é o intervalor entre os danos sofridos
    */
    public override IEnumerator DanoCaractere(float dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if (pontosDano.valor <= float.Epsilon)
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

    /*
    * Reinicia os dados do player.
    */
    public override void ResetCaractere()
    {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }

    /*
    * Destrói o player, e.g. quando o personagem morre (PD = 0).
    */
    public override void KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }

    /*
    * Trigger para quando ocorre uma colisão.
    * collision é o objeto colidido
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            var consumable = collision.gameObject.GetComponent<Consumable>();
            var objeto = consumable.item;
            print(objeto.tipoItem);

            if (objeto != null)
            {
                var deveDesaparecer = false;

                // Trata a colisão de colectáveis de forma diferente, dependendo do coletável.
                switch (objeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                    case Item.TipoItem.DIAMANTE:
                    case Item.TipoItem.ESCUDO:
                    case Item.TipoItem.KNIFE:
                        deveDesaparecer = inventario.AddItem(objeto);
                        break;

                    case Item.TipoItem.HEALTH:
                        deveDesaparecer = AjustePontosDano(objeto.quantidade);
                        break;

                    case Item.TipoItem.POTION:
                        PowerUp();
                        deveDesaparecer = inventario.AddItem(objeto);
                        print(MaxPontoDano.ToString() + pontosDano.valor.ToString() + " ");
                        break;

                    default:
                        break;
                }

                if (deveDesaparecer)
                {
                    collision.gameObject.SetActive(false);

                    if (audioColetavel != null)
                    {
                        audioColetavel.Play();
                    }
                }
            }
        }

    }

    /*
    * Altera a vida do player.
    * quantidade é a quantidade de vida a ser adicionada
    */
    public bool AjustePontosDano(float quantidade)
    {
        if (pontosDano.valor < MaxPontoDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            print("Ajustando PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        }
        else return false;

    }

    /*
    * Melhora os atributos do player.
    */
    public void PowerUp()
    {
        animator.SetBool("Super", true);
        MaxPontoDano *= 2;
        healthBar.maxPontosDano *= 2;
        AjustePontosDano(pontosDano.valor);

        if (espada != null)
        {
            espada.dano *= 2;
        }
    }
}
