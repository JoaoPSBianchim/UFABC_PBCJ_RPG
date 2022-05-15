using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia ações de botões.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    /*
    Inicia um cenário especificado.
    cena é o nome da cena a ser Carregada para o jogo
     */
    public void StartCenario(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    /*
    Fecha a aplicação.
    */
    public void Exit()
    {
        Application.Quit();
    }
}
