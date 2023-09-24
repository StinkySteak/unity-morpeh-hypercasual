using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerColliderConfig), menuName = "Data/PlayerCollider")]
public class PlayerColliderConfig : ScriptableObject
{
    [SerializeField] private float _sphereRadius;

    public float SphereRadius => _sphereRadius;
}
