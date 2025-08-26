using UnityEngine;

public class TravelAroundEpicenter : MonoBehaviour
{
    public Transform epicenter;
    public float radius = 5f;
    public float angularSpeed = 30f;

    public Vector3 orbitAxis = new Vector3(0.5f, 1f, 0.5f);

    public float selfRotationSpeed = 50f;     // degrees per second, spin like a planet
    public Vector3 selfRotationAxis = Vector3.up;

    private float currentAngle = 0f;
    
    void FixedUpdate()
    {
        currentAngle += angularSpeed * Time.fixedDeltaTime;
        currentAngle %= 360f;

        Vector3 flatOrbit = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * radius;

        // Create a rotation that orients the flat orbit plane to the orbitAxis
        Quaternion tilt = Quaternion.FromToRotation(Vector3.up, orbitAxis.normalized);

        // Apply tilt to the orbit path
        Vector3 tiltedOrbit = tilt * flatOrbit;

        // Self-rotation (planet spin)
        transform.Rotate(selfRotationAxis.normalized, selfRotationSpeed * Time.fixedDeltaTime, Space.Self);

        // Apply final position
        transform.position = epicenter.position + tiltedOrbit;
    }
    
}
