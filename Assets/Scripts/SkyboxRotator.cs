using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f; 

    private float currentRotation = 0f;

    void Update()
    {
        currentRotation += rotationSpeed * Time.deltaTime;
        currentRotation %= 360f; 

        if (RenderSettings.skybox.HasProperty("_Rotation"))
        {
            RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
        }
    }
}

