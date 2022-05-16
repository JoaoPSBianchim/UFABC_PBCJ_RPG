using UnityEngine;

/// <summary>
/// Armazena as sentenças a serem exibidas.
/// </summary>
[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    // Nome do personagem.
    public string character;

    // Sentenças do diálogo.
    [TextArea(3, 10)]
    public string[] sentences;
}
