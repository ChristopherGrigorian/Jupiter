using System.Xml;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main) transform.forward = Camera.main.transform.forward;
    }
}
