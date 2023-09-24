using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Unity.Collections;
using System.Runtime.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerCollideSystem))]
public sealed class PlayerCollideSystem : UpdateSystem
{
    private Filter _playerFilter;
    private Filter _scoreFilter;
    private Filter _parentGoalPostFilter;
    private Filter _playerCollideDistanceFilter;

    public override void OnAwake()
    {
        ConstructFilters();
    }

    private void ConstructFilters()
    {
        _playerFilter = World.Filter
            .With<UnityTransform>()
            .With<PlayerTag>()
            .Build();

        _scoreFilter = World.Filter
            .With<PlayerScore>()
            .Build();

        _parentGoalPostFilter = World.Filter
            .With<ParentGoalPostComponent>()
            .With<UnityTransform>()
            .Build();

        _playerCollideDistanceFilter = World.Filter
            .With<PlayerGoalDetectionDistance>()
            .Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        CheckCollider(GetPlayerPosition());
    }

    private Vector3 GetPlayerPosition()
    {
        Entity e = _playerFilter.First();

        UnityTransform transform = e.GetComponent<UnityTransform>();

        return transform.GetPosition();
    }

    private void CheckCollider(Vector3 playerPosition)
    {
        float minDistance = _playerCollideDistanceFilter.First().GetComponent<PlayerGoalDetectionDistance>().Distance;

        foreach (Entity e in _parentGoalPostFilter)
        {
            ref ParentGoalPostComponent goalPost = ref e.GetComponent<ParentGoalPostComponent>();

            float playerDistanceToLeft = Vector3.Distance(playerPosition, goalPost.LeftPosition);
            float playerDistanceToRight = Vector3.Distance(playerPosition, goalPost.RightPosition);

            NativeArray<float> distances = new(2, Allocator.Temp);
            distances[0] = playerDistanceToLeft;
            distances[1] = playerDistanceToRight;

            NativeArray<int> scores = new(2, Allocator.Temp);
            scores[0] = goalPost.LeftScore;
            scores[1] = goalPost.RightScore;

            if (goalPost.IsUsed)
            {
                scores.Dispose();
                distances.Dispose();
                continue;
            }

            for (int i = 0; i < 2; i++)
            {
                float distance = distances[i];

                bool isReached = distance <= minDistance;

                if (!isReached) continue;

                Entity entityScore = _scoreFilter.FirstOrDefault();

                ref PlayerScore score = ref entityScore.GetComponent<PlayerScore>();

                score.Value += scores[i];

                goalPost.IsUsed = true;

                World.GetEvent<EventPostTriggered>()
                    .NextFrame(new EventPostTriggered() { EntityId = e.ID });

                World.GetEvent<EventScoreUpdated>()
                    .NextFrame(new EventScoreUpdated() { Score = score.Value });
                break;
            }

            scores.Dispose();
            distances.Dispose();
        }
    }
}