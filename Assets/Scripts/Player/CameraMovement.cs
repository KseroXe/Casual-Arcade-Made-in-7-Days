using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
