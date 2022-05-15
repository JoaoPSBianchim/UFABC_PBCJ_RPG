using System.Collections;
using UnityEngine;

/// <summary>
/// Gerencia o trigger de ataque de um inimigo.
/// </summary>
[RequireComponent(typeof(CircleCollider2D))]
public class InimigoAttackSpot : MonoBehaviour
{
    // Temporizador de cooldown para os ataques, em segundos.
    public float cooldownAtaque;

    // Script de perâmbulo do inimigo relacionado.
    Perambular inimigo;

    // Animator do inimigo relacionado.
    Animator inimigoAnimator;

    // Collider de spot de ataque.
    CircleCollider2D circleCollider;

    // Alvo do ataque.
    GameObject alvo = null;

    /**
     * Setup inicial do objeto.
     */
    private void Start()
    {
        inimigo = GetComponentInParent<Perambular>();
        inimigoAnimator = inimigo?.GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        StartCoroutine(Attack());
    }

    /**
     * Trigger de colisão do objeto.
     * other é o objeto colidido.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && (inimigo?.perseguePlayer ?? false)
            && inimigoAnimator != null)
        {
            alvo = other.gameObject;
        }
    }

    /**
     * Trigger de término de colisão do objeto.
     * other é o objeto colidido.
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            alvo = null;
        }
    }

    /**
     * Para visualizar os componentes na tela (principalmente de colisão).
     */
    private void OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }

    /**
     * Coroutine para temporizar o cooldown entre cada ataque do inimigo.
     */
    private IEnumerator Attack()
    {
        while (true)
        {
            if (alvo != null)
            {
                var translacao = transform.position - alvo.transform.position;

                // Atualiza as variáveis de animação.
                if (Mathf.Abs(translacao.x) > Mathf.Abs(translacao.y))
                {
                    inimigoAnimator.SetFloat("AtacaX", translacao.x < 0 ? 1 : -1);
                    inimigoAnimator.SetFloat("AtacaY", 0);
                }
                else
                {
                    inimigoAnimator.SetFloat("AtacaX", 0);
                    inimigoAnimator.SetFloat("AtacaY", translacao.y < 0 ? 1 : -1);
                }
                inimigoAnimator.SetBool("Atacando", true);
            }

            yield return new WaitForSeconds(cooldownAtaque);
        }
    }
}
