using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoSpawn : MonoBehaviour
{
    // Prefab para ser realizado o spawn.
    public GameObject prefabParaSpawn;

    // Intervalo para spawn repetitivo do prefab.
    public float intervaloRepeticao;

    /// <summary>
    /// Chamado antes da atualização do primeiro frame.
    /// </summary>
    void Start()
    {
        // Configura timer para spawn, se necessário.
        if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }
    }

    /// <summary>
    /// Realiza o spawn do prefab configurado.
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate(prefabParaSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }
}
