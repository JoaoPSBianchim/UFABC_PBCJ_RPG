using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla o inventário atual do player
/// </summary>
public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab;                   // Objeto que recebe o prefab Slot
    public const int numSlots = 5;                  // Numero Fixo de Slots
    public Inventory inventory;                     // Dados de inventário armazenados.
    Image[] itemImagens = new Image[numSlots];      // Array de Imagens
    GameObject[] slots = new GameObject[numSlots];  // Array de Slots

    // Start is called before the first frame update
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
