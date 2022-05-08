using System.Collections;
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

    // Armazena o prefab da espada.
    public GameObject espadaPrefab;

    // Piscina de municao.
    static List<GameObject> municaoPiscina;

    // Tamanho da Piscina.
    public int tamanhoPiscina;

    // Velocidade da municao.
    public float velocidadeArma;

    // Velocidade da espada.
    public float velocidadeEspada;

    // Audios da espada.
    public AudioClip[] audiosEspada;


    // Armazena e reutiliza a espada.
    GameObject espada;

    // Armazena a rotacao inicial da espada.
    Vector3 espadaAngulos;

    bool atirando;


    [HideInInspector]
    public Animator animator;

    Camera cameraLocal;

    float slopePositivo;
    float slopeNegativo;

    enum Quadrante
    {
        Leste,
        Sul,
        Oeste,
        Norte
    }

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

    bool AcimaSlopePositivo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopePositivo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopePositivo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

    bool AcimaSlopeNegativo(Vector2 posicaoEntrada)
    {
        Vector2 posicaoPlayer = gameObject.transform.position;
        Vector2 posicaoMouse = cameraLocal.ScreenToWorldPoint(posicaoEntrada);
        float interseccaoY = posicaoPlayer.y - (slopeNegativo * posicaoPlayer.x);
        float entradaInterseccao = posicaoMouse.y - (slopeNegativo * posicaoMouse.x);
        return entradaInterseccao > interseccaoY;
    }

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


    private void Awake()
    {
        municaoPiscina ??= new List<GameObject>();
        for (int i = 0; i < tamanhoPiscina; i++)
        {
            GameObject municaoO = Instantiate(municaoPrefab);
            municaoO.SetActive(false);
            municaoPiscina.Add(municaoO);
        }

        espada = Instantiate(espadaPrefab);
        espadaAngulos = espada.transform.eulerAngles;
        espada.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atirando = true;
            //DisparaMunicao();
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("Apertei melee");
            //AtacaMelee();
        }
        UpdateEstado();
    }

    float PegaSlope(Vector2 ponto1, Vector2 ponto2)
    {
        return (ponto2.y - ponto1.y) / (ponto2.x - ponto1.x);
    }

    public GameObject SpawnMunicao(Vector3 posicao)
    {
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

    public GameObject SpawnEspada(Vector3 posicao)
    {
        espada.SetActive(true);
        espada.transform.position = posicao;
        espada.transform.eulerAngles = espadaAngulos;
        return espada;
    }

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

    void AtacaMelee()
    {
        var posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var espada = SpawnEspada(transform.position);

        var angulo = 0.0f;
        var deslocamento = new Vector3();
        var quadrante = PegaQuadrante();

        if (espada != null)
        {
            var script = espada.GetComponent<Swing>();
            var duracao = 1.0f / velocidadeEspada;

            var altura = espada.GetComponent<SpriteRenderer>().bounds.size.x;
            var largura = espada.GetComponent<SpriteRenderer>().bounds.size.y;
            switch (quadrante)
            {
                case Quadrante.Leste: angulo = 0.0f; deslocamento.x += largura / 2; break;
                case Quadrante.Norte: angulo = 90.0f; deslocamento.y += altura / 2; break;
                case Quadrante.Oeste: angulo = 180.0f; deslocamento.x -= largura / 2; break;
                case Quadrante.Sul: angulo = 270.0f; deslocamento.y -= altura / 2; break;
            }

            StartCoroutine(script.swing(deslocamento, angulo, duracao, gameObject));
        }
    }

    private void OnDestroy()
    {
        municaoPiscina = null;
    }
}
