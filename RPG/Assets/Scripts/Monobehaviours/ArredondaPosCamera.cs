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

    /*
    * Arrendonda um valor de acordo com o PixelsPerUnit.
    * x ? o valor a ser arredondado
    */
    float Round(float x)
    {
        return Mathf.Round(x * PixelsPerUnit) / PixelsPerUnit;
    }
}
