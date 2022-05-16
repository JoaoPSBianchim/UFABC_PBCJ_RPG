using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Gerencia o NPC fada.
/// </summary>
public class Fada : MonoBehaviour
{
    // Balão de fala.
    public GameObject balaoFala;

    // Objeto com o texto de exibição.
    public GameObject conteudoObject;

    // Objeto com o texto de "proximo".
    public GameObject proximoObject;

    // Classe que possui as sentenças para diálogo.
    public Dialogue dialogue;

    // Texto de exibição.
    TextMeshPro conteudo;

    // Collider da fada.
    CircleCollider2D circleCollider2D;

    // Armazena as sentenças do dialógo.
    Queue<string> sentences;

    // Indica se o balão de fala está sendo mostrado.
    bool conversando = false;

    /// <summary>
    /// Inicialização do script.
    /// </summary>
    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        conteudo = conteudoObject.GetComponent<TextMeshPro>();
        balaoFala.SetActive(false);
        proximoObject.SetActive(true);
        sentences = new Queue<string>();
    }

    /// <summary>
    /// Trata da colisão entre a fada e o jogador.
    /// </summary>
    /// <param name="collision">Objeto colidido.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IniciarConversa(dialogue, gameObject);
        }
    }

    /// <summary>
    /// Trata do fim da colisão entre a fada e o jogador.
    /// </summary>
    /// <param name="collision">Objeto colidido.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FinalizarConversa();
        }
    }

    /**
     * Configura o texto a ser exibido.
     * text é a sentença a ser exibida.
     */
    private void SetupSentence(string text)
    {
        conteudo.SetText(text);
        conteudo.ForceMeshUpdate();
    }

    /**
     * Inicializa a conversa da fada.
     */
    public void IniciarConversa(Dialogue dialogue, GameObject obj)
    {
        sentences.Clear();
        dialogue.sentences.ToList().ForEach(e => sentences.Enqueue(e));
        SetupSentence(sentences.Dequeue());

        balaoFala.SetActive(true);
        proximoObject.SetActive(true);
        conversando = true;
    }

    /**
     * Finaliza a conversa da fada.
     */
    public void FinalizarConversa()
    {
        balaoFala.SetActive(false);
        conversando = false;
    }

    /**
     * Continua a conversa da fada.
     */
    public void ContinuarConversa()
    {
        if (sentences.Count <= 0)
        {
            return;
        }

        var sentence = sentences.Dequeue();
        SetupSentence(sentence);

        if (sentences.Count <= 0)
        {
            proximoObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && conversando)
        {
            ContinuarConversa();
        }
    }
}