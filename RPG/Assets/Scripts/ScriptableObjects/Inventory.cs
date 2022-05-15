using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Armazena o estado do inventário.
/// </summary>
[CreateAssetMenu(menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    // Guarda a lista de items no inventário entre cenas.
    List<Item> items;

    // Quantidade de espadas 
    public int quantidadeEspadas = 0;

    /// <summary>
    /// Inicializa a listagem de items.
    /// </summary>
    /*
    * Inicializa a listagem de items.
    */
    private void Awake()
    {
        items = new List<Item>();
    }

    /*
    * Obtém os items armazenados no inventário.
    */
    public List<Item> GetItems()
    {
        return items;
    }

    /*
    * Limpa os items do inventário.
    */
    public void Clear()
    {
        items = new List<Item>();
    }

    /*
    * Adiciona um item ao inventário.
    * i é o item a ser adicionado ao inventário
    */
    public void AddItem(Item i)
    {
        var item = items.Where(e => e.tipoItem == i.tipoItem).FirstOrDefault();
        if (item == null)
        {
            var itemToAdd = Instantiate(i);
            itemToAdd.quantidade = 1;
            items.Add(itemToAdd);
            return;
        }

        var index = items.IndexOf(item);
        items[index].quantidade += 1;
    }
}
