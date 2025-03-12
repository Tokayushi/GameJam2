using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Asigna el Player en el Inspector
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }
}
