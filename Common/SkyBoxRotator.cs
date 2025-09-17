using UnityEngine;

namespace Script.Common
{
    public class SkyboxRotator : MonoBehaviour
    {
        int Rotation => Shader.PropertyToID("_Rotation");
        float RotationSpeed => 0.2f;

        void Update()
        {
            float currentRotation = RenderSettings.skybox.GetFloat(Rotation);
            currentRotation += RotationSpeed * Time.deltaTime;
            
            RenderSettings.skybox.SetFloat(Rotation, currentRotation);
        }
    }
}