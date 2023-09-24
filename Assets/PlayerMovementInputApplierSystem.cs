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
    private Filter _filter;

    public override void OnAwake()
    {
        _filter = World.Filter.With<ActivePlayerInput>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        Entity e = _filter.First();

        ActivePlayerInput playerInput = e.GetComponent<ActivePlayerInput>();

        ApplyInput(playerInput.Horizontal);
    }

    private void ApplyInput(float horizontalInput)
    {
        Filter filter = World.Filter.With<PlayerMovement>().Build();

        Entity entity = filter.First();
        ref PlayerMovement playerMovement = ref entity.GetComponent<PlayerMovement>();

        playerMovement.HorizontalMove = horizontalInput;
    }
}