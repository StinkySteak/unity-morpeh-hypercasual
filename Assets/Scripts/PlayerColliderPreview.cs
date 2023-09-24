using UnityEngine;

public class PlayerColliderPreview : MonoBehaviour
{
    [SerializeField] private PlayerColliderConfig _config;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _config.SphereRadius);
    }
}
