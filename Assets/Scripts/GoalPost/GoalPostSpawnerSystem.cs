using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(GoalPostSpawnerSystem))]
public sealed class GoalPostSpawnerSystem : Initializer
{
    [SerializeField] private Vector3 _initialSpawnPosition;
    [SerializeField] private float _zOffset;
    [SerializeField] private ParentGoalPostProvider _goalPostPrefab;
    [SerializeField] private int _initialSpawnGoalPost;

    private Event<EventPostTriggered> OnPostTriggered;

    public override void OnAwake()
    {
        OnPostTriggered = World.GetEvent<EventPostTriggered>();
        OnPostTriggered.Subscribe(OnTriggered);

        InitialSpawnGoalPost();
    }

    private void InitialSpawnGoalPost()
    {
        Vector3 nextSpawnPosition = _initialSpawnPosition;

        for (int i = 0; i < _initialSpawnGoalPost; i++)
        {
            ParentGoalPostProvider go = Instantiate(_goalPostPrefab);
            go.transform.position = nextSpawnPosition;

            nextSpawnPosition += Vector3.forward * _zOffset;
        }
    }

    private void OnTriggered(FastList<EventPostTriggered> eventPostTriggered)
    {
        foreach (var e in eventPostTriggered)
        {
            if (World.TryGetEntity(in e.EntityId, out Entity goalPost))
            {
                Transform t = goalPost.GetComponent<UnityTransform>().Transform;
                t.position += _initialSpawnGoalPost * _zOffset * Vector3.forward;
                goalPost.GetComponent<ParentGoalPostComponent>().IsUsed = false;
            }
            return;
        }
    }
    public override void Dispose() { }
}