using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace RtsEcs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class UnitProvider : MonoProvider<UnitComponent>
    {

    }
}