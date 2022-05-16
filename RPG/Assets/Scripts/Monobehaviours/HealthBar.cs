using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla a barra de vidas.
/// </summary>
public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;       //Objeto de Leitura dos dados de quantos pontos tem o Player
    public Player caractere;            //Recebera o objeto do Player
    public Image medidorImagem;         //Recebe a barra de medicao
    public Text pdText;                 //Recebe os dados de PD

    //Armazena a quantidade limite de Saude do Player
    [HideInInspector]
    public float maxPontosDano;

    /*
    * Chamado antes da atualização do primeiro frame.
    */
    void Start()
    {
        maxPontosDano = caractere.MaxPontoDano;
    }

    /*
    * Atualização a cada frame.
    */
    void Update()
    {
        if (caractere != null)
        {
            maxPontosDano = caractere.MaxPontoDano;
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;
            pdText.text = "PD: " + (medidorImagem.fillAmount * 100 * (maxPontosDano/10));
        }

    }
}
