using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define um ponto de spawn de um prefab.
/// </summary>
public class PontoSpawn : MonoBehaviour
{
    // Prefab para ser realizado o spawn.
    public GameObject prefabParaSpawn;

    // Intervalo para spawn repetitivo do prefab.
    public float intervaloRepeticao;

    // Indica se deve fazer o spawn automaticamente.
    public bool autoSpawn;

    /*
    * Chamado antes da atualização do primeiro frame.
    */
    void Start()
    {
        if (!autoSpawn)
        {
            return;
        }

        // Configura timer para spawn, se necessário.
        if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }
        else
        {
            SpawnO();
        }
    }

    /*
    * Realiza o spawn do prefab configurado.
    */
    public GameObject SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }
}
