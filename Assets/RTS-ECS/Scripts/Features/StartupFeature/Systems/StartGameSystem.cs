using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace RtsEcs
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class StartGameSystem : ISystem
    {
        public World World { get; set; }

        private Filter Filter;
        private Stash<StartGameComponent> StartGameComponents;
        //private Stash<CreateTankComponent> CreateTankStash;
        private Stash<SpawnComponent> SpawnComponents;

        public void OnAwake()
        {
            Filter = World.Filter
                .With<StartGameComponent>()
                .Build();

            SpawnComponents = World.GetStash<SpawnComponent>();
            StartGameComponents = World.GetStash<StartGameComponent>();
            //CreateTankStash = World.GetStash<CreateTankComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in Filter)
            {
                //CreateTankStash.Add(entity);
                SpawnComponents.Add(entity, new SpawnComponent()
                {
                    Count = 40000,
                });
                StartGameComponents.Remove(entity);
            }
        }

        public void Dispose()
        {

        }
    }
}