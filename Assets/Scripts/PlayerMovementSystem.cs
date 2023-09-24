using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMovementSystem))]
public sealed class PlayerMovementSystem : UpdateSystem
{
    private Filter _filter;

    public override void OnAwake()
    {
        _filter = World.Filter
            .With<PlayerMovement>()
            .With<MoveSpeed>()
            .With<UnityTransform>()
            .Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (Entity e in _filter)
        {
            Transform body = e.GetComponent<UnityTransform>().Transform;
            float moveSpeed = e.GetComponent<MoveSpeed>().Value;
            float horizontalMove = e.GetComponent<PlayerMovement>().HorizontalMove;
            float rightMoveSpeed = e.GetComponent<PlayerMovement>().RightMoveSpeed;

            float maxHorizontalDistance = e.GetComponent<PlayerMovement>().MaxHorizontalDistance;

            Vector3 forward = moveSpeed * body.transform.forward;
            Vector3 right = horizontalMove * rightMoveSpeed * body.transform.right;

            LimitHorizontalMove(maxHorizontalDistance, body.transform.position, ref right);

            body.transform.position += (forward + right) * deltaTime;
        }
    }

    private void LimitHorizontalMove(float maxHorizontalDistance, Vector3 playerPosition, ref Vector3 right)
    {
        if (playerPosition.x <= -maxHorizontalDistance && right.x < 0)
        {
            right.x = 0;
            return;
        }

        if (playerPosition.x >= maxHorizontalDistance && right.x > 0)
        {
            right.x = 0;
        }
    }
}