using UnityEngine;

public class CameraPan : MonoBehaviour
{
    private Transform target;
    public float panSpeed = 2f;
    private bool isPanning = false;

    public void PanTo(Transform newTarget)
    {
        target = newTarget;
        isPanning = true;
    }

    void Update()
    {
        if (isPanning && target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * panSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * panSpeed);

            if (Vector3.Distance(transform.position, target.position) < 0.05f &&
                Quaternion.Angle(transform.rotation, target.rotation) < 0.5f)
            {
                transform.position = target.position;
                transform.rotation = target.rotation;
                isPanning = false;
            }
        }
    }
}
