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

    // Nome da cena de fim de jogo.
    public string gameOverScene;

    // Variável de controle para limpar o inventário.
    public bool clearInventory;

    // Dados do inventário.
    public Inventory inventory;

    // Variável de controle para indicar que o player está em processo de spawn.
    bool spawning;

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

        spawning = true;
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
        if (clearInventory && inventory != null)
        {
            inventory.Clear();
        }

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
            spawning = false;
        }
    }

    /// <summary>
    /// Chamado a cada frame, se o MonoBehaviour estiver habilitado.
    /// </summary>
    private void Update()
    {
        CheckScene();
    }

    /// <summary>
    /// Verifica se a cena está completa, para prosseguir para a próxima cena.
    /// </summary>
    private void CheckScene()
    {
        var player = GameObject.FindWithTag("Player");
        if (player == null && !spawning)
        {
            SceneManager.LoadScene(gameOverScene);
            return;
        }

        var collectables = GameObject.FindGameObjectsWithTag("Coletavel");
        var enemies = GameObject.FindGameObjectsWithTag("Inimigo");

        if (collectables.Length == 0 && enemies.Length == 0 && !string.IsNullOrWhiteSpace(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }

        // // Quando é a última cena, a única condição de vitória é matar o boss.
        // if (SceneManager.GetActiveScene().name == "StageBoss" && enemies.Length == 0)
        // {
        //     SceneManager.LoadScene(nextScene);
        // }
    }
}
