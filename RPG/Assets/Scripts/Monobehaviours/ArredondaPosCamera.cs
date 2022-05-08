using UnityEngine;
using Cinemachine;

/// <summary>
/// Tratamento adicional ao Cinemachine.
/// </summary>
public class ArredondaPosCamera : CinemachineExtension
{
    public float PixelsPerUnit = 32; // Pixeis por unidade;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.FinalPosition;
            Vector3 pos2 = new Vector3(Round(pos.x), Round(pos.y), pos.z);
            state.PositionCorrection += pos2 - pos;
        }
    }

    /// <summary>
    /// Arrendonda um valor de acordo com o PixelsPerUnit.
    /// </summary>
    /// <param name="x">Valor a ser arredondado.</param>
    /// <returns>Valor arredondado.</returns>
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
