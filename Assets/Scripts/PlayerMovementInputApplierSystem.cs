using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMovementInputApplierSystem))]
public sealed class PlayerMovementInputApplierSystem : UpdateSystem
{
    private Filter _playerInputfilter;
    private Filter _playerMovementFilter;

    public override void OnAwake()
    {
        _playerInputfilter = World.Filter.With<ActivePlayerInput>().Build();
        _playerMovementFilter = World.Filter.With<PlayerMovement>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        float horizontalInput = GetHorizontalInput();

        ApplyInput(horizontalInput);
    }

    private float GetHorizontalInput()
    {
        Entity e = _playerInputfilter.First();

        ActivePlayerInput playerInput = e.GetComponent<ActivePlayerInput>();

        return playerInput.Horizontal;
    }

    private void ApplyInput(float horizontalInput)
    {
        Entity entity = _playerMovementFilter.First();
        ref PlayerMovement playerMovement = ref entity.GetComponent<PlayerMovement>();

        playerMovement.HorizontalMove = horizontalInput;
    }
}