using RtsEcs;
using Scellecs.Morpeh;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class UnitListInitializer : IInitializer 
{
    private Stash<UnitRepositoryComponent> UnitListComponent;

    public World World { get; set;}

    public void OnAwake() 
    {
        UnitListComponent = World.GetStash<UnitRepositoryComponent>();

        var entity = World.CreateEntity();

        UnitListComponent.Set(entity, new UnitRepositoryComponent()
        {
            Units = new List<UnitComponent>(),
        });
    }

    public void Dispose()
    {

    }
}