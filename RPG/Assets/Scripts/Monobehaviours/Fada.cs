using UnityEngine;
using TMPro;

/// <summary>
/// Gerencia o NPC fada.
/// </summary>
public class Fada : MonoBehaviour
{
    // Balão de fala.
    public GameObject balaoFala;

    // Texto a ser exibido.
    public GameObject texto;

    // Biblioteca para lidar melhor com texto.
    private TextMeshPro textMeshPro;

    // Collider da fada.
    private CircleCollider2D circleCollider2D;

    // Dica sendo exibida atualmente.
    private int numeroDica;

    private int contador;

    /*
    * Inicialização do script.
    */
    private void Awake()
    {
        circleCollider2D = GameObject.Find("Fada").GetComponent<CircleCollider2D>();
        textMeshPro = texto.GetComponent<TextMeshPro>();
        balaoFala.SetActive(false);
        texto.SetActive(false);
        numeroDica = 0;
        contador = 26;

    }

    /*
    * Configura o texto a ser exibido.
    * text é o novo texto a aparecer na fala
    */
    private void SetupText(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
    }

    /*
    * Controlador de dica da fada.
    */
    private void EstagioConversa() // NAO Esta Funcionando
    {
        if (numeroDica == 1)
        {
            SetupText("Para completar o seu pote de ouro\nvoc� precisar� enfrentar o BOSS!\n(Aperte E para pr�xima Dica)");
        }
        else if (numeroDica == 2)
        {
            SetupText("Cuidado! Voc� precisa estar\nbem forte para enfrent�-lo!\n(Aperte E para pr�xima Dica)");
        }
        else
        {
            SetupText("Ache a po��o m�gica\n antes que ele te ache.\nVoc� ganhar� super poderes!");
        }

        numeroDica++;

        if (numeroDica > 3)
        {
            numeroDica = 1;
        }
    }

    /*
    *Trata da colisão entre a fada e o jogador.
    *collision é o objeto colidido
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            balaoFala.SetActive(true);
            texto.SetActive(true);
            SetupText("Para completar o seu pote de ouro\nvoc� precisar� enfrentar o BOSS!\nAche a po��o m�gica para enfrent�-lo!");
        }
    }

    /*
    *Trata do fim da colisão entre a fada e o jogador.
    *collision é o objeto colidido
    */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            balaoFala.SetActive(false);
            texto.SetActive(false);
        }
    }
}