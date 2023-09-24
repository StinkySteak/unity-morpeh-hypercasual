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
    private Filter _mobileInputFilter;
    private const string HORIZONTAL = "Horizontal";

    public override void OnAwake()
    {
        _filter = World.Filter.With<ActivePlayerInput>().Build();

        CreateMobileInput();
    }

    private void CreateMobileInput()
    {
        _mobileInputFilter = World.Filter.With<ActiveMobileInput>().Build();

        Entity e = World.CreateEntity();
        e.AddComponent<ActiveMobileInput>();
    }

    public override void OnUpdate(float deltaTime)
    {
        CollectMobileInput();

        float horizontal = GetInput();

        foreach (Entity e in _filter)
        {
            ref ActivePlayerInput playerInput = ref e.GetComponent<ActivePlayerInput>();

            playerInput.Horizontal = horizontal;
        }
    }

    private void CollectMobileInput()
    {
        if (!Input.GetMouseButton(0)) return;

        Entity e = _mobileInputFilter.First();
        ref ActiveMobileInput mobileInput = ref e.GetComponent<ActiveMobileInput>();

        if (Input.GetMouseButtonDown(0))
        {
            mobileInput.TouchPositionX = Input.mousePosition.x;
        }

        float currentMousePositionX = Input.mousePosition.x;
        float previousMousePositionX = mobileInput.TouchPositionX;

        float delta = (currentMousePositionX - previousMousePositionX) / 2;

        mobileInput.TouchPositionX = currentMousePositionX;
        mobileInput.DeltaX = delta;
    }

    private float GetInput()
    {
        Entity e = _mobileInputFilter.First();

        ActiveMobileInput mobileInput = e.GetComponent<ActiveMobileInput>();

        return mobileInput.DeltaX;
    }
}