using Cinemachine;
using UnityEngine;

/// <summary>
/// Controla a câmera de visualização.
/// </summary>
public class RPGCameraManager : MonoBehaviour
{
    // Singleton para uma única instância de câmera.
    public static RPGCameraManager instanciaCompartilhada = null;

    // Câmera para visualização.
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

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
        GameObject vCamGameObject = GameObject.FindWithTag("Virtual Camera");
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
