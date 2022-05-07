using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPGGameManager : MonoBehaviour
{
    // Singleton para uma única instância de gerenciador de jogo.
    public static RPGGameManager instanciaCompartilhada = null;

    // Gerenciador de câmera.
    public RPGCameraManager cameraManager;

    // Ponto de spawn do jogador.
    public PontoSpawn playerPontoSpawn;

    // Nome da próxima cena a ser carregada.
    public string nextScene;

    /// <summary>
    /// Inicialização do objeto.
    /// </summary>
    private void Awake()
    {
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
    }

    /// <summary>
    /// Chamado antes da atualização do primeiro frame.
    /// </summary>
    void Start()
    {
        SetupScene();
    }

    /// <summary>
    /// Realiza o setup da cena. 
    /// </summary>
    public void SetupScene()
    {
        SpawnPlayer();
    }

    /// <summary>
    /// Realiza o spawn do player no mapa.
    /// </summary>
    public void SpawnPlayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }

    /// <summary>
    /// Chamado a cada frame, se o MonoBehaviour estiver habilitado.
    /// </summary>
    private void Update()
    {
        CheckEndScene();
    }

    /// <summary>
    /// Verifica se a cena está completa, para prosseguir para a próxima cena.
    /// </summary>
    private void CheckEndScene()
    {
        var collectables = GameObject.FindGameObjectsWithTag("Coletavel");
        var enemies = GameObject.FindGameObjectsWithTag("Inimigo");

        if (collectables.Length == 0 && enemies.Length == 0 && !string.IsNullOrWhiteSpace(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
