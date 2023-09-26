using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FloorMovingSystem))]
public sealed class FloorMovingSystem : UpdateSystem
{
    private Filter _floorFilter;
    private Filter _playerMoveSpeedFilter;

    public override void OnAwake()
    {
        _floorFilter = World.Filter.With<UnityTransform>().With<FloorTag>().Build();
        _playerMoveSpeedFilter = World.Filter.With<MoveSpeed>().With<PlayerMovement>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        Entity e = _floorFilter.First();
        UnityTransform transform = e.GetComponent<UnityTransform>();

        transform.Transform.position += deltaTime * GetPlayerSpeed() * Vector3.forward;
    }

    private float GetPlayerSpeed()
    {
        Entity e = _playerMoveSpeedFilter.First();

        MoveSpeed speed = e.GetComponent<MoveSpeed>();

        return speed.Value;
    }
}