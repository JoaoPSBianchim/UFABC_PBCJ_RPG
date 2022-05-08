using UnityEngine;

/// <summary>
/// Dados para um item coletável pelo jogador.
/// </summary>
[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    // Nome para o item.
    public string nomeObjeto;

    // Sprite para o item.
    public Sprite sprite;

    // Quantidade do item.
    public int quantidade;

    // Indica se é um item empilhável ou não.
    public bool empilhavel;

    // Tipos de items possíveis.
    public enum TipoItem
    {
        MOEDA,
        HEALTH,
        DIAMANTE,
        ESCUDO,
        KNIFE,
        POTION,
    }

    // Tipo do item.
    public TipoItem tipoItem;

}
