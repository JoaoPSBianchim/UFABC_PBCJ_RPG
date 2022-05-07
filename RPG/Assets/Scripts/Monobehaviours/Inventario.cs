using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab;                   // Objeto que recebe o prefab Slot
    public const int numSlots = 5;                  // Numero Fixo de Slots
    Image[] itemImagens = new Image[numSlots];      // Array de Imagens
    Item[] items = new Item[numSlots];              // Array de Itens
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
        }
    }

    /// <summary>
    /// Adiciona um item ao inventário.
    /// </summary>
    /// <param name="itemToAdd">item a ser adicionado</param>
    /// <returns></returns>
    public bool AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].tipoItem == itemToAdd.tipoItem && itemToAdd.empilhavel == true)
            {
                items[i].quantidade = items[i].quantidade + 1;
                UpdateSlotText(i);
                return true;
            }
            if (items[i] == null)
            {
                items[i] = Instantiate(itemToAdd);
                items[i].quantidade = 1;
                itemImagens[i].sprite = itemToAdd.sprite;
                itemImagens[i].enabled = true;
                UpdateSlotText(i);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Atualiza a contagem dos items de um slot do inventário.
    /// </summary>
    /// <param name="i">Índice do slot</param>
    public void UpdateSlotText(int i)
    {
        var slotScript = slots[i].gameObject.GetComponent<Slot>();
        var quantidadeTexto = slotScript.qtdTexto;
        quantidadeTexto.enabled = true;
        quantidadeTexto.text = items[i].quantidade.ToString();
    }
}
