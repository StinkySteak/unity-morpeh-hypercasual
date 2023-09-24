using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputCollectorSystem))]
public sealed class PlayerInputCollectorSystem : UpdateSystem
{
    private Filter _filter;
    private const string HORIZONTAL = "Horizontal";

    public override void OnAwake()
    {
        _filter = World.Filter.With<ActivePlayerInput>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        float horizontal = Input.GetAxisRaw(HORIZONTAL);
        
        foreach (Entity e in _filter)
        {
            ref ActivePlayerInput playerInput = ref e.GetComponent<ActivePlayerInput>();

            playerInput.Horizontal = horizontal;
        }
    }
}