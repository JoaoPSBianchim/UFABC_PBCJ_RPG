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

    // Variável de controle para resettar a vida.
    public bool resetPontosDano;

    // Dado da vida.
    public PontosDano pontosDano;

    // Variável de controle para indicar que o player está em processo de spawn.
    bool spawning;

    /**
     * Inicialização.
     */
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

    /**
     * Chamado antes da atualização do primeiro frame.
     */
    void Start()
    {
        SetupScene();
    }

    /**
     * Realiza o setup da cena. 
     */
    public void SetupScene()
    {
        if (clearInventory && inventory != null)
        {
            inventory.Clear();
        }
        if (resetPontosDano && pontosDano != null)
        {
            pontosDano.valor = 0f;
        }

        SpawnPlayer();
    }

    /**
     * Realiza o spawn do player no mapa.
     */
    public void SpawnPlayer()
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
            spawning = false;
        }
    }

    /**
     * Chamado a cada frame, se o MonoBehaviour estiver habilitado.
     */
    private void Update()
    {
        CheckSceneCondition();
    }

    /**
     * Verifica se a cena está completa, para prosseguir para a próxima cena.
     */
    private void CheckSceneCondition()
    {
        var player = GameObject.FindWithTag("Player");
        if (player == null && !spawning)
        {
            SceneManager.LoadScene(gameOverScene);
            return;
        }

        var enemies = GameObject.FindGameObjectsWithTag("Inimigo");
        if (enemies.Length == 0 && !string.IsNullOrWhiteSpace(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
