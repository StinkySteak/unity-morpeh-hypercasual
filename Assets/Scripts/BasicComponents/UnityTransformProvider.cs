using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class UnityTransformProvider : MonoProvider<UnityTransform>
{
    private void Reset()
    {
        ref UnityTransform unityTransform = ref GetData();
        unityTransform.Transform = transform;
    }
}