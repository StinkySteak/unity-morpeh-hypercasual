using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ParentGoalPostComponent : IComponent
{
    public bool IsUsed;
    public int LeftScore;
    public int RightScore;

    [Space]
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    public Vector3 LeftPosition => _left.position;
    public Vector3 RightPosition => _right.position;
}