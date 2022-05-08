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

    private void Awake()
    {
        items = new List<Item>();
    }

    public List<Item> GetItems()
    {
        return items;
    }

    public void Clear()
    {
        items = new List<Item>();
    }

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
