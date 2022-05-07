using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Armas : MonoBehaviour
{
    // Armazena o Prefab da Municao
    public GameObject municaoPrefab;

    // Armazena o prefab da espada
    public GameObject espadaPrefab;

    // Piscina de municao
    static List<GameObject> municaoPiscina;

    // Tamanho da Piscina
    public int tamanhoPiscina;

    // Velocidade da municao
    public float velocidadeArma;

    // Velocidade da espada, em graus/s
    public float velocidadeEspada;

    // Armazena a espada do player
    GameObject espada;

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
        espada.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            atirando = true;
            print("Apertei");
            DisparaMunicao();
        }
        if (Input.GetMouseButtonDown(1))
        {
            print("Apertei melee");
            AtacaMelee();
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
        var quadrante = PegaQuadrante();
        switch (quadrante)
        {
            case Quadrante.Leste: angulo = 0.0f; break;
            case Quadrante.Sul: angulo = 90.0f; break;
            case Quadrante.Oeste: angulo = 180.0f; break;
            case Quadrante.Norte: angulo = 270.0f; break;
        }

        if (espada != null)
        {
            var script = espada.GetComponent<Swing>();
            var duracaoTrajetoria = 90.0f / velocidadeEspada;
            StartCoroutine(script.swing(angulo, duracaoTrajetoria));
        }
    }

    private void OnDestroy()
    {
        municaoPiscina = null;
    }
}
