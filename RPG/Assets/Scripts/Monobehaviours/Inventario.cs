using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Item;

/// <summary>
/// Controla o inventário atual do player
/// </summary>
public class Inventario : MonoBehaviour
{
    // Objeto que recebe o prefab Slot.
    public GameObject slotPrefab;

    // Numero Fixo de Slots.
    public const int numSlots = 5;

    // Dados de inventário armazenados.
    public Inventory inventory;

    // Array de Imagens.
    Image[] itemImagens = new Image[numSlots];

    // Array de Slots.
    GameObject[] slots = new GameObject[numSlots];

    /**
     * Chamado antes do primeiro frame.
     */
    void Start()
    {
        CriaSlots();
    }

    /*
    * Cria os slots do inventário.
    */
    public void CriaSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                GameObject novoSlot = Instantiate(slotPrefab);
                novoSlot.name = "ItemSlot_" + i;
                novoSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = novoSlot;
                itemImagens[i] = novoSlot.transform.GetChild(1).GetComponent<Image>();
            }
            UpdateSlots();
        }
    }

    /*
    * Adiciona um item ao inventário.
    * item é o item a ser adiocionado ao inventário
    */
    public bool AddItem(Item item)
    {
        inventory.AddItem(item);
        UpdateSlots();
        return true;
    }

    /*
    * Remove um item do inventário.
    * tipo é o tipo do item a ser removido do inventário.
    * quantidade é a quantidade do item a ser removido do inventário.
    */
    public bool RemoveItem(TipoItem tipo, int quantidade)
    {
        inventory.RemoveItem(tipo, quantidade);
        UpdateSlots();
        return true;
    }

    /**
     * Obtém a quantidade de facas no inventário.
     */
    public int GetFacas()
    {
        var items = inventory.GetItems();
        var item = items.Where(e => e.tipoItem == TipoItem.KNIFE).FirstOrDefault();
        return item?.quantidade ?? 0;
    }

    /*
    * Atualiza as informações dos slots.
    */
    public void UpdateSlots()
    {
        var items = inventory.GetItems();
        for (var i = 0; i < items.Count; i++)
        {
            var item = items[i];

            if (item.empilhavel)
            {
                UpdateSlotText(i, item);
            }

            itemImagens[i].sprite = item.sprite;
            itemImagens[i].enabled = true;
        }
    }

    /*
    * Atualiza a contagem dos items de um slot do inventário.
    * i é o indice do slot no inventário
    * item é o item a ser atualizado
    */
    public void UpdateSlotText(int i, Item item)
    {
        var slotScript = slots[i].gameObject.GetComponent<Slot>();
        var quantidadeTexto = slotScript.qtdTexto;
        quantidadeTexto.enabled = true;
        quantidadeTexto.text = item.quantidade.ToString();
    }
}
