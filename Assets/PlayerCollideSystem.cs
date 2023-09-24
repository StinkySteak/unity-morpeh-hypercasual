using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerCollideSystem))]
public sealed class PlayerCollideSystem : UpdateSystem
{
    private Filter _filter;
    private Collider[] _queries;
    private const int CACHE_SIZE = 3;

    public override void OnAwake()
    {
        _filter = World.Filter
            .With<PlayerCollider>()
            .With<UnityTransform>()
            .Build();

        _queries = new Collider[CACHE_SIZE];
    }

    public override void OnUpdate(float deltaTime)
    {
        Entity e = _filter.First();

        PlayerCollider collider = e.GetComponent<PlayerCollider>();
        UnityTransform transform = e.GetComponent<UnityTransform>();

        Physics.OverlapSphereNonAlloc(transform.Transform.position, collider.SphereRadius, _queries);

        CheckCollider();
    }

    private void CheckCollider()
    {
        foreach (Collider collider in _queries)
        {
            if(collider == null) continue;

            if(collider.TryGetComponent(out Goalpost goalpost))
            {
                Debug.Log($"Collided with: {goalpost.name}");
            }
        }
    }
}