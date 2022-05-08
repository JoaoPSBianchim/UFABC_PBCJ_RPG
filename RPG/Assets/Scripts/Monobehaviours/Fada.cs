using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fada : MonoBehaviour
{
    public GameObject balaoFala;
    public GameObject texto;

    private TextMeshPro textMeshPro;
    private CircleCollider2D circleCollider2D;
    private int numeroDica;
    private int contador;
    private void Awake()
    {
        circleCollider2D = GameObject.Find("Fada").GetComponent<CircleCollider2D>();
        textMeshPro = texto.GetComponent<TextMeshPro>();
        balaoFala.SetActive(false);
        texto.SetActive(false);
        numeroDica = 0;
        contador = 26;

    }

    private void SetupText(string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
    }

    private void EstagioConversa() // NAO Esta Funcionando
    {
        if (numeroDica == 1)
        {
            SetupText("Para completar o seu pote de ouro\nvocê precisará enfrentar o BOSS!\n(Aperte E para próxima Dica)");
        }
        else if (numeroDica == 2)
        {
            SetupText("Cuidado! Você precisa estar\nbem forte para enfrentá-lo!\n(Aperte E para próxima Dica)");
        }
        else
        {
            SetupText("Ache a poção mágica\n antes que ele te ache.\nVocê ganhará super poderes!");
        }

        numeroDica++;

        if (numeroDica > 3)
        {
            numeroDica = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            balaoFala.SetActive(true);
            texto.SetActive(true);
            SetupText("Para completar o seu pote de ouro\nvocê precisará enfrentar o BOSS!\nAche a poção mágica para enfrentá-lo!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            balaoFala.SetActive(false);
            texto.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}