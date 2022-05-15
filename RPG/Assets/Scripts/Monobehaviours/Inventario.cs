using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>
    /// Cria os slots do inventário.
    /// </summary>
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

    /// <summary>
    /// Adiciona um item ao inventário.
    /// </summary>
    /// <param name="item">item a ser adicionado</param>
    /// <returns></returns>
    public bool AddItem(Item item)
    {
        inventory.AddItem(item);
        UpdateSlots();
        if(item.tipoItem.ToString() == "KNIFE"){
            inventory.quantidadeEspadas += 10;
                
       }
        return true;

       
       
    }

    /// <summary>
    /// Atualiza as informações dos slots.
    /// </summary>
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

    /// <summary>
    /// Atualiza a contagem dos items de um slot do inventário.
    /// </summary>
    /// <param name="i">Índice do slot.</param>
    /// <param name="item">Item a ser atualizado.</param>
    public void UpdateSlotText(int i, Item item)
    {
        var slotScript = slots[i].gameObject.GetComponent<Slot>();
        var quantidadeTexto = slotScript.qtdTexto;
        quantidadeTexto.enabled = true;
        quantidadeTexto.text = item.quantidade.ToString();
    }
}
