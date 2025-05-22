using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace RtsEcs
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct AddUnitToRepositoryComponent : IComponent
    {
        public UnitProvider UnitToAdd;
    }
}