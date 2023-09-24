using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerColliderInitializerSystem))]
public sealed class PlayerColliderInitializerSystem : Initializer
{

    [SerializeField] private PlayerColliderConfig _playerColliderConfig;

    public override void OnAwake()
    {
        var filter = World.Filter.With<PlayerCollider>().Build();
        
        Entity e = filter.FirstOrDefault();
        ref PlayerCollider playerCollider = ref e.GetComponent<PlayerCollider>();

        playerCollider.SphereRadius = _playerColliderConfig.SphereRadius;
    }

    public override void Dispose()
    {
    }
}