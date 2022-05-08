using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gerencia ações de botões.
/// </summary>
public class ButtonManager : MonoBehaviour
{
    /// <summary>
    /// Inicia um cenário especificado.
    /// </summary>
    /// <param name="cena"></param>
    public void StartCenario(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    /// <summary>
    /// Fecha a aplicação.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
