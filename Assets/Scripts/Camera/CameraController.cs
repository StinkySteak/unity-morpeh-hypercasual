using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    private void LateUpdate()
    {
        float zPosition = _player.position.z;
        Vector3 forward = zPosition * Vector3.forward;
        transform.position = forward + _offset;
    }
}
