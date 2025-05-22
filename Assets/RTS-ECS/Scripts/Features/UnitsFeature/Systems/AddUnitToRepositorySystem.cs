using RtsEcs;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public sealed class AddUnitToRepositorySystem : ISystem 
{
    private Stash<UnitRepositoryComponent> UnitRepositoryStash;
    private Stash<AddUnitToRepositoryComponent> AddUnitToRepositoryStash;

    public World World { get; set;}

    private Filter Filter;
    private Filter UnitRepositoryFilter;

    public void OnAwake() 
    {
        UnitRepositoryFilter = World.Filter.With<UnitRepositoryComponent>().Build();
        Filter = World.Filter.With<AddUnitToRepositoryComponent>().Build();

        UnitRepositoryStash = World.GetStash<UnitRepositoryComponent>();
        AddUnitToRepositoryStash = World.GetStash<AddUnitToRepositoryComponent>();
    }

    public void OnUpdate(float deltaTime) 
    {
        foreach (var entity in Filter)
        {
            ref var addUnitToRepository = ref AddUnitToRepositoryStash.Get(entity);
            ref var unitRepository = ref UnitRepositoryStash.Get(UnitRepositoryFilter.First());

            unitRepository.Units.Add(addUnitToRepository.UnitToAdd);

            AddUnitToRepositoryStash.Remove(entity);
        }
    }

    public void Dispose()
    {

    }
}