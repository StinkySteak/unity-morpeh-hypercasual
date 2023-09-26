using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ActiveMobileInput : IComponent
{
    public float TouchPositionX;
    public float DeltaX;
}