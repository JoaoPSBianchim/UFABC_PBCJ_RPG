using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla as ações que envolvem o armamento do player.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Armas : MonoBehaviour
{
    // Armazena o Prefab da Municao.
    public GameObject municaoPrefab;

    // Piscina de municao.
    static List<GameObject> municaoPiscina;

    // Tamanho da Piscina.
    public int tamanhoPiscina;

    // Velocidade da municao.
    public float velocidadeArma;

    // Audios da espada.
    public AudioClip[] audiosEspada;

    // Variável de controle se está atirando ou não.
    bool atirando;

    // Animator para o sprite do jogador.
    [HideInInspector]
    public Animator animator;

    // Câmera para obter referências de slopes.
    Camera cameraLocal;

    // Slope positivo (para cálculo de quadrantes em relação ao player).
    float slopePositivo;

    // Slope negativo (para cálculo de quadrantes em relação ao player).
    float slopeNegativo;

    // Quadrante em relação ao player.
    enum Quadrante
    {
        Leste,
        Sul,
        Oeste,
        Norte
    }

    /*
    * Chamado antes da atualização do primeiro frame.
    */
    private void Start()
    {
        animator = GetComponent<Animator>();
        atirando = false;

        cameraLocal = Camera.main;

        Vector2 abaixoEsquerda = cameraLocal.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 acimaDireita = cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 acimaEsquerda = cameraLocal.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 abaixoDireita = cameraLocal.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        slopePositivo = PegaSlope(abaixoEsquerda, acimaDireita);
        slopeNegativo = PegaSlope(acimaEsquerda, abaixoDireita);
    }

    /*
    * Checa se o ponto está acima do slope positivo ou não.
    * posicaoEntrada é o ponto a ser verificado
    */
    bool AcimaSlopePositivo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopePositivo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopePositivo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    /*
    * Checa se o ponto está acima do slope negativo ou não. 
    * posicaoEntrada é o ponto a ser verificado
    */
    bool AcimaSlopeNegativo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopeNegativo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopeNegativo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    /*
    * Obtém quadrante do local de click do mouse com relação ao jogador.
    */
    Quadrante PegaQuadrante()
    {
        Vector2 posicaoMouse = Input.mousePosition;
        Vector2 posicaoPlayer = transform.position;
        bool acimaSlopePositivo = AcimaSlopePositivo(Input.mousePosition);
        bool acimaSlopeNegativo = AcimaSlopeNegativo(Input.mousePosition);
        if (!acimaSlopePositivo && acimaSlopeNegativo)
        {
            return Quadrante.Leste;
        }
        if (!acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Sul;
        }
        if (acimaSlopePositivo && !acimaSlopeNegativo)
        {
            return Quadrante.Oeste;
        }
        else
        {
            return Quadrante.Norte;
        }
    }

    /*
    * Atualiza o estado do jogador.
    */
    void UpdateEstado()
    {
        if (atirando)
        {
            Vector2 vetorQuadrante;
            Quadrante quadranteEnum = PegaQuadrante();
            switch (quadranteEnum)
            {
                case Quadrante.Leste:
                    vetorQuadrante = new Vector2(1.0f, 0.0f);
                    break;
                case Quadrante.Sul:
                    vetorQuadrante = new Vector2(0.0f, -1.0f);
                    break;
                case Quadrante.Oeste:
                    vetorQuadrante = new Vector2(-1.0f, 0.0f);
                    break;
                case Quadrante.Norte:
                    vetorQuadrante = new Vector2(0.0f, 1.0f);
                    break;
                default:
                    vetorQuadrante = new Vector2(0.0f, 0.0f);
                    break;
            }
            animator.SetBool("Atirando", true);
            animator.SetFloat("AtiraX", vetorQuadrante.x);
            animator.SetFloat("AtiraY", vetorQuadrante.y);

            var audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource != null && audiosEspada.Length > 0)
            {
                audioSource.clip = audiosEspada[Random.Range(0, audiosEspada.Length)];
                audioSource.Play();
            }

            atirando = false;
        }
        else
        {
            animator.SetBool("Atirando", false);
        }
    }


    /*
    * Inicialização do script.
    */
    private void Awake()
    {
        municaoPiscina ??= new List<GameObject>();
        for (int i = 0; i < tamanhoPiscina; i++)
        {
            GameObject municaoO = Instantiate(municaoPrefab);
            municaoO.SetActive(false);
            municaoPiscina.Add(municaoO);
        }
    }

    /*
    * Atualização a cada frame.
    */
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atirando = true;
            //DisparaMunicao();
        }
        UpdateEstado();
    }

    /*
    * Obtém a inclinação de uma reta dados dois pontos.
    * ponto1 é o primeiro ponto
    * ponto2 é o segundo ponto
    */
    float PegaSlope(Vector2 ponto1, Vector2 ponto2)
    {
        return (ponto2.y - ponto1.y) / (ponto2.x - ponto1.x);
    }

    /*
    * Realiza spawn de uma munição.
    * posicao é o valor da posicao inicial da munição
    */
    public GameObject SpawnMunicao(Vector3 posicao)
    {
        // Utiliza Object Pooling para controlar as instâncias de munições.
        foreach (GameObject municao in municaoPiscina)
        {
            if (municao.activeSelf == false)
            {
                municao.SetActive(true);
                municao.transform.position = posicao;
                return municao;
            }
        }
        return null;
    }

    /*
    * Realiza o tiro de uma munição.
    */
    void DisparaMunicao()
    {
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject municao = SpawnMunicao(transform.position);
        if (municao != null)
        {
            Arco arcoScript = municao.GetComponent<Arco>();
            float duracaoTrajetoria = 1.0f / velocidadeArma;
            StartCoroutine(arcoScript.arcoTrajetoria(posicaoMouse, duracaoTrajetoria));
        }
    }

    /*
    * Destrói recursos em uso.
    */
    private void OnDestroy()
    {
        municaoPiscina = null;
    }
}
