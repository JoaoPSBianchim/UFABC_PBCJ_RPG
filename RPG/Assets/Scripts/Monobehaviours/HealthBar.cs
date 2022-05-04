using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;       //Objeto de Leitura dos dados de quantos pontos tem o Player
    public Player caractere;            //Recebera o objeto do Player
    public Image medidorImagem;         //Recebe a barra de medicao
    public Text pdText;                 //Recebe os dados de PD
    float maxPontosDano;                //Armazena a quantidade limite de Saude do Player

    // Start is called before the first frame update
    void Start()
    {
        maxPontosDano = caractere.MaxPontoDano;
    }

    // Update is called once per frame
    void Update()
    {
        if(caractere != null)
        {
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;
            pdText.text = "PD: " + (medidorImagem.fillAmount * 100);
        }
        
    }
}
